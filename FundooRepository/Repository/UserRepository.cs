﻿using FundooModels;
using FundooRepository.Interface;
using FundooRepository.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public RegisterModel Register(RegisterModel userDetails)
        {
            try
            {
                bool ifEmailExist = this.context.Users.Any(user => user.Email.Equals(userDetails.Email));

                if (!ifEmailExist)
                {
                    userDetails.Password = PasswordEncryption(userDetails.Password);
                    this.context.Users.Add(userDetails);
                    this.context.SaveChanges();
                    return userDetails;
                }
                else
                {
                    return null;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Login(UserCredentialsModel userCredentials)
        {
            try
            {
                bool userEmailExist = this.context.Users.Any(user => user.Email.Equals(userCredentials.UserEmail));
                if (userEmailExist)
                {
                    //userCredentials.UserPassword = PasswordEncryption(userCredentials.UserPassword);
                    var result = this.context.Users.Where(user => user.Email.Equals(userCredentials.UserEmail) && user.Password.Equals(userCredentials.UserPassword)).Count();
                    if (result == 1)
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
                    return "Incorrect Email";
                }
            }
            catch (ArgumentNullException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public static string PasswordEncryption(string password)
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
