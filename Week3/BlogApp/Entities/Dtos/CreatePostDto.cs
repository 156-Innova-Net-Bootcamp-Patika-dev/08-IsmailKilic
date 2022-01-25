using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class CreatePostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MinLength(100)]
        public string Body { get; set; }
        [Required]
        [Url]
        public string Photo { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
