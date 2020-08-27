using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TodoItemsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoCustomer>>> GetTodoItems()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoCustomer>> GetTodoItem(long id)
        {
            var todoItem = await _context.Customers.FindAsync(id);
            
            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoCustomer todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
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

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        //  public async Task<ActionResult<TodoCustomer>> PostTodoItem(TodoCustomer todoItem)
        //{
        //  _context.Customers.Add(todoItem);
        // await _context.SaveChangesAsync();
        //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        // return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        //}
        [HttpPost]
        public HttpResponseMessage Post(JToken postData, HttpRequestMessage request)
        {
            // Initialization  
            HttpResponseMessage response = null;
            TodoCustomer requestObj = JsonConvert.DeserializeObject<TodoCustomer>(postData.ToString());
            DataTable responseObj = new DataTable();
            string json = string.Empty;

       
                // Settings.  
                json = JsonConvert.SerializeObject(responseObj);
            response = requestObj.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }




        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoCustomer>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.Customers.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
