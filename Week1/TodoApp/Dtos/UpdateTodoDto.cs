using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos
{
    // Dto used to update todo item
    // It consists description
    // Id will be taken as parameter in controller
    public class UpdateTodoDto
    {
        [Required]
        public string Description { get; set; }
    }
}
