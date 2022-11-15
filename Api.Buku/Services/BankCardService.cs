using Api.Buku.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Buku.Services
{
    public class BankCardService
    {

        private readonly IMongoCollection<BankCards> _income;

        public BankCardService(IBukuDatabaseSettings settings)
        {

            settings.BukuCollectionName = "BankCards";
            var settings_ = MongoClientSettings
                .FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settings_);
            var database = client.GetDatabase(settings.DatabaseName);
            _income = database.GetCollection<BankCards>(settings.BukuCollectionName);


            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);

            // _users = database.GetCollection<Users>(settings.BukuCollectionName);
        }


        public async Task<List<BankCards>> Get() =>
          await _income.Find(Income => true).ToListAsync();


        public decimal GetTotal()
        {
            var  result = _income.Find(Income => true).ToList().Sum(x => x.Amount);
            return result;
        }
       
        
        public async Task<BankCards> Get(string id) =>
           await _income.Find<BankCards>(users => users.Id == id).FirstOrDefaultAsync();

        public async Task<BankCards> Create(BankCards Income)
        {
            await _income.InsertOneAsync(Income);
            return Income;
        }


        //public async Task<Users> AuthLogin(UserLogin users)
        //{
        //    Users UserResults = null;
        //    try
        //    {

        //      ////  var query =
        //      ////_users.AsQueryable<Users>()
        //      ////  .Where(e => e.EmailAddress == "amoahusa@gmail.com" && e.Password == "111").FirstOrDefaultAsync();
        //      //////  .Select(e => e); // this trivial projection is optional when using lambda syntax

        //      ////  _users.fi

        //        UserResults = await _users
        //      .Find<Users>(x => x.EmailAddress == users.Email
        //        && x.Password == users.Password).FirstOrDefaultAsync();

        //    }
        //    catch (Exception E)
        //    {

        //    }
        //    return UserResults;


        //}

        public async void Update(string id, BankCards Income) =>
           await  _income.ReplaceOneAsync(Income => Income.Id == id, Income);

        public async void Remove(BankCards Income) =>
           await _income.DeleteOneAsync(Income => Income.Id == Income.Id);

        public async void Remove(string id) =>
           await _income.DeleteOneAsync(Income => Income.Id == id);
    }
}

