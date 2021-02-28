using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Timers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace udips4_api
{
    public class ChangeToken
    {
        public void StartTokenTimer()
        {
            System.Diagnostics.Debug.WriteLine("Reseting token every 2 hour");
            Thread.Sleep(120*60*1000);
            DailyChangeToken();
        }

        public void DailyChangeToken()
        {
            System.Diagnostics.Debug.WriteLine("Starting reset token function call!");
            try
            {
                // Connect to database
                var client = new MongoClient("mongodb://23.94.134.205");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("login");
                var doc = col.Find(new BsonDocument()).ToList();

                foreach(BsonDocument d in doc)
                {
                    // Create new token from old token
                    var olddoc = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(d["_id"].ToString()));
                    string newtoken = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(d["token"].ToString())))
                        .Replace("+", "").Replace("/", "").Replace("!", "").Replace("#", "").Replace("¤", "")
                        .Replace("%", "").Replace("&", "").Replace("(", "").Replace(")", "").Replace("=", "");

                    // New document to be replaced with
                    BsonDocument newdoc = new BsonDocument 
                        {
                            { "name", d["name"] },
                            { "pass", d["pass"]},
                            { "token", newtoken },
                            { "role", d["role"] },
                            { "pic", d["pic"] }
                        };

                    col.ReplaceOne(olddoc, newdoc);
                }
                // Let's put the timer on repeat
                StartTokenTimer();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {e}");
                StartTokenTimer();
            }
        }
    }
}
