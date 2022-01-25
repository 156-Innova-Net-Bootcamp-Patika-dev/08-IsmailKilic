using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class CreateCommentDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}
