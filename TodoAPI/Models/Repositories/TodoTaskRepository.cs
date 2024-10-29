
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoAPI.DataContext;

namespace TodoAPI.Models.Repositories
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly TodoDbContext _context;
        public TodoTaskRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoTask?>> GetAllAsync()
        { 
            return await _context.TodoTasks.ToListAsync();
        }

        public async Task<TodoTask?> GetByIdAsync(int id)
        {
            var todo = await _context.TodoTasks.FirstOrDefaultAsync(t => t.Id == id);
            if (todo == null)
                throw new ArgumentNullException("Todo task is not found");
            return todo;
        }

        public async Task PostAsync(TodoTask todo)
        {
            _context.TodoTasks.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoTask todo)
        {
            var todoForUpdate = await _context.TodoTasks.FirstOrDefaultAsync(t => t.Id == todo.Id);

            if (todoForUpdate == null)
                throw new ArgumentException("Todo task not found");

            todoForUpdate.Title = todo.Title;
            todoForUpdate.Description = todo.Description;
            todoForUpdate.IsCompleted = todo.IsCompleted;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await GetByIdAsync(id);

            if (todo == null)
                throw new ArgumentException("Todo task not found");

            _context.TodoTasks.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIsCompleted(int id, bool isCompleted)
        {
            var todo = await _context.TodoTasks.FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
                throw new ArgumentException("Todo task not found");

            todo.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();
        }
    }
}
