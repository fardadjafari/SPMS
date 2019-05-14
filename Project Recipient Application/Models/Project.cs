using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Recipient_Application.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }


        public int SturId { get; set; }


        [Display(Name = "موضوع پروژه")]
        [MaxLength(50, ErrorMessage = "نباید بیش از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "ماهیت فایل")]
        [MaxLength(50, ErrorMessage = "نباید بیش از {1} کاراکتر باشد")]
        public string sortFile { get; set; }

        [Display(Name = "فایل پروژه")]
        public string Urlfile { get; set; }

        [Display(Name = "تاریخ ارسال")]
        public DateTime DateSend { get; set; }

        [ForeignKey("SturId")]
        public virtual Student Studentses { get; set; }

      
    }
}