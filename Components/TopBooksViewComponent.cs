using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Components {
    public class TopBooksViewComponent : ViewComponent {
        private readonly IBookRepository _bookRepository;
        public TopBooksViewComponent(IBookRepository bookRepository) {
            _bookRepository = bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync() {
            var data = await _bookRepository.GetTopBooks();
            return View(data);
        }
    }
}

