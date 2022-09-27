using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class DAL
    {
        public static string MongoConnectionString = ConfigurationManager.ConnectionStrings["MongoConnectionString"].ConnectionString;
        public static string MongoDB = ConfigurationManager.AppSettings["MongoDB"];
        public static IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(MongoConnectionString);
            var database = client.GetDatabase(MongoDB);
            return database;
        }
    }
}