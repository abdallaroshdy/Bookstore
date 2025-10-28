using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {

        private readonly IBookstoreRepository<Author> authorRepo;

        public AuthorController(IBookstoreRepository<Author> authorRepo)
        {
            this.authorRepo = authorRepo;
        }

        // GET: AuthorController
        public ActionResult Index()
        {
            var authors = authorRepo.List();
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authorRepo.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            //if (id != author.Id)
            //{
            //    return BadRequest(); // prevent ID tampering
            //}

            try
            {

                authorRepo.Update(id, author);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepo.Find(id);
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author Item)
        {
            try
            {
                authorRepo.Delete(Item.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
