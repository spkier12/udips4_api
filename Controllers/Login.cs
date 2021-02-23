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

        // Change password on account
        [HttpPost("update/key/{token}/{pass}")]
        public bool Updatepass(string token, string pass)
        {
            try
            {
                VerifyToken checktoken = new VerifyToken();
                Loginapi getfunccall = new Loginapi();
                var verify = checktoken.Verify(token);
                
                // If it's false then our account dosn't exist and therefor you can't update a password for a non existing account
                if (verify == "false")
                {
                    return false;
                }
                return getfunccall.Changepassword(token, pass);

            } 
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        // Get user profil
        [HttpGet("get/key/{token}")]
        public string[] Profile(string token)
        {
            Loginapi getref = new Loginapi();
            return getref.GetProfile(token);
        }

        // Check if token is valid
        [HttpGet("valid/{token}")]
        public bool Valid(string token)
        {
            VerifyToken getref = new VerifyToken();
            var check = getref.Verify(token);
            if (check == "false")
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
