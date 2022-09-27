using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace API.Models
{
    [BsonIgnoreExtraElements]
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }


        private const string collectionName = "user";

        public UserModel()
        {

        }

        public static List<UserModel> GetList()
        {
            IMongoDatabase database = DAL.GetDatabase();
            var collection = database.GetCollection<UserModel>(collectionName);
            return collection.AsQueryable().ToList();
        }

        internal void Insert()
        {

            var list = GetList();
            var maxID = list.Count > 0 ? list.Max(p => p.UserId) : 0;
            UserId = maxID + 1;
            BsonDocument bson = this.ToBsonDocument();
            var db = DAL.GetDatabase();
            var col = db.GetCollection<BsonDocument>(collectionName);
            col.InsertOne(bson);
        }





        internal void Update()
        {
            var db = DAL.GetDatabase();
            var updateFilter = Builders<UserModel>.Filter.Eq("UserId", UserId);
            var collection = db.GetCollection<UserModel>(collectionName);
            collection.ReplaceOne(updateFilter, this);
        }



        internal static void Delete(int id)
        {
            var db = DAL.GetDatabase();
            var deleteFilter = Builders<UserModel>.Filter.Eq("UserId", id);
            var collection = db.GetCollection<UserModel>(collectionName);
            collection.DeleteOne(deleteFilter);
        }

        public static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public static string ComputeHash(byte[] bytesToHash, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }


    }
}