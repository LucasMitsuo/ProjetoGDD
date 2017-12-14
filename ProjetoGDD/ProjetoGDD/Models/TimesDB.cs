using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoGDD.Models
{
    public class TimesDB
    {
        private readonly String connectionString = "mongodb://localhost";
        private readonly String DataBaseName = "projetogdd";
        public MongoDatabase Database;

        public TimesDB()
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();

            Database = server.GetDatabase(DataBaseName);
        }

        public MongoCollection<Time> Times
        {
            get
            {
                var times = Database.GetCollection<Time>("Times");

                return times;
            }
        }
    }
}