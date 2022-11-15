using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Buku.Models
{
    public class BankCards
    {
        public string Id { get; set; }
        public string BankName { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public DateTime Expiry { get; set; }
        public string CardName { get; set; }
        public String img { get; set; }

    }

}
