#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetCoreMySQL.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly rocketelevatorsfoobarContext _context;

        public UserController(rocketelevatorsfoobarContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        

        


        // [HttpGet("isCustomer/{email}")]
        // public async Task<ActionResult<Employee>> IsValidUser(string email)
        // {
        //     Console.WriteLine(email);
        //     var allUsers = await _context.users.ToListAsync();
        //     var validUser = allUsers.Where(u => u.email == email);
        //     if(validUser.Count() > 0) {
        //         var employee = await _context.employees.FindAsync(validUser.First().id);
        //         Console.WriteLine("---------------- " + employee + "-----------------");
        //         if(employee != null) {
        //             return employee;
        //         } 
        //     }
        //     return Unauthorized();
        // }


        [HttpGet("isEmployee/{email}")]
        public async Task<ActionResult<Employee>> IsValidUser(string email)
        {
            Console.WriteLine(email);
            var allUsers = await _context.users.ToListAsync();
            var validUser = allUsers.Where(u => u.email == email);
            if(validUser.Count() > 0) {
                var employee = await _context.employees.FindAsync(validUser.First().id);
                if(employee != null) {
                    return employee;
                } 
            }
            return Unauthorized();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.users.Any(e => e.id == id);
        }
    }
}
