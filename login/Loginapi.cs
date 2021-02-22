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

                foreach(BsonDocument d in doc)
                {
                    if (d["name"] == user)
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
    }
}
