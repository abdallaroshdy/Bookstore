namespace Bookstore.Models.Repositories
{
    public class AuthorDBRepository : IBookstoreRepository<Author>
    {
        private readonly BookStoreDBContext dBContext;

        public AuthorDBRepository(BookStoreDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public void Add(Author entity)
        {
            dBContext.Authors.Add(entity);
            Commit();
        }


        public void Delete(int id)
        {
            var auth = Find(id);
            dBContext.Authors.Remove(auth);
            Commit();
        }

        public Author? Find(int id)
        {
            var auth = dBContext.Authors.SingleOrDefault(x => x.Id == id);
            return auth;
        }

        public IList<Author> List()
        {
            return dBContext.Authors.ToList();
        }

        public void Update( Author entity)
        {
            dBContext.Update(entity);
            Commit();
        }

        private void Commit()
        {
            dBContext.SaveChanges();
        }

    }
}

