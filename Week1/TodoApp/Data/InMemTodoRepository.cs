using System;
using System.Collections.Generic;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class InMemTodoRepository : ITodoRepository
    {
        private readonly List<Todo> todos = new()
        {
            new Todo { Id = Guid.NewGuid(), Description = "Buy Eggs", CreatedDate = DateTimeOffset.UtcNow },
            new Todo { Id = Guid.NewGuid(), Description = "Read a Book", CreatedDate = DateTimeOffset.UtcNow },
            new Todo { Id = Guid.NewGuid(), Description = "Organize Office", CreatedDate = DateTimeOffset.UtcNow }
        };

        public void CreateTodo(Todo todo)
        {
            todos.Add(todo);
        }

        public void DeleteTodo(Guid id)
        {
            var index = todos.FindIndex(existingTodo => existingTodo.Id == id);
            todos.RemoveAt(index);
        }

        public Todo GetTodo(Guid id)
        {
            var todo = todos.Find(todo => todo.Id == id);
            return todo;
        }

        public IEnumerable<Todo> GetTodos()
        {
            return todos;
        }

        public void UpdateTodo(Todo todo)
        {
            var index = todos.FindIndex(existingTodo => existingTodo.Id == todo.Id);
            todos[index] = todo;
        }
    }
}
