using Bookstore.Models;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int? BookId { get; set; }

        [Display(Name = "Book Title")]
        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 120 ,MinimumLength = 15)]
        public string Description { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage = "Please select an Author")]
        public int AuthorId { get; set; }

        public List<Author>? Authors { get; set; }

        public IFormFile Image { get; set; }
        public string ImageURL { get; set; } = string.Empty;
    }
}
