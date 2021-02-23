using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;

namespace udips4_api.login
{
    public class Loginapi
    {
        // Login to the system and get our daily token
        public string Loginuser(string user, string pass)
        {
            try
            {
                var hashedpass = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass)));
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("login");
                var doc = col.Find(new BsonDocument()).ToList();

                // Loops thru the entire array ov bsondocuments until it find a match with both pass and name
                foreach(BsonDocument d in doc)
                {
                    if (d["name"].ToString().ToLower() == user.ToLower())
                    {
                        System.Diagnostics.Debug.WriteLine("We found a match for the user");
                        if (d["pass"] == hashedpass)
                        {
                            System.Diagnostics.Debug.WriteLine("We found your password");
                            return d["token"].ToString();
                        }
                    }
                }
                return "Error 404";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        // Create a new user in the database
        public string Createuser(string role, string user, string pass)
        {
            try
            {
                // Let's hash the password and username and bring a token!
                string hashedpass = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass)));
                string token = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass+user)))
                    .Replace("+", "").Replace("/", "").Replace("!", "").Replace("#", "").Replace("¤", "")
                    .Replace("%", "").Replace("&", "").Replace("(", "").Replace(")", "").Replace("=", "");

                var newuser = new BsonDocument 
                {
                    { "name", user },
                    { "pass", hashedpass},
                    { "token", token },
                    { "role", role }
                };

                // Let's connect and use the mongo database witch is totally mongo!
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("login");
                col.InsertOne(newuser);

                return $"Account sucesfully made: {newuser}";
            } catch(Exception e)
            {
                Console.WriteLine(e);
                return e.ToString();
            }
        }

        // Update current user with new password
        public bool Changepassword(string token, string newpassword)
        {
            try
            {
                // make new user password and hash it to sha 256 and convert it to a string and same with token
                // Token is a mix of the new hashed pass and the current token
                string newpass_hashed = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(newpassword)));
                string newtoken = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(token + newpass_hashed)))
                    .Replace("+", "").Replace("/", "").Replace("!", "").Replace("#", "").Replace("¤", "")
                    .Replace("%", "").Replace("&", "").Replace("(", "").Replace(")", "").Replace("=", "");

                // Try and connect to database
                var client = new MongoClient("mongodb://ulrik:ly68824@ubsky.xyz");
                var db = client.GetDatabase("login");
                var col = db.GetCollection<BsonDocument>("login");
                var doc = col.Find(new BsonDocument()).ToList();


                // Loop thru all accounts until you find the correct one!
                foreach(BsonDocument d in doc)
                {
                    if (d["token"] == token)
                    {
                        var olddoc = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(d["_id"].ToString()));
                        BsonDocument newdocument = new BsonDocument
                            {
                                { "name", d["name"] },
                                { "pass", newpass_hashed},
                                { "token", newtoken },
                                { "role", d["role"] }
                            };
                        col.ReplaceOne(olddoc, newdocument);
                        return true;
                    }
                }
                
                return false;

            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }
    }
}
