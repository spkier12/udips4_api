using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using udips4_api;
using udips4_api.journal;


namespace udips4_api.Controllers

{

    public class UserModel
    {
        public string UserUpdateText { get; set; }
    }



    [Route("api/[controller]")]
    [ApiController]
    public class journals : ControllerBase
    {
        public string UpdateData { get; set; }

        // Create new journal
        [HttpPost("create/{token}/{journalname}/{birthdate}/{blood}")]
        public bool CreateJournal(string token, string journalname, string birthdate, string bloodtype)
        {
            VerifyToken getref = new VerifyToken();
            Journals getref2 = new Journals();

            var checktoken = getref.Verify(token);
            if (checktoken == "false") return false;

            return getref2.CreateUser(journalname, birthdate, bloodtype);
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

        // Update the journal of the person as a post request
        [HttpPost("updatejournal/{token}/{id}")]
        public void Update(string token, string id, [FromBody] UserModel da)
        {
            VerifyToken getref = new VerifyToken();
            Journals getref2 = new Journals();
            var verify = getref.Verify(token);
            if (verify == "false")
            {
            } else
            {
                getref2.UpdateJournal(da.UserUpdateText, id);
            }

        }
    }
}
