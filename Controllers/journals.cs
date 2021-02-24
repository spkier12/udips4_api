using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using udips4_api;
using udips4_api.journal;


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
        [HttpPost("create/{token}/{journalname}/{birthdate}")]
        public bool CreateJournal(string token, string journalname, string birthdate)
        {
            VerifyToken getref = new VerifyToken();
            Journals getref2 = new Journals();

            var checktoken = getref.Verify(token);
            if (checktoken == "false") return false;

            return getref2.CreateUser(journalname, birthdate);
        }

        // Get All journals
        [HttpGet("GetAll/{token}")]
        public string UpdateJournal(string token)
        {
            Journals getref = new Journals();
            VerifyToken getref2 = new VerifyToken();
            var checktoken = getref2.Verify(token);

            // Check if token is valid or not
            if (checktoken == "false") return "";

            return getref.GetJournals();
        }
    }
}
