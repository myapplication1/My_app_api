using Api.Buku.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Buku.Services
{
    public class SeatService
    {

        private readonly IMongoCollection<seat> _table;

        public SeatService(IBukuDatabaseSettings settings)
        {

            settings.BukuCollectionName = "Seat";
            var settings_ = MongoClientSettings
                .FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(settings_);
            var database = client.GetDatabase(settings.DatabaseName);
            _table = database.GetCollection<seat>(settings.BukuCollectionName);


            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);

            // _users = database.GetCollection<Users>(settings.BukuCollectionName);
        }


        public async Task<List<seat>> Get() =>
          await _table.Find(seat => true).ToListAsync();

        public async Task<seat> Get(string id) =>
           await _table.Find<seat>(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<seat>> GetByEventID(string id) =>
          await _table.Find<seat>(x => x.EventID == id).ToListAsync();


        public async Task<seat> Create(seat seat)
        {
            await _table.InsertOneAsync(seat);
            return seat;
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

        public async void Update(string id, seat seat) =>
           await  _table.ReplaceOneAsync(x => x.Id == id, seat);

        public async void Remove(seat seat) =>
           await _table.DeleteOneAsync(x => x.Id == seat.Id);

        public async void Remove(string id) =>
           await  _table.DeleteOneAsync(x => x.Id == id);
    }
}

