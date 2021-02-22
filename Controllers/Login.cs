using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using udips4_api.login;

namespace udips4_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        // Login the user
        [HttpPost("check/{user}/{pass}")]
        public string Logincheck(string user, string pass)
        {
            Loginapi getapi = new Loginapi();
            return getapi.test0();
        }

        // Create a new user in the database
        [HttpPost("create/key/{user}")]
        public bool Newuser(string user)
        {
            return true;
        }

        // Get user profil
        [HttpGet("get/key/{user}")]
        public bool Profile(string user)
        {
            return false;
        }
    }
}
