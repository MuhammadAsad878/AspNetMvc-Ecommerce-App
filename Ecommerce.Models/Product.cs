using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }
        
        

        [Required]
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        [Display(Name = "Price for 1-50 books")]
        public double Price { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        [Display(Name = "Price for 50+ books")]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        [Display(Name = "Price for 100+ books")]
        public double Price100 { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        [Display(Name = "Book Image")]
        public string ImageUrl { get; set; } = string.Empty;

    }
}
