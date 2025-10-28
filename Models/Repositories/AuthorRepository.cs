
namespace Bookstore.Models.Repositories
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {
        List<Author> authros;
        public AuthorRepository()
        {
            authros = new List<Author>()
            {
                new Author()
                {
                    Id=1 , FullName = "ahmed"
                },
                new Author()
                {
                    Id=2 , FullName = "mohamed"
                },
                new Author()
                {
                    Id=3 , FullName = "salah"
                },
            };
        }
        public void Add(Author entity)
        {
            authros.Add(entity);
        }

        public void Delete(int id)
        {
            var auth = Find(id);
            authros.Remove(auth);
        }

        public Author Find(int id)
        {
            var auth = authros.SingleOrDefault(x => x.Id == id);
            return auth;
        }

        public IList<Author> List()
        {
            return authros;
        }

        public void Update(int id, Author entity)
        {
           var auth = Find(id);

           auth.FullName = entity.FullName;
        }
    }
}
