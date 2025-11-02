using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(maximumLength: 120 ,MinimumLength = 15)]
        public string Description { get; set; } = string.Empty ;

        public Author Author { get; set; }


    }
}
