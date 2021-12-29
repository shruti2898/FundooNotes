using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        Task<RegisterModel> Register(RegisterModel user);
        Task<RegisterModel> Login(UserCredentialsModel loginUser);
        Task<bool> ResetPassword(UserCredentialsModel userCredentials);
        Task<bool> ForgotPassword(string userEmail);
        string GenrateJwtToken(string email);
    }
}
