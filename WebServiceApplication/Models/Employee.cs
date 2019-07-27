using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServiceApplication.Models
{
    public class Employee : AbstractEntity
    {
       
        //public string Name { get; set; }

        [Required(ErrorMessage = "Не указано имя пользователя")]
        [Display(Name = "Имя")]
        [XmlAttribute("FirstName")]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия пользователя")]
        [Display(Name = "Фамилия")]
        [XmlAttribute("SecondName")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Не указано отчество пользователя")]
        [Display(Name = "Отчество")]
        [XmlAttribute("ThirdName")]
        public string ThirdName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Не присвоен идентификатор пользователя")]
        [Display(Name = "Внешний идентификатор пользователя")]
        [XmlAttribute("IDEmployeе")]
        
        public Guid IDEmployeе { get; set; }
        public byte[] Image { get; set; }


        [XmlIgnore]
        public ICollection<StatusTasksEmployee> StatusTasksEmployees { get; set; }

        [XmlIgnore]
        public ICollection<MeteringDevice> MeteringDevices { get; set; }

        public Employee(string FirstName, string SecondName, string ThirdName) : base($"{SecondName} {FirstName} {ThirdName}")
        {
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.IDEmployeе = Guid.NewGuid();

            StatusTasksEmployees = new List<StatusTasksEmployee>();
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