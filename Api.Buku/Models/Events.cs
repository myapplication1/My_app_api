using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Buku.Models
{
    public class events
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string eventName { get; set; }
        public string desp { get; set; }
        public string eventDate { get; set; }
        public string venue { get; set; }
        public string Address { get; set; }
        public string city { get; set; }
        public string town { get; set; }
        public string zipcode { get; set; }
    }


}
