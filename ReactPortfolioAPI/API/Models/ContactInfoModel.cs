using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace API.Models
{
    [BsonIgnoreExtraElements]
    public class ContactInfoModel
    {
        public int ContactInfoId { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
        public bool DisplayOnHome { get; set; }
        

        private int? _order;
        [ScriptIgnore]
        public int? Order
        {
            get
            {
                if (_order == null)
                {
                    _order = 0;
                }
                return _order;
            }
            set
            {
                _order = value;
            }
        }

        private const string collectionName = "contactInfo";

        public ContactInfoModel(int contactInfoId, string type, string details)
        {
            ContactInfoId = contactInfoId;
            Type = type;
            Details = details;
        }
        public ContactInfoModel()
        {

        }

        public static List<ContactInfoModel> GetList()
        {
            IMongoDatabase database = DAL.GetDatabase();
            var collection = database.GetCollection<ContactInfoModel>(collectionName);
            return collection.AsQueryable().OrderByDescending(p=>p.Order).ToList();
        }

        internal void Insert()
        {

            var list = GetList();
            var maxID = list.Count > 0 ? list.Max(p => p.ContactInfoId) : 0;
            ContactInfoId = maxID + 1;
            BsonDocument bson = this.ToBsonDocument();
            var db = DAL.GetDatabase();
            var col = db.GetCollection<BsonDocument>(collectionName);
            col.InsertOne(bson);
        }





        internal void Update()
        {
            var db = DAL.GetDatabase();
            var updateFilter = Builders<ContactInfoModel>.Filter.Eq("ContactInfoId", ContactInfoId);
            var collection = db.GetCollection<ContactInfoModel>(collectionName);
            collection.ReplaceOne(updateFilter, this);
        }



        internal static void Delete(int id)
        {
            var db = DAL.GetDatabase();
            var deleteFilter = Builders<ContactInfoModel>.Filter.Eq("ContactInfoId", id);
            var collection = db.GetCollection<ContactInfoModel>(collectionName);
            collection.DeleteOne(deleteFilter);
        }


    }
}