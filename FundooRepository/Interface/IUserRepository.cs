// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooRepository.Interface
{
    using System.Threading.Tasks;
    using FundooModels;

    /// <summary>
    /// User Repository Interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>User data after successful registration</returns>
        Task<RegisterModel> Register(RegisterModel user);

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="loginUser">The login user.</param>
        /// <returns>User data after logging in successfully</returns>
        Task<RegisterModel> Login(UserCredentialsModel loginUser);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>True if password is changed successfully else false</returns>
        Task<bool> ResetPassword(UserCredentialsModel userCredentials);

        /// <summary>
        /// Sends email for reset password.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>True if password reset link is mailed successfully else false</returns>
        Task<bool> ForgotPassword(string userEmail);

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Token string</returns>
        string GenerateJwtToken(string email);
    }
}
