using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.Controllers {
    [Authorize]
    public class BookController : Controller {
        private readonly IBookRepository _bookRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment) {         // dependency 
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index() {
            
            return View();
        }
        [Route("bn/mybooks")]
        public async Task<IActionResult> GetAllBooks() {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }
        public async Task<IActionResult> GetBook(int id) {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }
        //Book/searchbook?name=dotnet&autorName=wajiul

        public IActionResult AddNewBook(bool isDataAdded = false, int bookId = 0) {
            ViewBag.isDataAdded = isDataAdded;
            ViewBag.BookId = bookId;
            var book = new BookModel()
            {
                Title = "my MVC",
                Author = "wajiul islam"
            };
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookData) {
            if (ModelState.IsValid) {
                if(bookData.CoverPhoto != null) {
                    string folder = "images/";
                    folder += Guid.NewGuid().ToString() + bookData.CoverPhoto.FileName;
                    bookData.CoverImgUrl = "/" + folder; // only folder name as we are in root
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await bookData.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                int id = await _bookRepository.AddNewBook(bookData);
                if (id > 0) {
                    return RedirectToAction(nameof(AddNewBook), new { isDataAdded = true, bookId = id });
                }
            }
            ModelState.AddModelError("", "This is for testing purpose");
            return View();
        }
        public List<BookModel> SearchBook(string title, string authorName) {
            return _bookRepository.SearchBook(title, authorName);

        }

    }
}
