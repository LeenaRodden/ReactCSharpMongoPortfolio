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
    public class WebsiteModel
    {

        public int WebsiteId { get; set; }
        public string Url { get; set; } 
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<TechnologyModel> Technologies { get; set; }
        ///// <summary>
        ///// Url, Alt Text
        ///// </summary>
        //[Obsolete("Needed so JSX Project will work. Use ImageModels instead")]
        //public Dictionary<string,string> Images { get; set; }
        public List<ImageModel> ImageModels { get; set; }
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

        private const string collectionName = "website";

        public WebsiteModel(int websiteId, string url, string displayName, string description, List<TechnologyModel> technologies, List<ImageModel> images)
        {
            WebsiteId = websiteId;
            Url = url;
            DisplayName = displayName;
            Description = description;
            Technologies = technologies;
            ImageModels = images;
        }



        public WebsiteModel()
        {

        }


        public static List<WebsiteModel> GetList()
        {
            var client = new MongoClient(DAL.MongoConnectionString);
            var database = client.GetDatabase(DAL.MongoDB);
            var collection = database.GetCollection<WebsiteModel>(collectionName);
            var list = collection.AsQueryable().OrderByDescending(p => p.Order).ToList();
            foreach (var item in list)
            {
                if (item.ImageModels == null)
                {
                    item.ImageModels = new List<ImageModel>();
                }
            }
            return list;
        }

        internal void Insert()
        {
            if(Technologies == null)
            {
                Technologies = new List<TechnologyModel>();
            }
            Technologies = Technologies.OrderByDescending(p => p.Order).ToList();
            if (ImageModels == null)
            {
                ImageModels = new List<ImageModel>();   
            }
            
            var list = GetList();
            var maxID = list.Count > 0 ? list.Max(p=>p.WebsiteId) : 0;
            WebsiteId = maxID + 1;
            BsonDocument bson = this.ToBsonDocument();
            var db = DAL.GetDatabase();
            var col = db.GetCollection<BsonDocument>(collectionName);
            col.InsertOne(bson);
        }


        internal void Update()
        {
            if (Technologies == null)
            {
                Technologies = new List<TechnologyModel>();
            }
            Technologies = Technologies.OrderByDescending(p => p.Order).ToList();
            if (ImageModels == null)
            {
                ImageModels = new List<ImageModel>();
            }
            var db = DAL.GetDatabase();
            var updateFilter = Builders<WebsiteModel>.Filter.Eq("WebsiteId", WebsiteId);
            var collection = db.GetCollection<WebsiteModel>(collectionName);

            collection.ReplaceOne(updateFilter, this);
        }

        internal static void Delete(int id)
        {
            var db = DAL.GetDatabase();
            var deleteFilter = Builders<WebsiteModel>.Filter.Eq("WebsiteId", id);
            var collection = db.GetCollection<WebsiteModel>(collectionName);
            collection.DeleteOne(deleteFilter);
        }
    }
}