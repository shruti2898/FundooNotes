// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooRepository.Repository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Experimental.System.Messaging;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;
    using System.Collections.Generic;
    using FundooRepository.CustomException;

    /// <summary>
    /// User Repository Class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The context for User
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configuration">The configuration.</param>
        public UserRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns>
        /// User data after successful registration
        /// </returns>
        public async Task<RegisterModel> Register(RegisterModel userDetails)
        {
          
            ArgumentValidator.Validate(userDetails);
            var emailExist = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userDetails.Email));
            if (emailExist == null)
            {
                userDetails.Password = this.PasswordEncryption(userDetails.Password);
                this.context.Users.Add(userDetails);
                await this.context.SaveChangesAsync();
                return userDetails;
            }
            throw new CustomExistingDataException("Email already exist");
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>
        /// Jwt token string
        /// </returns>
        public async Task<string> Login(UserCredentialsModel userCredentials)
        {
            ArgumentValidator.Validate(userCredentials);
            var userEmailExist = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail));
            if (userEmailExist != null)
            {
                userCredentials.UserPassword = this.PasswordEncryption(userCredentials.UserPassword);
                var userDetails = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail) && user.Password.Equals(userCredentials.UserPassword));

                if (userDetails != null)
                {
                    ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(this.Configuration["RedisServer"]);
                    IDatabase database = multiplexer.GetDatabase();
                    database.StringSet(key: "User ID", userDetails.UserId.ToString());
                    database.StringSet(key: "First Name", userDetails.FirstName);
                    database.StringSet(key: "Last Name", userDetails.LastName);

                    string token = GenerateJwtToken(userDetails.Email, userDetails.UserId);
                    return token;
                }
                throw new CustomUnauthorizedException("Invalid credentials");
            }
            throw new CustomNotFoundException("Email does not exist in our system");
        }

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Token string
        /// </returns>
        public string GenerateJwtToken(string email, int userId)
        {
            string secret = this.Configuration.GetValue<string>("SecretJWT");
            byte[] key = Encoding.UTF8.GetBytes(secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, email),
                new Claim("UserId", userId.ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>
        /// True if password is changed successfully 
        /// </returns>
        public async Task<bool> ResetPassword(UserCredentialsModel userCredentials)
        {   
            ArgumentValidator.Validate(userCredentials);
            var userInfo = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail));
            if (userInfo != null)
            {
                userCredentials.UserPassword = this.PasswordEncryption(userCredentials.UserPassword);
                userInfo.Password = userCredentials.UserPassword;
                this.context.Users.Update(userInfo);
                await this.context.SaveChangesAsync();
                return true;
            }
            throw new CustomNotFoundException("Email does not exist in our system");
        }

        /// <summary>
        /// Sends email for reset password.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>
        /// True if password reset link is mailed successfully
        /// </returns>
        public async Task<bool> ForgotPassword(string userEmail)
        {
            ArgumentValidator.Validate(userEmail);
            var userDetails = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userEmail));
            if (userDetails != null)
            {
                string userDisplayName = userDetails.FirstName + " " + userDetails.LastName;
                string smtpEmail = this.Configuration.GetValue<string>("Smtp:SmtpUsername");
                string smtpPassword = this.Configuration.GetValue<string>("Smtp:SmtpPassword");
                MailMessage sendEmail = new MailMessage();

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                sendEmail.From = new MailAddress(smtpEmail);
                sendEmail.To.Add(userEmail);
                sendEmail.Subject = "Reset your password";
                this.SendMSMQ(userDisplayName);
                sendEmail.Body = this.ReceiveMSMQ();
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword);
                smtpServer.EnableSsl = true;

                await smtpServer.SendMailAsync(sendEmail);
                return true;
            }
            throw new CustomNotFoundException("Email does not exist in our system"); 
        }

        /// <summary>
        /// Sends message to the MSMQ.
        /// </summary>
        /// <param name="userDisplayName">Display name of the user.</param>
        public void SendMSMQ(string userDisplayName)
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\Fundoo"))
            {
                messageQueue = new MessageQueue(@".\Private$\Fundoo");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\Fundoo");
            }

            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = $"Hello {userDisplayName},\n " +
                          $"A password reset for your account was requested.Please click the link below to change your password.\n" +
                          $"http://localhost:4200/resetPassword";
            messageQueue.Label = "Mail Body";
            messageQueue.Send(body);
        }

        /// <summary>
        /// Receives message from the MSMQ.
        /// </summary>
        /// <returns>Mail body</returns>
        public string ReceiveMSMQ()
        {
            MessageQueue messageQueue = new MessageQueue(@".\Private$\Fundoo");
            var receiveMessage = messageQueue.Receive();
            receiveMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receiveMessage.Body.ToString();
        }

        /// <summary>    
        /// Passwords the encryption.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Encrypted password</returns>
        /// <exception cref="System.Exception">Error in base64Encode exception message</exception>
        public string PasswordEncryption(string password)
        {
            try
            {
                byte[] encryptData = new byte[password.Length];
                encryptData = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encryptData);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode " + ex.Message);
            }
        }
    }
}
