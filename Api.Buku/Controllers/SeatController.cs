using Api.Buku.Models;
using Api.Buku.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Buku.Controllers
{
    [Route("api/[controller]")]
    // [EnableCors("CorsPolicy")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly SeatService _bukuService;

        public SeatController(SeatService bukuService)
        {
            _bukuService = bukuService;
        }

        [HttpGet]
        public async Task<ActionResult<List<seat>>> Get() =>
          await _bukuService.Get();

        [HttpGet("{id:length(24)}", Name = "GetSeats")]
        public async Task<ActionResult<seat>> Get(string id)
        {
            var events = await _bukuService.Get(id);

            if (events == null)
            {
                return NotFound();
            }

            return events;
        }

        [HttpPost]
        public async Task<ActionResult<events>> Create(seat seat)
        {
            await _bukuService.Create(seat);

            return Created("", seat);
        }


        //[HttpPost (Name = "AuthUser")]
        //public async Task<ActionResult<Users>> AuthUser(UserLogin Users)
        //{
        //   var result =  await _bukuService.AuthLogin(Users);

        //    return CreatedAtRoute("GetAuth", new { id = result.Id.ToString() }, Users);
        //}


        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, seat seat)
        {
            var events1 = await _bukuService.Get(id);

            if (events1 == null)
            {
                return NotFound();
            }

            _bukuService.Update(id, seat);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var events = await _bukuService.Get(id);

            if (events == null)
            {
                return NotFound();
            }

            _bukuService.Remove(events.Id);

            return NoContent();
        }
    }
}

