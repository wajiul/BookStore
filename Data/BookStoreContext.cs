using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data {
    public class BookStoreContext : IdentityDbContext<ApplicationUser> {
        public BookStoreContext(DbContextOptions<BookStoreContext> options): base(options) {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Submission> userSubmission { get; set; }
        public DbSet<BookStore.Models.SignUpUserModel> SignUpUserModel { get; set; }
        public DbSet<BookStore.Models.SignInModel> SignInModel { get; set; }
    }
}
