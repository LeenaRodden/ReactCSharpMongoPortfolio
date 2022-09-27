using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class SettingsModel
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }



        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string SummaryText { get; set; }


        private const string collectionName = "settings";

        public SettingsModel()
        {

        }

        public static SettingsModel GetSettings()
        {
            var list = GetList();
            var model = new SettingsModel();
            if (list != null && list.Count > 0)
            {
                model._id = list[0]._id;
                model.Name = list[0].Name;
                model.Description = list[0].Description;
                model.PhoneNumber = list[0].PhoneNumber;
                model.SummaryText = list[0].SummaryText;

            }

            return model;
        }

        private static List<SettingsModel> GetList()
        {
            IMongoDatabase database = DAL.GetDatabase();
            var collection = database.GetCollection<SettingsModel>(collectionName);
            return collection.AsQueryable().OrderBy(p=>p.Name).ToList();
        }

        public void Update()
        {
            var db = DAL.GetDatabase();
            var updateFilter = Builders<SettingsModel>.Filter.Eq("_id", _id);
            var collection = db.GetCollection<SettingsModel>(collectionName);
            if(collection != null && collection.AsQueryable().Count() > 0)
            {
                collection.ReplaceOne(updateFilter, this);
            }
            else
            {
                collection.InsertOne(this);
            }

        }

    }
}