using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
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
                var time = DateTime.Now;

                var newuser = new BsonDocument
                {
                    { "Added", time },
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
        public string GetJournalsName()
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
                    All += "<br>" + d["Added"].ToString().Split(":")[0]  + "|" + d["name"].ToString() + "<br>";
                }
                return All;
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "Kunne ikke finne noen i database...";
            }
        }

        public string GetJournalsName2()
        {
            try
            {
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("journals");
                var getdocs = col.Find(new BsonDocument()).ToList();
                string All = null;

                foreach (BsonDocument d in getdocs)
                {
                    All += "<br>" + d["name"].ToString() + "<br>";
                }
                return All;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "Kunne ikke finne noen i database...";
            }
        }

        // Get Incident reports on journal profiles
        public string GetJournalIncident(string id)
        {
            try
            {
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("journals");
                var doc = col.Find(new BsonDocument()).ToList();

                foreach(BsonDocument d in doc)
                {
                    if (d["name"] == id)
                    {
                        return d["incident"].ToString();
                    }
                }

                return "";
            } 
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return "";
            }
        }

        // Update journal content
        public void UpdateJournal(string incident, string id)
        {
            try
            {
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("journals");
                var doc = col.Find(new BsonDocument()).ToList();

                foreach(BsonDocument d in doc)
                {
                    if (d["name"] == id)
                    {
                        var olddoc = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(d["_id"].ToString()));

                        BsonDocument newdoc = new BsonDocument 
                        {
                            { "Added", d["Added"] },
                            { "name", d["name"] },
                            { "birth", d["birth"] },
                            { "incident", d["incident"] + "<br>" + incident + "<br>"}
                        };

                        // Let's replace the document with new
                        col.ReplaceOne(olddoc, newdoc);
                    }
                }

            } catch
            {
            }
        }

    }
}
