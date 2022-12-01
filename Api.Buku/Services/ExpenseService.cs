using Api.Buku.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Buku.Services
{
    public class ExpenseService
    {

        private readonly IMongoCollection<Expense> _Expense;

        public ExpenseService(IBukuDatabaseSettings settings)
        {

            settings.BukuCollectionName = "Expense";
            var settings_ = MongoClientSettings
                .FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settings_);
            var database = client.GetDatabase(settings.DatabaseName);
            _Expense = database.GetCollection<Expense>(settings.BukuCollectionName);


            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);

            // _users = database.GetCollection<Users>(settings.BukuCollectionName);
        }


        public async Task<List<Expense>> Get() =>
          await _Expense.Find(Expense => true).ToListAsync();

        public decimal GetToTal() {
         return  _Expense.Find(Expense => true).ToList().Sum(x => x.Amount);
        }
           public decimal GetToTalByEmail(string email) {
         return  _Expense.Find(x => x.email==email).ToList().Sum(x => x.Amount);
        }

        public async Task<Expense> Get(string id) =>
           await _Expense.Find<Expense>(users => users.Id == id).FirstOrDefaultAsync();

        public async Task<Expense> Create(Expense Expense)
        {
            await _Expense.InsertOneAsync(Expense);
            return Expense;
        }
        
        public async Task<List<Expense>> GetAll(string id) =>
           await _Expense.Find(users => users.email == id).ToListAsync();

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

        public async void Update(string id, Expense Income) =>
           await  _Expense.ReplaceOneAsync(Income => Income.Id == id, Income);

        public async void Remove(Income Income) =>
           await _Expense.DeleteOneAsync(Income => Income.Id == Income.Id);

        public async void Remove(string id) =>
           await _Expense.DeleteOneAsync(Income => Income.Id == id);
    }
}

