using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Recipient_Application.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(30, ErrorMessage = "نباید بیش از{1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(50, ErrorMessage = "نباید بیش از{1} کاراکتر باشد")]
        public string Family { get; set; }

        [Display(Name = "کد دانشجویی")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(50, ErrorMessage = "نباید بیش از {1} کاراکتر باشد")]
        public string stuCode { get; set; }

        [Display(Name = "رشته دانشجو")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(50, ErrorMessage = "نباید بیش از {1} کاراکتر باشد")]
        public string Curse { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}