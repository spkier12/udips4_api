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
    public class journals : ControllerBase
    {
        // Get journal of user
        [HttpGet("profile/{journalid}")]
        public bool GetJournal(string journalid)
        {
            return false;
        }

        // Create new journal
        [HttpPost("create/{journalname}")]
        public bool CreateJournal(string journalname)
        {
            return false;
        }

        // Update journal
        [HttpPost("update/{journalname}")]
        public bool UpdateJournal(string journalname)
        {
            return false;
        }
    }
}
