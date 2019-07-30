using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServiceApplication.Models
{
    public class EmployeesPosition
    {
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Display(Name = "Период")]
        public DateTime Period { get; set; }

        [Required(ErrorMessage = "Не указан пользователь")]
        [Display(Name = "Пользователь")]
        [ForeignKey("EmployeеID")]
        public virtual Employee Employeе { get; set; }

        [Required(ErrorMessage = "Не указан пользователь")]
        [Display(Name = "Пользователь")]
        [HiddenInput(DisplayValue = false)]
        public int EmployeеID { get; set; }

        [Required(ErrorMessage = "Не указана должность")]
        [Display(Name = "Должность")]        
        public Position Position { get; set; }

    }
}