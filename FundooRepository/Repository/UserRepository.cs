using FundooModels;
using FundooRepository.Interface;
using FundooRepository.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public IConfiguration configuration { get; }
        public UserRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<RegisterModel> Register(RegisterModel userDetails)
        {
            try
            {  
                var emailExist = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userDetails.Email));

                if (emailExist == null)
                {  
                    userDetails.Password = PasswordEncryption(userDetails.Password);
                    this.context.Users.Add(userDetails);
                    await this.context.SaveChangesAsync();
                    return userDetails;
                }
                return null;
            }
            catch (ArgumentNullException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<RegisterModel> Login(UserCredentialsModel userCredentials)
        {
            try
            {
                var userEmailExist = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail));
                if (userEmailExist != null)
                {
                    userCredentials.UserPassword = PasswordEncryption(userCredentials.UserPassword);
                    var userDetails = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail) && user.Password.Equals(userCredentials.UserPassword));

                    ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configuration["RedisServer"]);
                    IDatabase database = multiplexer.GetDatabase();
                    database.StringSet(key: "User ID", userDetails.UserId.ToString());
                    database.StringSet(key: "First Name", userDetails.FirstName);
                    database.StringSet(key: "Last Name", userDetails.LastName);

                    if (userDetails != null)
                    {
                        return userDetails;
                    }
                    return null;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ResetPassword(UserCredentialsModel userCredentials)
        {
            try                      
            {
                var userInfo = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail));
                if (userInfo != null)
                {
                    userCredentials.UserPassword = PasswordEncryption(userCredentials.UserPassword);
                    userInfo.Password = userCredentials.UserPassword;
                    this.context.Users.Update(userInfo);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (ArgumentNullException ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ForgotPassword(string userEmail)
        {
            try
            {
                var userDetails = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userEmail));
                if (userDetails != null)
                {
                    string userDisplayName = userDetails.FirstName + " " + userDetails.LastName;
                    string SmtpEmail = configuration.GetValue<string>("Smtp:SmtpUsername");
                    string SmtpPassword = configuration.GetValue<string>("Smtp:SmtpPassword");
                    MailMessage sendEmail = new MailMessage();

                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                    sendEmail.From = new MailAddress(SmtpEmail);
                    sendEmail.To.Add(userEmail);
                    sendEmail.Subject = "Reset your password";
                    SendMSMQ(userDisplayName);
                    sendEmail.Body = ReceiveMSMQ();
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential(SmtpEmail, SmtpPassword);
                    smtpServer.EnableSsl = true;

                    await smtpServer.SendMailAsync(sendEmail);
                    return true;
                }
                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
            string body = $"Hello {userDisplayName}, A password reset for your account was requested.Please click the link below to change your password.";
            messageQueue.Label = "Mail Body";
            messageQueue.Send(body);
        }

        public string ReceiveMSMQ()
        {
            MessageQueue messageQueue = new MessageQueue(@".\Private$\Fundoo");
            var receiveMessage = messageQueue.Receive();
            receiveMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receiveMessage.Body.ToString();
        }

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
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string GenrateJwtToken(string email)
        {
            string secret = configuration.GetValue<string>("SecretJWT");
            byte[] key = Encoding.UTF8.GetBytes(secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
