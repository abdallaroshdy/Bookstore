
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Bookstore.Models.Repositories
{
    public class BookDBRepository : IBookstoreRepository<Book>
    {
        private readonly BookStoreDBContext dBContext;

        public BookDBRepository(BookStoreDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public void Add(Book entity)
        {
            dBContext.Books.Add(entity);
            Commit();
        }

        public Book? Find(int id)
        {
            Book? book = dBContext.Books.Include(a => a.Author).SingleOrDefault(x => x.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return dBContext.Books.Include(a=>a.Author).ToList();
        }

        public void Update(Book entity)
        {
            dBContext.Books.Update(entity);
            Commit();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            dBContext.Books.Remove(book);
            Commit();
        }
        private void Commit()
        {
            dBContext.SaveChanges();
        }
    }
}
