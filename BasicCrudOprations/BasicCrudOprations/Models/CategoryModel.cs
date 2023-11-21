using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicCrudOprations.Models
{
    public class CategoryModel
    {
        [Key]
        [Required]
        [DisplayName("Category ID")]
        public int CategoryID { get; set; }

        [Required]
        [DisplayName("Category Name ")]
        public string CategoryName { get; set; }
    }
}