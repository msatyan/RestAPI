using System.ComponentModel.DataAnnotations;

namespace MyAspCoreRestAPI.Models
{
    public class MyProducts
    {
        public int Id { get; set; }

        [Required, MaxLength(30)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public int Price { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        public string ContactEmail { get; set; }
    }
}

