using Api.Buku.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Buku.Services
{
    public class IncomeService
    {

        private readonly IMongoCollection<Income> _income;

        public IncomeService(IBukuDatabaseSettings settings)
        {

            settings.BukuCollectionName = "Income";
            var settings_ = MongoClientSettings
                .FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settings_);
            var database = client.GetDatabase(settings.DatabaseName);
            _income = database.GetCollection<Income>(settings.BukuCollectionName);


            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);

            // _users = database.GetCollection<Users>(settings.BukuCollectionName);
        }


        public async Task<List<Income>> Get() =>
          await _income.Find(Income => true).ToListAsync();


        public decimal GetTotal()
        {
            var  result = _income.Find(Income => true).ToList().Sum(x => x.Amount);
            return result;
        }

         public decimal GetTotalByEmail(string email)
        {
            var  result = _income.Find(x => x.email== email).ToList().Sum(x => x.Amount);
            return result;
        }

        public async Task<Income> Get(string id) =>
        await _income.Find(users => users.Id == id).FirstOrDefaultAsync();

        public async Task<List<Income>> GetAll(string id) =>
           await _income.Find(users => users.email == id).ToListAsync();

        public async Task<Income> Create(Income Income)
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

        public async void Update(string id, Income Income) =>
           await  _income.ReplaceOneAsync(Income => Income.Id == id, Income);

        public async void Remove(Income Income) =>
           await _income.DeleteOneAsync(Income => Income.Id == Income.Id);

        public async void Remove(string id) =>
           await _income.DeleteOneAsync(Income => Income.Id == id);
    }
}

