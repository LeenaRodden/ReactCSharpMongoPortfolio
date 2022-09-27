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
    public class ExperienceModel
    {

        public int ExperienceId { get; set; }
        public string Dates { get; set; } 
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }

        private int? _order;
        [ScriptIgnore]
        public int? Order
        {
            get
            {
                if( _order == null)
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

        private const string collectionName = "experience";
        public List<string> BulletPoints { get; set; }

        public ExperienceModel(int experienceId, string dates, string jobTitle, string company, string location, string[] bulletPoints)
        {
            ExperienceId = experienceId;
            Dates = dates;
            JobTitle = jobTitle;
            Company = company;
            Location = location;
            BulletPoints = bulletPoints.ToList();
        }
        public ExperienceModel()
        {

        }

        public static List<ExperienceModel> GetList()
        {
            //return DefaultList();
            IMongoDatabase database = DAL.GetDatabase();
            var collection = database.GetCollection<ExperienceModel>(collectionName);


            return collection.AsQueryable().OrderByDescending(p=>p.Order).ToList();
        }

        internal void Insert()
        {
            if (BulletPoints == null)
            {
                BulletPoints = new List<string>();
            }
            var list = GetList();
            var maxID = list.Count > 0 ? list.Max(p => p.ExperienceId) : 0;
            ExperienceId = maxID + 1;
            BsonDocument bson = this.ToBsonDocument();
            var db = DAL.GetDatabase();
            var col = db.GetCollection<BsonDocument>(collectionName);
            col.InsertOne(bson);
        }


        internal void Update()
        {
            if (BulletPoints == null)
            {
                BulletPoints = new List<string>();
            }
            var db = DAL.GetDatabase();
            var updateFilter = Builders<ExperienceModel>.Filter.Eq("ExperienceId", ExperienceId);
            var collection = db.GetCollection<ExperienceModel>(collectionName);
            collection.ReplaceOne(updateFilter, this);
        }

        internal static void Delete(int id)
        {
            var db = DAL.GetDatabase();
            var deleteFilter = Builders<ExperienceModel>.Filter.Eq("ExperienceId", id);
            var collection = db.GetCollection<ExperienceModel>(collectionName);
            collection.DeleteOne(deleteFilter);
        }

    }
}