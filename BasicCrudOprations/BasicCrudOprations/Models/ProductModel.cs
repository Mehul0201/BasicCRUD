using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicCrudOprations.Models
{
    public class ProductModel
    {
        [Key]
        [Required]
        [DisplayName("Product ID")]
        public int ProductID { get; set; }
        [Required]
        [DisplayName("Product Name ")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("Category ID ")]
        public int CategoryID { get; set; }

        public CategoryModel Category { get; set; }
    }
}