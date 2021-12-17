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
                var emailExist = await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userDetails.Email));

                if (emailExist==null)
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
                var userEmailExist= await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail));
                if (userEmailExist != null)
                {
                    userCredentials.UserPassword =  PasswordEncryption(userCredentials.UserPassword);
                    var userDetails =  await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail) && user.Password.Equals(userCredentials.UserPassword));
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
                var userInfo =  await this.context.Users.SingleOrDefaultAsync(user => user.Email.Equals(userCredentials.UserEmail));
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

                    MailMessage sendEmail = new MailMessage();

                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                    sendEmail.From = new MailAddress("shruti160447@gmail.com");
                    sendEmail.To.Add(userEmail);
                    sendEmail.Subject = "Reset your password";
                    sendEmail.Body = $"Hello {userDisplayName}, A password reset for your account was requested. Please click the link below to change your password.";

                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential("shruti160447@gmail.com", "160447@Cse");
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
