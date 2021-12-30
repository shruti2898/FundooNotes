// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooManager.Manager
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;

    /// <summary>
    /// User Manager Class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.IUserManager" />
    public class UserManager : IUserManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// User data after successful registration
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="loginUser">The login user.</param>
        /// <returns>
        /// User data after logging in successfully
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>
        /// True if password is changed successfully else false
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Sends email for reset password.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>
        /// True if password reset link is mailed successfully else false
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Token string
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public string GenerateJwtToken(string email)
        {
            try
            {
                return this.repository.GenerateJwtToken(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
