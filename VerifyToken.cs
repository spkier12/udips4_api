using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace udips4_api
{
    public class VerifyToken
    {
        public string Verify(string token)
        {
            try
            {
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("login");
                var doc = col.Find(new BsonDocument()).ToList();

                var alldocs = "";

                foreach(BsonDocument d in doc)
                {
                    if (d["token"] == token)
                    {
                        System.Diagnostics.Debug.WriteLine($"{d["token"]} We found a match!!");
                        return d["role"].ToString();
                    }
                    System.Diagnostics.Debug.WriteLine(d);
                    //alldocs += d;
                }

                return alldocs;

            } catch(Exception e)
            {
                return e.ToString();
            }
        }
    }
}
