using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace udips4_api.journal
{
    public class Journals
    {

        // Create a new journal in database
        public bool CreateUser(string name, string birthdate)
        {
            try
            {
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("journals");
                var newuser = new BsonDocument 
                {
                    { "name", name },
                    { "birth", birthdate },
                    { "incident", "" }
                };

                col.InsertOne(newuser);


                return true;
            } 
            catch
            {
                return false;
            }
        }

        // Get all journals in database
        public string GetJournals()
        {
            try
            {
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("journals");
                var getdocs = col.Find(new BsonDocument()).ToList();
                string All = null;

                foreach(BsonDocument d in getdocs)
                {
                    All += d["name"] + "\n";
                }

                return All;
            } 
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "Kunne ikke finne noen i database...";
            }
        }

    }
}
