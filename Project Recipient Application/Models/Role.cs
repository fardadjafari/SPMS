using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Recipient_Application.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(20, ErrorMessage = "نباید بیش از{1} کاراکتر باشد")]
        public string RoleName { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(20, ErrorMessage = "نباید بیش از{1} کاراکتر باشد")]
        public string RoleTitle { get; set; }

        public virtual ICollection<User> Useres { get; set; }
    }
}