using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using udips4_api;
using udips4_api.admin;


namespace udips4_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class admin : ControllerBase
    {
        // Restart a type of service
        [HttpPost("restart/{token}/{service}")]
        public bool RestartService(string token, string service)
        {
            VerifyToken getref = new VerifyToken();
            Admin getref2 = new Admin();
            var checktoken = getref.Verify(token);
            if (checktoken == "owner")
            {
                getref2.Restartservice();
                return true;
            } else
            {
                return false;
            }
        }

        // Update settings
        [HttpPost("updatesettings/{token}")]
        public bool UpdateSettings(string token)
        {
            return false;
        }

        // Get the time when the api started
        [HttpGet("startup")]
        public string Startup()
        {
            Startup_Time getref = new Startup_Time();
            return getref.StartTimeOfApplication;
        }
    }
}
