using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EcommerceRazor.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [NotNull]
        [DisplayName("Category Name")]  // this annotation will also be used in label fields
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Range Must be between 1-100")] // this will also pass the custom error message to asp-validation-for tag-Helper
        public int DisplayOrder { get; set; }
    }
}
