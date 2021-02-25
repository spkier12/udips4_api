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

        // Get All journals including time it was created
        [HttpGet("GetAll/{token}")]
        public string GetJournals(string token)
        {
            Journals getref = new Journals();
            VerifyToken getref2 = new VerifyToken();
            var checktoken = getref2.Verify(token);

            // Check if token is valid or not
            if (checktoken == "false") return "";

            return getref.GetJournalsName();
        }

        // Get All journals name exincluding time it was created
        [HttpGet("GetAllName/{token}")]
        public string GetJournals2(string token)
        {
            Journals getref = new Journals();
            VerifyToken getref2 = new VerifyToken();
            var checktoken = getref2.Verify(token);

            // Check if token is valid or not
            if (checktoken == "false") return "";

            return getref.GetJournalsName2();
        }

        // Get incident report on journal
        [HttpGet("incident/key/{token}/{id}")]
        public string GetIncident(string token, string id)
        {
            Journals getref = new Journals();
            VerifyToken getref2 = new VerifyToken();
            var checktoken = getref2.Verify(token);
            if (checktoken == "false") return "";

            return getref.GetJournalIncident(id);
        }
    }
}
