﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

         
        //[HttpGet("list/{id:customName}")]
        //public string CheckUser(string user_name)
        //{
        //   // var user = await _context.Users.FindAsync(id);
            
        //    User user= (User)_context.Users.Where(e => e.User_Name == user_name && e.Password == "1234");

        //    IDictionary<string, string> result = new Dictionary<string, string>();
   
          
        //    if (user != null)
        //    {
        //        result["status"] = "200";
        //        result["message"] = "Success";
        //    }
        //    else
        //    {
        //        result["status"] = "404";
        //        result["message"] = "Failed";
        //    }
        //   return JsonConvert.SerializeObject(result);

        //}
        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);

        //}
        [HttpPost]
        [Route("CheckUser")]
        public string CheckUser(string user_name,string password)
        {
            // var user = await _context.Users.FindAsync(id);

            var  user =  _context.Users.Where(e => e.User_Name == user_name && e.Password == password);
           
            IDictionary<string, string> result = new Dictionary<string, string>();


            if (user.Count()>0)
            {
                result["status"] = "200";
                result["message"] = "Success";
            }
            else
            {
                result["status"] = "404";
                result["message"] = "Failed";
            }
            return JsonConvert.SerializeObject(result);

        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
