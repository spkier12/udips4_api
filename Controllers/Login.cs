using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using udips4_api.login;
using udips4_api;

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
        [HttpPost("create/key/{token}/{user}")]
        public bool Newuser(string token, string user)
        {
            return true;
        }

        // Get user profil
        [HttpGet("get/key/{token}/{user}")]
        public bool Profile(string token, string user)
        {
            return false;
        }
    }
}
