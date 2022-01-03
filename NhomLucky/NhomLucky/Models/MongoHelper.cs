using MongoDB.Driver;
using NhomLucky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace do_an_ao_hoa.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoDatabase = "quanlysach";
        public static string MongoConnection = @"mongodb+srv://lucky:123@cluster0.mdklp.mongodb.net/myFirstDatabase?quanlysach=true&w=majority";
        public static IMongoCollection<Book> book_collection { get; set; }
        // You might need to replace & with &amp; in your Mongo Connection string stored in Web.Config
        internal static void ConnectToMongoService()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://lucky:123@cluster0.mdklp.mongodb.net/myFirstDatabase?quanlysach=true&w=majority");
            var client = new MongoClient(settings);
           database = client.GetDatabase("quanlysach");
        }
    }
}