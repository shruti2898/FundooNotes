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
        public IActionResult Register([FromBody] RegisterModel user)
        {
            try
            {
                RegisterModel data =  this.manager.Register(user);
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
        public IActionResult Login([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                string result = this.manager.Login(userCredentials);
                if (result.Equals("Logged in successfully"))
                {
                    return this.Ok(new ResponseModel<UserCredentialsModel>{ Status = true, Message = result, Data = userCredentials });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = true, ex.Message });
            }
        }
    }
}
