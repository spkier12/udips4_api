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
                var client = new MongoClient("mongodb://23.94.134.205");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("login");
                var doc = col.Find(new BsonDocument()).ToList();

                var alldocs = "false";

                foreach(BsonDocument d in doc)
                {
                    if (d["token"] == token)
                    {
                        System.Diagnostics.Debug.WriteLine($"{d["token"]} We found a match!!");
                        return $"User: {d["name"]} has {d["role"]} role";
                    }
                }

                return alldocs;

            } catch
            {
                return "false";
            }
        }
    }
}
