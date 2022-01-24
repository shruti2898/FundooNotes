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
    using FundooRepository.CustomException;
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
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            try
            {
                this.logger.LogInformation($"Registering user for email - {user.Email}");
                RegisterModel data = await this.manager.Register(user);
                if (data != null)
                {
                    HttpContext.Session.SetString("Email", user.Email);
                    string sessionEmail = HttpContext.Session.GetString("Email");
                    this.logger.LogInformation($"New user registered successfully for email - {user.Email}");
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "Registered Succesfully", Data = data });
                }
                else
                {
                    this.logger.LogInformation($"Registration failed for email - {user.Email}");
                    return this.BadRequest(new { Status = false, Message = "Email already exist" });
                }
            }
            catch (CustomException ex)
            {
                return new ObjectResult(new { Status = false, Message = ex.Message }) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Logins the specified user credentials.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>
        /// Ok object result if user logs in successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                this.logger.LogInformation($"Logging in as {userCredentials.UserEmail}");
                string result = await this.manager.Login(userCredentials);

                if (result != null)
                {
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
                else
                {
                    this.logger.LogInformation($"Unable to log in as {userCredentials.UserEmail}");
                    return this.BadRequest(new { Status = false, Message = "You have entered incorrect email address or incorrect password. Please try again!" });
                }
            }
            catch (CustomException ex)
            {
                return new ObjectResult(new { Status = false, Message = ex.Message }) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>
        /// Ok object result if password is changed successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpPut]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                var resultReset = await this.manager.ResetPassword(userCredentials);
                if (resultReset)
                {
                    this.logger.LogInformation($"Password changed for {userCredentials.UserEmail}");
                    return this.Ok(new ResponseModel<UserCredentialsModel> { Status = true, Message = "Password changed successfully", Data = userCredentials });
                }
                else
                {
                    this.logger.LogInformation($"Unable to change password for {userCredentials.UserEmail}");
                    return this.BadRequest(new { Status = false, Message = $"{userCredentials.UserEmail} email address does not exist in our system. Please try again!" });
                }
            }
            catch (CustomException ex)
            {
                return new ObjectResult(new { Status = false, Message = ex.Message }) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Sends email for reset password.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>
        /// Ok object result if password reset link is mailed successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string userEmail)
        {
            try
            {
                this.logger.LogInformation($"{userEmail} is requesting to send forgot password link");
                var resultForgotPassword = await this.manager.ForgotPassword(userEmail);
                if (resultForgotPassword)
                {
                    this.logger.LogInformation($"Forgot password link sent on {userEmail}");
                    return this.Ok(new { Status = true, Message = "Link for reset password has been sent on your email" });
                }
                else
                {
                    this.logger.LogInformation($"{userEmail} not found in database");
                    return this.BadRequest(new { Status = false, Message = "Email does not exist in our system." });
                }
            }
            catch (CustomException ex)
            {
                return new ObjectResult(new { Status = false, Message = ex.Message }) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
