using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    /// <summary>
    /// Dto used to create a new category
    /// </summary>
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
