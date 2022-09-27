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
    public class EducationModel
    {
        public int EducationId { get; set; }
        public string GraduationDate { get; set; }
        public string DegreeText { get; set; }
        public string University { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
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

        private const string collectionName = "education";

        public EducationModel(int educationId, DateTime graduationDate, string degreeText, string university, string location, string notes)
        {
            EducationId = educationId;
            GraduationDate = graduationDate.ToString("MMMM yyyy");
            DegreeText = degreeText;
            University = university;
            Location = location;
            Notes = notes;
        }
        public EducationModel()
        {

        }

        public static List<EducationModel> GetList()
        {
            IMongoDatabase database = DAL.GetDatabase();
            var collection = database.GetCollection<EducationModel>(collectionName);
            return collection.AsQueryable().OrderByDescending(p=>p.Order).ToList();
        }

        internal void Insert()
        {

            var list = GetList();
            var maxID = list.Count > 0 ? list.Max(p => p.EducationId) : 0;
            EducationId = maxID + 1;
            BsonDocument bson = this.ToBsonDocument();
            var db = DAL.GetDatabase();
            var col = db.GetCollection<BsonDocument>(collectionName);
            col.InsertOne(bson);
        }





        internal void Update()
        {
            var db = DAL.GetDatabase();
            var updateFilter = Builders<EducationModel>.Filter.Eq("EducationId", EducationId);
            var collection = db.GetCollection<EducationModel>(collectionName);
            collection.ReplaceOne(updateFilter, this);
        }



        internal static void Delete(int id)
        {
            var db = DAL.GetDatabase();
            var deleteFilter = Builders<EducationModel>.Filter.Eq("EducationId", id);
            var collection = db.GetCollection<EducationModel>(collectionName);
            collection.DeleteOne(deleteFilter);
        }


    }
}