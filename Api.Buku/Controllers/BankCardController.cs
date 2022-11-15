
using Api.Buku.Models;
using Api.Buku.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
namespace Api.Buku.Controllers
{
    [Route("api/[controller]")]
   // [EnableCors("CorsPolicy")]
    [ApiController]
    public class BankCardController : ControllerBase
    {
        private readonly BankCardService _bukuService;

        public BankCardController(BankCardService bukuService)
        {
            _bukuService = bukuService;
        }

        [HttpGet]
        public async Task<ActionResult<decimal>> Get() =>
            _bukuService.GetTotal();

        [HttpGet("{id:length(24)}", Name = "GetCards")]
        public async  Task< ActionResult<BankCards>> Get(string id)
        {
            var income =await _bukuService.Get(id);

            if (income == null)
            {
                return NotFound();
            }

            return income;
        }
        //[HttpGet( Name = "GetTotal")]
        //public async Task<ActionResult<decimal>> GetTotal()
        //{
           
        //        var income =  _bukuService.GetTotal();
          
        //    return income;
        //}

        [HttpPost]
        public async Task<ActionResult<BankCards>> Create(BankCards Income)
        {
            Income.Id = Guid.NewGuid().ToString("N").Substring(0, 24);
            await _bukuService.Create(Income);

            return CreatedAtRoute("GetIncome", new { id = Income.Id.ToString() }, Income);
        }


        //[HttpPost (Name = "AuthUser")]
        //public async Task<ActionResult<Users>> AuthUser(UserLogin Users)
        //{
        //   var result =  await _bukuService.AuthLogin(Users);

        //    return CreatedAtRoute("GetAuth", new { id = result.Id.ToString() }, Users);
        //}


        //[HttpPut("{id:length(24)}")]
        //public async Task<IActionResult> Update(string id, BankCards Income)
        //{
        
        //    var income =await  _bukuService.Get(id);
        //    var inc = new Income();
        //    //var guests = new guests();
        //    //var guestsList = new List<guests>();

        //    //if (users == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //events
            
        //    if(income!=null)
        //    {
        //       // Guid obj = Guid.NewGuid();
        //       // users.events = events;
        //        inc.Id = Guid.NewGuid().ToString();
        //        inc.Amount= income.Amount;
        //        inc.Status = income.Status;
        //        inc.DateEntered = income.DateEntered;
        //        inc.Description = income.Description;

        //    }


        //    _bukuService.Update(id, inc);

        //    return Created("",inc);
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