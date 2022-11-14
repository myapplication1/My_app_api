using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Buku.Models
{
    public class table
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string tableNumber { get; set; }
        public string eventID { get; set; }
        public List<seat> SeatList { get; set; }

    }
    public class seat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string tableID { get; set; }
        public string SeatNumber { get; set; }

        public string EventID { get; set; }
        public string GuestNumber { get; set; }

       // public List<guests> GuestList { get; set; }

    }

}
