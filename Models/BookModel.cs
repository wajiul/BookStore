using BookStore.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Validaton_in_MVC;

namespace BookStore.Models {
    public class BookModel {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required] 
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int TotalPages { get; set; }
        [Required]
        public LanguageEnum LanguageList { get; set; }
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImgUrl { get; set; }
    }
}
