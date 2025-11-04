using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.Services;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
        private readonly IAttachmecntService attachmecntService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting;

        public BookController(IBookstoreRepository<Book> Books
                            , IBookstoreRepository<Author> authorRepository
                            , IAttachmecntService attachmecntService
                            , Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            bookRepository = Books;
            this.authorRepository = authorRepository;
            this.attachmecntService = attachmecntService;
            this.hosting = hosting;
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
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "You have to fill all fields!!");
                Item.Authors = FillSelectBox();
                return View(Item);
            }

            try
            {
                //string imageName =string.Empty;
                //if (Item.Image is not null)
                //{

                //    string ImagePath = Path.Combine(hosting.WebRootPath, "Uploads");
                //    imageName = Item.Image.FileName;
                //    string FullPath = Path.Combine(ImagePath, imageName);
                //    Item.Image.CopyTo(new FileStream(FullPath, FileMode.Create));

                //}

                string FolderPath = Path.Combine(hosting.WebRootPath, "Uploads");
                string? ImageName = attachmecntService.Create(Item.Image, FolderPath);

                if (ImageName is null)
                {
                    ModelState.AddModelError("", "You should upload Image that less than 10 Migabytes");
                    Item.Authors = FillSelectBox();
                    return View(Item);
                }

                var author = authorRepository.Find(Item.AuthorId);

                var book = new Book()
                {
                    Description = Item.Description,
                    Title = Item.Title,
                    Author = author,
                    ImageURL = ImageName,
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
                BookId = book.Id,
                ImageURL = book.ImageURL
            };
            TempData["ImageURL"] = book.ImageURL;

            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "You have to fill all fields!!");
                model.Authors = FillSelectBox();
                return View(model);
            }

            try
            {
                //var imageName = string.Empty;
                //if (model.Image is not null)
                //{
                //    var uplods = Path.Combine(hosting.WebRootPath, "Uploads");
                //    imageName = model.Image.FileName;

                //    //delete old image
                //    if (!model.ImageURL.Equals(string.Empty))
                //    {

                //    string fullOldPath = Path.Combine(uplods, model.ImageURL);
                //    System.IO.File.Delete(fullOldPath);

                //    }
                //    // save new image
                //    string fullPath = Path.Combine(uplods, imageName);  
                //    model.Image.CopyTo(new FileStream(fullPath, FileMode.Create));
                //}

                string FolderPath = Path.Combine(hosting.WebRootPath, "Uploads");
                string? ImageName = attachmecntService.Create(model.Image, FolderPath);

                if (ImageName is null)
                {
                    ModelState.AddModelError("", "You should upload Image that less than 10 Migabytes");
                    model.Authors = FillSelectBox();
                    return View(model);
                }

                attachmecntService.Delete(TempData["ImageURL"]?.ToString(), FolderPath);

                var book = new Book()
                {
                    Author = authorRepository.Find(model.AuthorId),
                    Description = model.Description,
                    Title = model.Title,
                    ImageURL = ImageName,
                };

                bookRepository.Update(model.BookId.Value, book);

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
            return authors;
        }
    }
}
