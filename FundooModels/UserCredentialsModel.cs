// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCredentialsModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// User Credentials Model Class
    /// </summary>
    public class UserCredentialsModel
    {
        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        /// <value>
        /// The user password.
        /// </value>
        public string UserPassword { get; set; }
    }
}
