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
    public class TableController : ControllerBase
    {
        private readonly TableService _bukuService;

        public TableController(TableService bukuService)
        {
            _bukuService = bukuService;
        }

        [HttpGet]
        public async Task<ActionResult<List<table>>> Get() =>
          await _bukuService.Get();

        [HttpGet("{id:length(24)}", Name = "GetTable")]
        public async Task<ActionResult<List<table>>> Get(string id)
        {
            var table = await _bukuService.GetByEventID(id);

            if (table == null)
            {
                return NotFound();
            }  

            return table;
        }

        [HttpPost]
        public async Task<ActionResult<table>> Create(table table)
        {
            await _bukuService.Create(table);

            return Created("", table);
        }


        //[HttpPost (Name = "AuthUser")]
        //public async Task<ActionResult<Users>> AuthUser(UserLogin Users)
        //{
        //   var result =  await _bukuService.AuthLogin(Users);

        //    return CreatedAtRoute("GetAuth", new { id = result.Id.ToString() }, Users);
        //}


        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, table table)
        {
            var _table = await _bukuService.Get(id);

            if (_table == null)
            {
                return NotFound();
            }

            _bukuService.Update(id, table);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var _table = await _bukuService.Get(id);

            if (_table == null)
            {
                return NotFound();
            }

            _bukuService.Remove(_table.Id);

            return NoContent();
        }
    }
}

