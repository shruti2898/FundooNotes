using FundooModels;
using FundooManager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace FundooNotes.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        private readonly ILogger<UserController> logger;
        public IConfiguration configuration { get; }
        public UserController(IUserManager manager, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            try
            {
                this.logger.LogInformation($"Registering user for email - {user.Email}");
                RegisterModel data = await this.manager.Register(user);
                if (data != null)
                {
                    this.logger.LogInformation($"New user registered successfully for email - {user.Email}");
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "Registered Succesfully", Data = data });
                }
                else
                {
                    this.logger.LogInformation($"Registration failed for email - {user.Email}");
                    return this.BadRequest(new { Status = false, Message = "Email already exist" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Exception : {ex.Message}");
                return this.NotFound(new { Status = true, ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                this.logger.LogInformation($"Logging in as {userCredentials.UserEmail}");
                RegisterModel result = await this.manager.Login(userCredentials);

                if (result != null)
                {
                    ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configuration["RedisServer"]);
                    IDatabase database = multiplexer.GetDatabase();
                    int id = Convert.ToInt32(database.StringGet("User ID"));
                    string firstName = database.StringGet("First Name");
                    string lastName = database.StringGet("Last Name");
                    RegisterModel loginData = new RegisterModel

                    {
                        FirstName = firstName,
                        LastName = lastName,
                        UserId = id,
                        Email = userCredentials.UserEmail
                    };
                    string tokenJWT = this.manager.GenrateJwtToken(loginData.Email);

                    this.logger.LogInformation($"Logged in as {userCredentials.UserEmail}");
                    return this.Ok(new { Status = true, Message = "Logged in successfully", Data = loginData, Token = tokenJWT });

                }
                else
                {
                    this.logger.LogInformation($"Unable to log in as {userCredentials.UserEmail}");
                    return this.BadRequest(new { Status = false, Message = "You have entered incorrect email address or incorrect password. Please try again!" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Exception : {ex.Message}");
                return this.NotFound(new { Status = true, ex.Message });
            }
        }

        [HttpPut]
        [Route("api/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                this.logger.LogInformation($"Request for password reset from {userCredentials.UserEmail}");
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
            catch (Exception ex)
            {
                this.logger.LogWarning($"Exception : {ex.Message}");
                return this.NotFound(new { Status = true, ex.Message });
            }
        }

        [HttpPost]
        [Route("api/forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string userEmail)
        {

            try
            {
                this.logger.LogInformation($"{userEmail} is requesting to send forgot password link");
                var resultForgotPassword = await this.manager.ForgotPassword(userEmail);
                if (resultForgotPassword)
                {
                    this.logger.LogInformation($"Forgot password link sent on {userEmail}");
                    return this.Ok(new { Status = true, Message = $"A link has been sent on your email address- {userEmail}" });
                }
                else
                {
                    this.logger.LogInformation($"{userEmail} not found in database");
                    return this.BadRequest(new { Status = false, Message = userEmail + " email does not exist in our system." });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"Exception : {ex.Message}");
                return this.NotFound(new { Status = true, ex.Message });
            }
        }
    }
}
