using System;

namespace TodoApp.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
