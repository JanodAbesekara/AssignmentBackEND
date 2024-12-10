using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; } 

        public string ISBN { get; set; } 

        [Required]
        public DateTime PublicationDate { get; set; } 
    }
}
