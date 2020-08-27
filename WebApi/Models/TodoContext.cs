using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace TodoApi.Models // main class
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoCustomer> TodoItems { get; set; } // join data TodoItem
    }
}
