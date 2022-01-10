using System.ComponentModel.DataAnnotations;
using TodoApp.Models;

namespace TodoApp.Dtos
{
    public class CreateTodoDto
    {
        [Required]
        public string Description { get; set; }
    }
}
