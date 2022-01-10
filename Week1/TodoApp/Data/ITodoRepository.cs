﻿using System;
using System.Collections.Generic;
using TodoApp.Models;

namespace TodoApp.Data
{
    public interface ITodoRepository
    {
        Todo GetTodo(Guid id);
        IEnumerable<Todo> GetTodos();
        void CreateTodo(Todo todo);
        void UpdateTodo(Todo todo);
        void DeleteTodo(Guid id);
    }
}
