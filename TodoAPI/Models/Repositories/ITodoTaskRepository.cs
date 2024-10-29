namespace TodoAPI.Models.Repositories
{
    public interface ITodoTaskRepository
    {
        Task<IEnumerable<TodoTask?>> GetAllAsync();
        Task<TodoTask?> GetByIdAsync(int id);
        Task PostAsync(TodoTask todo);
        Task UpdateAsync(TodoTask todo);
        Task DeleteAsync(int id);
        Task UpdateIsCompleted(int id, bool isCompleted);

    }
}
