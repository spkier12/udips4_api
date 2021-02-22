using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using udips4_api;


namespace udips4_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class journals : ControllerBase
    {
        // Get journal of user
        [HttpGet("profile/{token}/{journalid}")]
        public bool GetJournal(string token, string journalid)
        {
            return false;
        }

        // Create new journal
        [HttpPost("create/{token}/{journalname}")]
        public bool CreateJournal(string token, string journalname)
        {
            return false;
        }

        // Update journal
        [HttpPost("update/{token}/{journalname}")]
        public bool UpdateJournal(string token, string journalname)
        {
            return false;
        }
    }
}
