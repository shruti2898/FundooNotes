using FundooModels;
using FundooRepository.Interface;
using FundooRepository.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<RegisterModel> Register(RegisterModel userDetails)
        {
            try
            {
                bool emailExist = this.context.Users.Any(user => user.Email.Equals(userDetails.Email));

                if (!emailExist)
                {
                    userDetails.Password = PasswordEncryption(userDetails.Password);
                    this.context.Users.Add(userDetails);
                    await this.context.SaveChangesAsync();
                    return userDetails;
                }
                else
                {
                    return null;
                }
            }
            catch (ArgumentNullException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public string Login(UserCredentialsModel userCredentials)
        {
            try
            {
                bool userEmailExist = this.context.Users.Any(user => user.Email.Equals(userCredentials.UserEmail));
                if (userEmailExist)
                {
                    userCredentials.UserPassword =  PasswordEncryption(userCredentials.UserPassword);
                    var result =  this.context.Users.SingleOrDefault(user => user.Email.Equals(userCredentials.UserEmail) && user.Password.Equals(userCredentials.UserPassword));
                    if (result != null)
                    {   
                        
                        return "Logged in successfully";
                    }
                    else
                    {
                        return "Incorrect Password";
                    }
                }
                else
                {  
                    return "Email does not exist in our system";
                }
            }
            catch (ArgumentNullException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool ResetPassword(UserCredentialsModel userCredentials)
        {
            try
            {
                var userInfo = this.context.Users.SingleOrDefault(user => user.Email.Equals(userCredentials.UserEmail));
                if (userInfo != null)
                {
                    userCredentials.UserPassword = PasswordEncryption(userCredentials.UserPassword);
                    userInfo.Password = userCredentials.UserPassword;
                    this.context.Users.Update(userInfo);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool ForgotPassword(string userEmail)
        {
            try
            {
                var userDetails = this.context.Users.SingleOrDefault(user => user.Email.Equals(userEmail));
                if (userDetails != null)
                {
                    string userName = userDetails.FirstName + " " + userDetails.LastName;
                    int sendMailSuccess = SendEmail(userEmail, userName);

                    if (sendMailSuccess == 1)
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public int SendEmail(string emailAddress, string userName)
        {
            try
            {
                MailMessage sendEmail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                sendEmail.From = new MailAddress("shruti160447@gmail.com");
                
                sendEmail.To.Add(emailAddress);
                sendEmail.Subject = "Reset your password";
                sendEmail.Body = $"Hello {userName}, A password reset for your account was requested. Please click the link below to change your password.";

                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("shruti160447@gmail.com", "160447@Cse");
                smtpServer.EnableSsl = true;

                smtpServer.Send(sendEmail);
                sendEmail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                int result = ((int)sendEmail.DeliveryNotificationOptions);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string PasswordEncryption(string password)
        {
            try
            {
                byte[] encryptData = new byte[password.Length];
                encryptData = Encoding.UTF8.GetBytes(password);
                string encodedData= Convert.ToBase64String(encryptData);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
