using Microsoft.AspNetCore.Mvc;
using TodoAPI.Filters;
using TodoAPI.Filters.ActionFilters;
using TodoAPI.Models;
using TodoAPI.Models.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITodoTaskRepository _repository;

        public TodoTaskController(ITodoTaskRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var todos = await _repository.GetAllAsync();

            if (todos == null || !todos.Any())
                return NotFound();

            return Ok(todos);
        }

        [HttpGet("{id}")]
        [TodoIdValidateFilter]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _repository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [TodoPostValidateFilter]
        public async Task<IActionResult> Post([FromBody] TodoTask todo)
        {
            await _repository.PostAsync(todo);
            return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        [TodoUpdateValidateFilter]
        public async Task<IActionResult> Update(int id, [FromBody] TodoTask updatedTodo)
        {
            try
            {
                await _repository.UpdateAsync(updatedTodo);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(updatedTodo);
        }

        [HttpDelete("{id}")]
        [TodoIdValidateFilter]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpPatch("{id}/isCompleted")]
        [TodoIdValidateFilter]
        public async Task<IActionResult> UpdateIsCompleted(int id, [FromBody] bool isCompleted)
        {
            await _repository.UpdateIsCompleted(id, isCompleted);

            return NoContent();
        }
    }
}
