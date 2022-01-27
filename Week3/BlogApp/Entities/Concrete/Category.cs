using System.ComponentModel.DataAnnotations;
using Entities.Abstract;
using System.Collections.Generic;

namespace Entities.Concrete
{
    /// <summary>
    /// Category table
    /// Has one to many relationship with post table
    /// </summary>
    public class Category : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
