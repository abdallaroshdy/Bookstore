
namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,Title = "introduction to C++" , Description=  "No Description yet"
                },
                new Book()
                {
                    Id = 2,Title = "introduction to C##" , Description=  "No Description yet"
                },
                new Book()
                {
                    Id = 3,Title = "introduction to Java" , Description=  "No Description yet"
                },
            };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(x => x.Id) + 1;
            books.Add(entity);
        }

        public Book Find(int id)
        {
           var book  = books.SingleOrDefault(x => x.Id == id);
            return book;
        }

        public IList<Book> List()
        {
           return books;
        }

        public void Update(Book entity)
        {
            var book = Find(entity.Id);
            book.Title = entity.Title;
            book.Description = entity.Description;  
            book.Author = entity.Author;
            book.ImageURL = entity.ImageURL;
            
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }


    }
}
