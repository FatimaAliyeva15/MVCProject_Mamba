﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba_Core.Models
{
    public class Employee: BaseEntity
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Fullname { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Position { get; set; }
        public string? ImgUrl { get; set; } = null!;
        [NotMapped]
        public IFormFile? ImgFile { get; set; } = null!;
    }
}
