using Microsoft.EntityFrameworkCore;
using Kotarski_Wiktor_ToDo_API.Models;

namespace Kotarski_Wiktor_ToDo_API.Data
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> Items { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options)
            :base(options)
        {

        }
    }
}
