using FundooModels;
using FundooManager.Interface;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<RegisterModel> Register(RegisterModel user)
        {
            try
            {
                return await this.repository.Register(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RegisterModel> Login(UserCredentialsModel loginUser)
        {
            try
            {
                return await this.repository.Login(loginUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ResetPassword(UserCredentialsModel userCredentials)
        {
            try
            {
                return await this.repository.ResetPassword(userCredentials);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ForgotPassword(string userEmail)
        {
            try
            {
                return await this.repository.ForgotPassword(userEmail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
