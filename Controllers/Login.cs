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
            var checklogin = getapi.Loginuser(user, pass);
            if (checklogin == "Error 404")
            {
                return "Brukernavn / passord er feil!";
            } else
            {
                return checklogin;
            }
        }

        // Create a new user in the database
        [HttpPost("create/key/{token}/{user}/{pass}/{role}")]
        public string Newuser(string token, string user, string pass, string role)
        {
            VerifyToken checktoken = new VerifyToken();
            Loginapi makelogin = new Loginapi();

            // Check if it contains owner or admin else return false beacuse it can't be created if token is not valid
            var check = checktoken.Verify(token);
            if (check.Contains("admin"))
            {
                return makelogin.Createuser(role, user, pass).ToString(); ;
            } else
            {
                return $"Could not make account Wrong permissions he had: {check}";
            }
        }

        // Get user profil
        [HttpGet("get/key/{token}/{user}")]
        public bool Profile(string token, string user)
        {
            return false;
        }
    }
}
