using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
        public BookController(IBookstoreRepository<Book> Books , IBookstoreRepository<Author> authorRepository)
        {
            bookRepository = Books;
            this.authorRepository = authorRepository;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel()
            {
                Authors = FillSelectBox(),
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel Item)
        {
            try
            {
                if (Item.AuthorId == -1)
                {
                    ViewBag.Message = "Please select an Author from the list";
                    Item.Authors = FillSelectBox();
                    return View(Item);
                }

                var author = authorRepository.Find(Item.AuthorId);

                var book = new Book()
                {
                    Description = Item.Description,
                    Title = Item.Title,
                    Author = author,
                };
                bookRepository.Add(book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);

            var authorId = book.Author is null ? 0 : book.Author.Id;

            var model = new BookAuthorViewModel()
            {
                AuthorId = authorId,
                Description = book.Description,
                Title = book.Title,
                Authors = FillSelectBox(),
                BookId = book.Id
            };

            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel model)
        {
            try
            {
                if (model.AuthorId == -1)
                {
                    ViewBag.Message = "Please select an Author from the list";
                    model.Authors = FillSelectBox();
                    return View(model);
                }

                var book = new Book()
                {
                    Author = authorRepository.Find(model.AuthorId),
                    Description = model.Description,
                    Title = model.Title,

                };

                bookRepository.Update(model.BookId, book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<Author> FillSelectBox()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author()
            {
                FullName = "---- Please select Author ----",
                Id = -1
            });
            return authors;
        }
    }
}
