using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        RegisterModel Register(RegisterModel user);
        string Login(UserCredentialsModel loginUser);
    }
}
