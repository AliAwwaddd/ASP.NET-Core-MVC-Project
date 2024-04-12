using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ProjectWebRazor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public required string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display ORder must be between 1 and 100")]
        public int DisplayOrder { get; set; }

    }
}

