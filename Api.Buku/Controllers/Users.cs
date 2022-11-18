
using Api.Buku.Models;
using Api.Buku.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Buku.Controllers
{
    [Route("api/[controller]")]
   // [EnableCors("CorsPolicy")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BukuService _bukuService;

        public UsersController(BukuService bukuService)
        {
            _bukuService = bukuService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> Get() =>
          await  _bukuService.Get();

        //[HttpGet("{id:length(24)}", Name = "GetUsers")]
        //public async  Task< ActionResult<Users>> Get(string id)
        //{
        //    var users =await _bukuService.Get(id);

        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return users;
        //}


        [HttpGet("{email}/{password}", Name = "GetUsers")]
        public async Task<ActionResult<Users>> Get(string email , string password)
        {
            var users = await _bukuService.GetLogin(email, password);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }


        [HttpPost]
        public async Task<ActionResult<Users>> Create(Users Users)
        {
            Users.Id = Guid.NewGuid().ToString("N").Substring(0, 24);
            await _bukuService.Create(Users);

            return CreatedAtRoute("GetUsers", new { id = Users.Id.ToString() }, Users);
        }


        //[HttpPost (Name = "AuthUser")]
        //public async Task<ActionResult<Users>> AuthUser(UserLogin Users)
        //{
        //   var result =  await _bukuService.AuthLogin(Users);

        //    return CreatedAtRoute("GetAuth", new { id = result.Id.ToString() }, Users);
        //}


        //[HttpPut("{id:length(24)}")]
        //public async Task<IActionResult> Update(string id, UsersDTO Users)
        //{
        
        //    var users =await  _bukuService.Get(id);
        //    var events = new events();
        //    var guests = new guests();
        //    var guestsList = new List<guests>();

        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    //events
            
        //    if(Users.events!=null)
        //    {
        //       // Guid obj = Guid.NewGuid();
        //        users.events = events;
        //        users.events.Id = Guid.NewGuid().ToString();
        //        users.events.eventName = Users.events.eventName;
        //        users.events.Address = Users.events.Address;
        //        users. events.desp = Users.events.desp;
        //        users. events.city = Users.events.city;
        //        users. events.town = Users.events.town;
        //    }


            //guests
        //    if (Users.guests != null)
        //    {
        //    //    Guid obj = new Guid();
        //        guests.Id = Guid.NewGuid().ToString(); 
        //        guests.Fname = Users.guests.Fname;
        //        guests.Lname = Users.guests.Lname;
        //        guests.CellNumber = Users.guests.CellNumber;
        //        guests.Seat = Users.guests.Seat;
        //        guests.Table = Users.guests.Table;
        //        users.guests.Add(guests);// = guestsList;
        //       // List<guests> fff = new List<guests>(); 
                     
        //        ////users.guests.AddRange(guestsList);
        //    }
        //    _bukuService.Update(id, users);

        //    return Created("",users);
        //}

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var users =await _bukuService.Get(id);

            if (users == null)
            {
                return NotFound();
            }

            _bukuService.Remove(users.Id);

            return NoContent();
        }
    }
}