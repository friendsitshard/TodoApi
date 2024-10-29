using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.DataContext
{
    public class TodoDbContext:DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options) { }
        public DbSet<TodoTask> TodoTasks { get; set; }

    }
}
