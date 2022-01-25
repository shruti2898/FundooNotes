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
    using FundooRepository.CustomException;
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
        public async Task<RegisterModel> Register(RegisterModel user)
        {
            return await this.repository.Register(user);
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="loginUser">The login user.</param>
        /// <returns>
        /// Jwt token string
        /// </returns>
        public async Task<string> Login(UserCredentialsModel loginUser)
        {
            return await this.repository.Login(loginUser);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>
        /// True if password is changed successfully
        /// </returns>
        public async Task<bool> ResetPassword(UserCredentialsModel userCredentials)
        {
            return await this.repository.ResetPassword(userCredentials);
        }

        /// <summary>
        /// Sends email for reset password.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>
        /// True if password reset link is mailed successfully
        /// </returns>
        public async Task<bool> ForgotPassword(string userEmail)
        {
            return await this.repository.ForgotPassword(userEmail);
        }

    }
}
