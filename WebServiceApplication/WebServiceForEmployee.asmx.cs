using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using WebServiceApplication.Models;

namespace WebServiceApplication
{  

    /// <summary>
    /// Сводное описание для WebServiceForEmployee
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceForEmployee : System.Web.Services.WebService
    {
        private MatchingContext db = new MatchingContext();

        
        public class Person
        {
            [XmlAttribute("FirstName")]
            [Required(ErrorMessage = "Не указано имя пользователя.")]
            public string FirstName { get; set; }

            [XmlAttribute("SecondName")]
            [Required(ErrorMessage = "Не указана фамилия пользователя.")]
            public string SecondName { get; set; }

            [XmlAttribute("ThirdName")]
            [Required(ErrorMessage = "Не указано отчество пользователя.")]
            public string ThirdName { get; set; }

            [XmlAttribute("Image")]
            [Required(ErrorMessage = "Необходимо передать base64 сроку.")]
            public string Image { get; set; }
                        
        }
        public struct Device
        {
            [XmlAttribute("IDEmployeе")]            
            public Guid IDEmployeе { get; set; }

            [XmlAttribute("FactoryNumber")]
            public string FactoryNumber { get; set; }

            [XmlAttribute("IDMeteringDevice")]
            public Guid IDMeteringDevice { get; set; }
        }



        [WebMethod]                
        public Employee Authorization(Person person)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(person);
            string Mistake = "";
            if (!Validator.TryValidateObject(person, context, results, true))
            {
                foreach (var error in results)
                {
                    Mistake = Mistake + " " + error.ErrorMessage;
                }
            }

            if (Mistake != "")
            {
                throw new Exception(Mistake);
            }

            Employee employeе = new Employee() { FirstName = person.FirstName, SecondName = person.SecondName, ThirdName = person.ThirdName };
            employeе.FullName = employeе.GetInfo();

            if (person.Image != "")
            {
                employeе.Image = Convert.FromBase64String(person.Image);
            }

            db.Employeеs.Add(employeе);
            db.SaveChanges();

            return employeе;
        }


        [WebMethod]
        public TasksEmployee[] GetTasksToEmployee()
        {       
            try
            {
                TasksEmployee[] tasksEmployees      = db.TasksEmployees.ToArray();
                return tasksEmployees;
            }
            catch
            {
                throw new Exception("Не выполнено");
            }

        }

        [WebMethod]
        public string AddTasksToEmployee(Guid IDEmployeе)
        {     
            try
            {
                TasksEmployee.AddTasksToEmployee(IDEmployeе);                
                return "Выполнено";
            }
            catch
            {
                return "Не выполнено";
            }

        }
          

        [WebMethod]
        public Device AddMeteringDeviceEmployee(Guid IDEmployeе, string FactoryNumber, string FullName)
        {
            string Mistake = "";
            if (FactoryNumber == "")
            {
                Mistake = "Не указан атрибут FactoryNumber.";
            }

            if (FullName == "")
            {
                Mistake = Mistake + " Не указан атрибут FullName.";
            }

            if (Mistake != "")
            {
                throw new Exception(Mistake);
            }

            Device device = new Device();
            try
            {
                MeteringDevice meteringDevice = new MeteringDevice() { FactoryNumber = FactoryNumber, IDMeteringDevice = Guid.NewGuid(), FullName = FullName };
                Employee employee = db.Employeеs.First(p => p.IDEmployeе == IDEmployeе); //FirstOrDefault();
                if (employee != null)
                {
                    meteringDevice.Owner            = employee;
                    db.MeteringDevices.Add(meteringDevice);
                    db.SaveChanges();

                    device.IDEmployeе       = IDEmployeе;
                    device.FactoryNumber    = FactoryNumber;
                    device.IDMeteringDevice = meteringDevice.IDMeteringDevice;
                }
                else
                {
                    throw new Exception("Не найден пользователь с внешним идентификатором: " + IDEmployeе.ToString());
                }

                return device;
            }
            catch
            {
                throw new Exception("Не выполнено");
            }

        }
    }
}
