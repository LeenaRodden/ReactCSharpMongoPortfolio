using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace API.Models
{
    [BsonIgnoreExtraElements]
    public class TechnologyModel
    {



        public int TechnologyId { get; set; }
        public string TechnologyName { get; set; } 
        /// <summary>
        /// Front End, Server, Database, Other etc
        /// </summary>
        public string TechnologyType { get; set; }
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
        private const string collectionName = "technology";

        public TechnologyModel(int technologyId, string technologyName, string technologyType)
        {
            TechnologyId = technologyId;
            TechnologyName = technologyName;
            TechnologyType = technologyType;
        }

        public TechnologyModel()
        {

        }

        public static List<TechnologyModel> GetList()
        {
            var client = new MongoClient(DAL.MongoConnectionString);
            var database = client.GetDatabase(DAL.MongoDB);
            var collection = database.GetCollection<TechnologyModel>(collectionName);
            return collection.AsQueryable().OrderByDescending(p=>p.Order).ToList();
        }

        internal void Insert()
        {
            var list = GetList();
            var maxID = list.Count > 0 ? list.Max(p => p.TechnologyId) : 0;
            TechnologyId = maxID + 1;
            BsonDocument bson = this.ToBsonDocument();
            var db = DAL.GetDatabase();
            var col = db.GetCollection<BsonDocument>(collectionName);
            col.InsertOne(bson);
        }



        internal void Update()
        {
            var db = DAL.GetDatabase();
            var updateFilter = Builders<TechnologyModel>.Filter.Eq("TechnologyId", TechnologyId);
            var collection = db.GetCollection<TechnologyModel>(collectionName);
            collection.ReplaceOne(updateFilter, this);
        }

        internal static void Delete(int id)
        {
            var db = DAL.GetDatabase();
            var deleteFilter = Builders<TechnologyModel>.Filter.Eq("TechnologyId", id);
            var collection = db.GetCollection<TechnologyModel>(collectionName);
            collection.DeleteOne(deleteFilter);
        }
    }
}