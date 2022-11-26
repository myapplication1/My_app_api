
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
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _bukuService;

        public ExpenseController(ExpenseService bukuService)
        {
            _bukuService = bukuService;
        }

        [HttpGet]
        public ActionResult<decimal> Get() =>
            _bukuService.GetToTal();

        [HttpGet("{id:length(24)}", Name = "")]
        public async  Task< ActionResult<Expense>> Get(string id)
        {
            var income =await _bukuService.Get(id);

            if (income == null)
            {
                return NotFound();
            }

            return income;
        }

        [HttpPost]
        public async Task<ActionResult<Users>> Create(Expense Expense)
        {
            //Income.Id =  Guid.NewGuid().ToString();
            Expense.Id = Guid.NewGuid().ToString("N").Substring(0, 24);
            await _bukuService.Create(Expense);

            return CreatedAtRoute("", new { id = Expense.Id.ToString() }, Expense);
        }


        //[HttpPost (Name = "AuthUser")]
        //public async Task<ActionResult<Users>> AuthUser(UserLogin Users)
        //{
        //   var result =  await _bukuService.AuthLogin(Users);

        //    return CreatedAtRoute("GetAuth", new { id = result.Id.ToString() }, Users);
        //}


        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Expense Expense)
        {
        
            var income =await  _bukuService.Get(id);
            var inc = new Expense();
            //var guests = new guests();
            //var guestsList = new List<guests>();

            //if (users == null)
            //{
            //    return NotFound();
            //}

            //events
            
            if(Expense != null)
            {
               // Guid obj = Guid.NewGuid();
               // users.events = events;
                inc.Id = Guid.NewGuid().ToString();
                inc.Amount= income.Amount;
                inc.Status = income.Status;
                inc.DateEntered = income.DateEntered;
                inc.Description = income.Description;

            }


            _bukuService.Update(id, inc);

            return Created("",inc);
        }

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