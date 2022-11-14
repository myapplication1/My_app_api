using Api.Buku.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Buku.Services
{
    public class EventService
    {

        private readonly IMongoCollection<events> _events;

        public EventService(IBukuDatabaseSettings settings)
        {

            settings.BukuCollectionName = "Events";
            var settings_ = MongoClientSettings
                .FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settings_);
            var database = client.GetDatabase(settings.DatabaseName);
            _events = database.GetCollection<events>(settings.BukuCollectionName);


            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);

            // _users = database.GetCollection<Users>(settings.BukuCollectionName);
        }


        public async Task<List<events>> Get() =>
          await _events.Find(events => true).ToListAsync();

        public async Task<events> Get(string id) =>
           await _events.Find<events>(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<events> Create(events events)
        {
            await _events.InsertOneAsync(events);
            return events;
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

        public async void Update(string id, events events) =>
           await  _events.ReplaceOneAsync(x => x.Id == id, events);

        public async void Remove(events events) =>
           await _events.DeleteOneAsync(x => x.Id == events.Id);

        public async void Remove(string id) =>
           await  _events.DeleteOneAsync(x => x.Id == id);
    }
}

