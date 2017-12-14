using MongoDB.Driver;
using System;

namespace ProjetoGDD.Models
{
    public class ReportesDB
    {
        private readonly String connectionString = "mongodb://localhost";
        private readonly String DataBaseName = "projetogdd";
        private readonly MongoDatabase Database;

        public ReportesDB()
        {
            var client = new MongoClient(connectionString);
            var server = client.GetServer();

            Database = server.GetDatabase(DataBaseName);
        }

        public MongoCollection<Reporte> Reportes
        {
            get
            {
                var reportes = Database.GetCollection<Reporte>("Reporte");

                return reportes;
            }
        }
    }
}