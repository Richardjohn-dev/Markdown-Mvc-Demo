using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBlogProject.Models.Common
{
    public class BaseDomainImageEntity : BaseDomainEntity
    {
        // everything that has an image is a base domain entity (updatable)..

        // storing images
        [Display(Name = "Image")]
        public byte[] ImageData { get; set; }

        [Display(Name = "Image Type")]
        public string ContentType { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
