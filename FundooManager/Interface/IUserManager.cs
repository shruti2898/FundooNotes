using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        RegisterModel Register(RegisterModel user);

        string Login(UserCredentialsModel loginUser);
    }
}
