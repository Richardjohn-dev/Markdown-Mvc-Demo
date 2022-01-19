using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBlogProject.Models.Common;

namespace TheBlogProject.Models
{
    public class Post : BaseDomainImageEntity
    {

        [Display(Name = "Blog Name")]
        public int CategoryId { get; set; }
        public string BlogUserId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string Abstract { get; set; }
    
        
        [Required]
        public string Content { get; set; }

       // public ReadyStatus ReadyStatus { get; set; }

        public string Slug { get; set; }


        public bool FeaturedPost { get; set; }
        public bool CommentsEnabled { get; set; }
        public int? TimeToRead { get; set; }       

        // Navigation Properties
       // public virtual Category Category { get; set; }
       // public virtual BlogUser BlogUser { get; set; } // Author is the parent

       //// public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>(); 

       // public virtual ICollection<MainComment> Comments { get; set; } = new HashSet<MainComment>();

       // public virtual ICollection<Series> Series { get; set; } = new HashSet<Series>(); // Skip Collection
       // public virtual ICollection<PostSeries> PostSeries { get; set; } = new HashSet<PostSeries>();  // Collection navigation
       // public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();  // Collection navigation
    }


}

