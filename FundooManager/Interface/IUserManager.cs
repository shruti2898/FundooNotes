using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<RegisterModel> Register(RegisterModel user);

        string Login(UserCredentialsModel loginUser);

        bool ResetPassword(UserCredentialsModel userCredentials);
    }
}
