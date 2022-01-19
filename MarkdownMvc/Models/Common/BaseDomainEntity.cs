using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBlogProject.Models.Common
{
    public class BaseDomainEntity
    {
        //Base fields that every class/domain entity will need
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
       
        [DataType(DataType.Date)]
        [Display(Name = "Date Updated")]
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
