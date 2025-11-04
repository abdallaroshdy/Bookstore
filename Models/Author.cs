using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Author
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Full Name")]
        [StringLength(50 , MinimumLength = 10)]
        public string FullName { get; set; } = string.Empty;
    }
}
