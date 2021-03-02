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
        public bool CreateUser(string name, string birthdate, string bloodtype)
        {
            try
            {
                var client = new MongoClient("mongodb://ub:ly68824ub@23.94.134.205:10000");
                var db = client.GetDatabase("udips4");
                var col = db.GetCollection<BsonDocument>("journals");
                var time = DateTime.Now;

                var newuser = new BsonDocument
                {
                    { "Added", time },
                    { "name", name },
                    { "birth", birthdate },
                    { "blodtype", bloodtype },
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
                var client = new MongoClient("mongodb://ub:ly68824ub@23.94.134.205:10000");
                var db = client.GetDatabase("udips4");
                var col = db.GetCollection<BsonDocument>("journals");
                var getdocs = col.Find(new BsonDocument()).ToList();
                string All = null;

                foreach(BsonDocument d in getdocs)
                {
                    All += $"<p class='form-select'><br>Navn: {d["name"]}<br> Lagt til: {d["Added"]} <br> Blodtype: {d["blodtype"]} <br> FødselsDato: {d["birth"]}<br></p>";
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
                var client = new MongoClient("mongodb://ub:ly68824ub@23.94.134.205:10000");
                var db = client.GetDatabase("udips4");
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
                var client = new MongoClient("mongodb://ub:ly68824ub@23.94.134.205:10000");
                var db = client.GetDatabase("udips4");
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
                var client = new MongoClient("mongodb://ub:ly68824ub@23.94.134.205:10000");
                var db = client.GetDatabase("udips4");
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
                            { "blodtype", d["blodtype"] },
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

        // Update journal with new bloodtype
        public bool UpdateBloodTypeJournal(string id, string bloodtype)
        {
            try
            {
                var client = new MongoClient("mongodb://ub:ly68824ub@23.94.134.205:10000");
                var db = client.GetDatabase("udips4");
                var col = db.GetCollection<BsonDocument>("journals");
                var doc = col.Find(new BsonDocument()).ToList();

                foreach(BsonDocument d in doc)
                {
                    if (d["name"] == id)
                    {
                        var olddoc = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(d["_id"].ToString()));
                        var newdoc = new BsonDocument 
                        {
                            { "Added", d["Added"] },
                            { "name", d["name"] },
                            { "birth", d["birth"] },
                            { "blodtype", bloodtype },
                            { "incident", d["incident"]}
                        };

                        col.ReplaceOne(olddoc, newdoc);
                        return true;
                    }
                }

                return false;
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return false;
            }
        }

    }
}
