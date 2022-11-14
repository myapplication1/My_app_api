using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Buku.Models
{
    public class guests
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string CellNumber { get; set; }
        public string Table { get; set; }
        public string Seat { get; set; }

    }


}
