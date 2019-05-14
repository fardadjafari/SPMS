using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Recipient_Application.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        public int RoleId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(11, ErrorMessage = "نباید بیش از{1} کاراکتر باشد")]
        public string Username { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(50, ErrorMessage = "نباید بیش از{1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "نام")]
        [MaxLength(30, ErrorMessage = "نباید بیش از{1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(50, ErrorMessage = "نباید بیش از{1} کاراکتر باشد")]
        public string Family { get; set; }


        [Display(Name = "مجوز فعالیت")]
        public bool Activeate { get; set; }


        [Display(Name = "سطح دسترسی کامل ")]
        public bool fullpermision { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}