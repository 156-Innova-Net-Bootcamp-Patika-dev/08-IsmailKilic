using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    /// <summary>
    /// Dto used to create a new post
    /// </summary>
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
