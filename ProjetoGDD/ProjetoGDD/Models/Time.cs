using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoGDD.Models
{
    public class Time
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<string> Membros { get; set; }
    }
}