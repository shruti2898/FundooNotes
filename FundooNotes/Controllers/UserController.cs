// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    
    /// <summary>
    /// Controller for User
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {  
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager manager;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="manager">The user manager</param>
        /// <param name="configuration">The configuration</param>
        /// <param name="logger">The logger</param>
        public UserController(IUserManager manager, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.Configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>
        /// Ok object result if user registration is successful
        /// </returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            this.logger.LogInformation($"Registering user for email - {user.Email}");
            RegisterModel data = await this.manager.Register(user);
              
            HttpContext.Session.SetString("Email", user.Email);
            string sessionEmail = HttpContext.Session.GetString("Email");
            this.logger.LogInformation($"New user registered successfully for email - {user.Email}");
            return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "Registered Succesfully", Data = data });
              
        }

        /// <summary>
        /// Logins the specified user credentials.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>
        /// Ok object result if user logs in successfully
        /// </returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsModel userCredentials)
        {
            this.logger.LogInformation($"Logging in as {userCredentials.UserEmail}");
            string result = await this.manager.Login(userCredentials);

            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(this.Configuration["RedisServer"]);
            IDatabase database = multiplexer.GetDatabase();
            int id = Convert.ToInt32(database.StringGet("User ID"));
            string firstName = database.StringGet("First Name");
            string lastName = database.StringGet("Last Name");
            var loginData = new
            {
                FirstName = firstName,
                LastName = lastName,
                UserId = id,
                Email = userCredentials.UserEmail
            };

            this.logger.LogInformation($"Logged in as {userCredentials.UserEmail}");
            return this.Ok(new { Status = true, Message = "Logged in successfully", Data = loginData, Token = result });
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>
        /// Ok object result if password is changed successfully
        /// </returns>
        [HttpPut]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserCredentialsModel userCredentials)
        {
            var resultReset = await this.manager.ResetPassword(userCredentials);
            this.logger.LogInformation($"Password changed for {userCredentials.UserEmail}");
            return this.Ok(new ResponseModel<UserCredentialsModel> { Status = true, Message = "Password changed successfully", Data = userCredentials });
        }

        /// <summary>
        /// Sends email for reset password.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>
        /// Ok object result if password reset link is mailed successfully
        /// </returns>
        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string userEmail)
        {
            this.logger.LogInformation($"{userEmail} is requesting to send forgot password link");
            var resultForgotPassword = await this.manager.ForgotPassword(userEmail);
                
            this.logger.LogInformation($"Forgot password link sent on {userEmail}");
            return this.Ok(new { Status = true, Message = "Link for reset password has been sent on your email" });
        }
    }
}
