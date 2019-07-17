using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebServiceApplication.Models
{
    public class Employee : AbstractEntity
    {
       
        //public string Name { get; set; }

        [Required(ErrorMessage = "Не указано имя пользователя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия пользователя")]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Не указано отчество пользователя")]
        [Display(Name = "Отчество")]
        public string ThirdName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Не присвоен идентификатор пользователя")]
        [Display(Name = "Внешний идентификатор пользователя")]
        public Guid IDEmployeе { get; set; }



        public Employee(string FirstName, string SecondName, string ThirdName) : base($"{SecondName} {FirstName} {ThirdName}")
        {
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.IDEmployeе = Guid.NewGuid();
        }

        public Employee()
        {
            this.FullName = this.GetInfo();
            this.IDEmployeе = Guid.NewGuid();
        }

        public string GetInfo()
        {
            return $"{this.SecondName} {this.FirstName} {this.ThirdName}";
        }
    }
}