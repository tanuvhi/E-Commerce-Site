using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UltraShopBd.Domain.Entities
{
    public  class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        
        public string Name { get; set; }

        [Required]
       [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        
        public string Description { get; set; }

        [Required]
       [Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]  
        public decimal Price { get; set; }

        public int SubSubCategoryId { get; set; }

        [Display(Name = "Cover Image")]
        public string ImageUrl { get; set; }

        public int Activity { get; set; }
    }
}
