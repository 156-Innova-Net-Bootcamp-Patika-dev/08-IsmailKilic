using System;
using System.Collections.Generic;
using TodoApp.Models;

namespace TodoApp.Data
{
    // In memory implementation of ITodoRepository
    // It consists a static list of todos
    public class InMemTodoRepository : ITodoRepository
    {
        private readonly List<Todo> todos = new()
        {
            new Todo { Id = Guid.NewGuid(), Description = "Buy Eggs", CreatedDate = DateTimeOffset.UtcNow },
            new Todo { Id = Guid.NewGuid(), Description = "Read a Book", CreatedDate = DateTimeOffset.UtcNow },
            new Todo { Id = Guid.NewGuid(), Description = "Organize Office", CreatedDate = DateTimeOffset.UtcNow }
        };

        // Creates a new todo
        public void CreateTodo(Todo todo)
        {
            todos.Add(todo);
        }

        // Deletes a todo by id
        public void DeleteTodo(Guid id)
        {
            var index = todos.FindIndex(existingTodo => existingTodo.Id == id);
            todos.RemoveAt(index);
        }

        // Finds a todo by id
        // Returns Todo item
        public Todo GetTodo(Guid id)
        {
            var todo = todos.Find(todo => todo.Id == id);
            return todo;
        }

        // Returns all todo items
        public IEnumerable<Todo> GetTodos()
        {
            return todos;
        }

        // Updates todo item
        public void UpdateTodo(Todo todo)
        {
            var index = todos.FindIndex(existingTodo => existingTodo.Id == todo.Id);
            todos[index] = todo;
        }
    }
}
