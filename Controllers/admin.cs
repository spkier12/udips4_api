using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return true;
        }

        // Update settings
        [HttpPost("updatesettings/{token}")]
        public bool UpdateSettings(string token)
        {
            return false;
        }
    }
}
