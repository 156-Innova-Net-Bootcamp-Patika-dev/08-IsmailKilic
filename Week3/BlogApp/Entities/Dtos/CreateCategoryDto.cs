using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
