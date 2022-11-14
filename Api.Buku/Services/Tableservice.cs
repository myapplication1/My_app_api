using Api.Buku.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Buku.Services
{
    public class TableService
    {

        private readonly IMongoCollection<table> _table;

        public TableService(IBukuDatabaseSettings settings)
        {

            settings.BukuCollectionName = "Table";
            var settings_ = MongoClientSettings
                .FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settings_);
            var database = client.GetDatabase(settings.DatabaseName);
            _table = database.GetCollection<table>(settings.BukuCollectionName);


            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);

            // _users = database.GetCollection<Users>(settings.BukuCollectionName);
        }


        public async Task<List<table>> Get() =>
          await _table.Find(table => true).ToListAsync();

        public async Task<table> Get(string id) =>
           await _table.Find<table>(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<table>> GetByEventID(string id) =>
          await _table.Find<table>(x => x.eventID == id).ToListAsync();


        public async Task<table> Create(table table)
        {
            await _table.InsertOneAsync(table);
            return table;
        }


        //public async Task<Users> AuthLogin(UserLogin users)
        //{
        //    Users UserResults = null;
        //    try
        //    {

        //        UserResults = await _users
        //      .Find<Users>(x => x.EmailAddress == users.Email
        //        && x.Password == users.Password).FirstOrDefaultAsync();

        //    }
        //    catch (Exception E)
        //    {

        //    }
        //    return UserResults;


        //}

        public async void Update(string id, table table) =>
           await  _table.ReplaceOneAsync(x => x.Id == id, table);

        public async void Remove(table table) =>
           await _table.DeleteOneAsync(x => x.Id == table.Id);

        public async void Remove(string id) =>
           await  _table.DeleteOneAsync(x => x.Id == id);
    }
}

