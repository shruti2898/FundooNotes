using FundooModels;
using FundooManager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            try
            {
                RegisterModel data = await this.manager.Register(user);
                if(data!=null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>{ Status = true, Message = "Registered Succesfully" ,Data = data});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Email already exist" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = true, ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                RegisterModel result = await this.manager.Login(userCredentials);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>{ Status = true, Message = "Logged in successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "You have entered incorrect email address or incorrect password. Please try again!"});
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = true, ex.Message });
            }
        }

        [HttpPut]
        [Route("api/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                var resultReset = await this.manager.ResetPassword(userCredentials);
                if (resultReset)
                {
                    return this.Ok(new ResponseModel<UserCredentialsModel> { Status = true, Message = "Password changed successfully", Data = userCredentials });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "You have entered incorrect email address or incorrect password. Please try again!" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = true, ex.Message });
            }
        }

        [HttpPost]
        [Route("api/forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string userEmail)
        {

            try
            {
                var resultForgotPassword = await this.manager.ForgotPassword(userEmail);
                if (resultForgotPassword)
                {
                    return this.Ok(new { Status = true, Message = $"A link has been sent on your email address- {userEmail}"});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = userEmail + " email does not exist in our system." });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = true, ex.Message });
            }
        }
    }
}
