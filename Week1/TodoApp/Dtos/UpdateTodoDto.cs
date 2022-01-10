using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos
{
    public class UpdateTodoDto
    {
        [Required]
        public string Description { get; set; }
    }
}
