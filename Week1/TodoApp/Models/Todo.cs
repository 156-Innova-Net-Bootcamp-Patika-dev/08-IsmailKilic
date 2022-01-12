using System;

namespace TodoApp.Models
{
    // Todo Model
    public class Todo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
