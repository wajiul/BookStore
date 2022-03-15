using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository {
    public class BookRepository : IBookRepository {

        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context) {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model) {
            var newBook = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                CoverImgUrl = model.CoverImgUrl
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks() {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.ToListAsync();
            if (allBooks?.Any() == true) {
                foreach (var book in allBooks) {
                    books.Add(new BookModel()
                    {
                        Id = book.Id,
                        Author = book.Author,
                        Title = book.Title,
                        Description = book.Description,
                        CoverImgUrl = book.CoverImgUrl
                    }); ;
                }
            }
            return books;
        }

        public async Task<List<BookModel>> GetTopBooks() {
            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Title = book.Title,
                Description = book.Description,
                TotalPages = book.TotalPages

            }).Take(5).ToListAsync();
        }


        public async Task<BookModel> GetBookById(int id) {
            var book = await _context.Books.FindAsync(id);
            if (book != null) {
                var bookDetails = new BookModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    CoverImgUrl = book.CoverImgUrl
                };
                return bookDetails;
            }
            return null;
        }
        public string Talk() {
            return "Hi , I am a example of DI on view";
        }
        public List<BookModel> SearchBook(string title, string authorname) {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorname)).ToList();
        }
        private List<BookModel> DataSource() {
            return new List<BookModel>()
            {
                new BookModel() {Id = 1, Title = "MVC", Author = "wajiul", Description = "description 1", Category="Programming", TotalPages = 200},
                new BookModel() {Id = 2, Title = "MVC1", Author = "wajiul1" , Description = "description 1", Category="Programming",  TotalPages = 200},
                new BookModel() {Id = 11, Title = "MVC2", Author = "wajiul2" , Description = "description 1", Category="Programming",  TotalPages = 200},
                new BookModel() {Id = 2006, Title = "MVC2", Author = "wajiul2" , Description = "description 1", Category="Programming",  TotalPages = 200},
                new BookModel() {Id = 2007, Title = "MVC2", Author = "wajiul2" , Description = "description 1", Category="Programming", TotalPages = 200}

            };
        }
    }
}
