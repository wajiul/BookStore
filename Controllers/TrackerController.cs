using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers {
    public class TrackerController : Controller {
        private readonly BookStoreContext _context;
        public TrackerController(BookStoreContext context ) {
            _context = context;
        }
        public IActionResult AddSubmission() {
            return View();
        }
        [HttpPost]
        public IActionResult AddSubmission(Submission sub) {
            _context.userSubmission.Add(sub);
            _context.SaveChanges();
            return View();
        }
    }
}
