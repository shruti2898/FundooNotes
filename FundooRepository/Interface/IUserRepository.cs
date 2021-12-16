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
        string Login(UserCredentialsModel loginUser);

        bool ResetPassword(UserCredentialsModel userCredentials);
        bool ForgotPassword(string userEmail);
    }
}
