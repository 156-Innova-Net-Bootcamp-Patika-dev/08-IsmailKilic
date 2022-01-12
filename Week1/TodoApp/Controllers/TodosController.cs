using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;
using System;
using TodoApp.Dtos;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository repository;

        public TodosController(ITodoRepository repository)
        {
            this.repository = repository;
        }

        // Returns all todo items
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            var todos = repository.GetTodos();
            return Ok(todos);
        }

        // Returns todo item by id
        // If not found, returns 404 error
        [HttpGet("{id}")]
        public ActionResult<Todo> GetTodoById(Guid id)
        {
            var todo = repository.GetTodo(id);

            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // Creates a todo item
        // Returns 201 status code
        [HttpPost]
        public ActionResult<Todo> CreateDto(CreateTodoDto todoDto)
        {
            Todo todo = new()
            {
                Id = Guid.NewGuid(),
                Description = todoDto.Description,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateTodo(todo);

            return new ObjectResult(todo) { StatusCode = StatusCodes.Status201Created };
        }

        // Updates todo item by id
        // If not found, returns 404 Notfound error
        [HttpPut("{id}")]
        public ActionResult UpdateTodo(Guid id, UpdateTodoDto todoDto)
        {
            var existingItem = repository.GetTodo(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            existingItem.Description = todoDto.Description;

            repository.UpdateTodo(existingItem);

            return NoContent();
        }

        // Deletes todo item by id
        // If not found, returns 404 Notfound error
        [HttpDelete("{id}")]
        public ActionResult DeleteItemAsync(Guid id)
        {
            var existingItem = repository.GetTodo(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            repository.DeleteTodo(id);

            return NoContent();
        }
    }
}
