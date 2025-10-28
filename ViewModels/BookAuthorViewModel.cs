using Bookstore.Models;

namespace Bookstore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }

        public List<Author> Authors { get; set; }
    }
}
