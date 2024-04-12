using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project.Models
{
    public class Temp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required String Name { get; set; }

    }
}

