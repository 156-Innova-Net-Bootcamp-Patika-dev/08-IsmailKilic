using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Entities.Abstract;

namespace Entities.Concrete
{
    /// <summary>
    /// Post table
    /// Has one to one relationship with category and user tables
    /// Has many to one relationship with comment table
    /// </summary>
    public class Post : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
