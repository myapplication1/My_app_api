using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Buku.Models
{
    public class Expense
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string DateEntered { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string email { get; set; }
        
        
    }
   
}



