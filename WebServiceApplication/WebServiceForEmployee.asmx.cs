using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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


        //[Serializable]
        //[XmlType]
        //[XmlElement(IsNullable = true)]
        public class Person
        {
            [XmlAttribute("FirstName")]
            [Required(AllowEmptyStrings = true, ErrorMessage = "Не указано имя пользователя.")]
            //[XmlElement(IsNullable = true)]
            public string FirstName { get; set; }

            [XmlAttribute("SecondName")]
            [Required(ErrorMessage = "Не указана фамилия пользователя.")]
           //[XmlElement(IsNullable = false)]
            public string SecondName { get; set; }

            [XmlAttribute("ThirdName")]
            [Required(ErrorMessage = "Не указано отчество пользователя.")]
            //[XmlElement(IsNullable = false)]
            public string ThirdName { get; set; }

            [XmlAttribute("Image")]
            [Required(ErrorMessage = "Необходимо передать base64 сроку.")]
            //[XmlElement(IsNullable = false)]
            public string Image { get; set; }

        }


        [WebMethod(Description = "Авторизация сотрудника и получение внешнего идентификатора('IDEmployeе').")]       
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

        [WebMethod(Description = "Обновление НСИ сотрудника по внешнему идентификатору('IDEmployeе').")]
        public Employee UpdateEmployee(Guid IDEmployeе, Person person)
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

            Employee employee = db.Employeеs.First(p => p.IDEmployeе == IDEmployeе);
            if (employee != null)
            {
                employee.FirstName = person.FirstName;
                employee.SecondName = person.SecondName;
                employee.ThirdName = person.ThirdName;
                employee.FullName = employee.GetInfo();
                employee.Image = Convert.FromBase64String(person.Image);

                db.SaveChanges();
            }
            else
            {
                throw new Exception("Не найден пользователь с внешним идентификатором: " + IDEmployeе.ToString());
            }

            return employee;
        }

        [WebMethod(Description = "Добавление истории должностей сотрудника по внешнему идентификатору('IDEmployeе') в формате json.")]
        public string AddPositionListForEmployeeJson(string Data)
        {
            var DataJson = JObject.Parse(Data);
            Guid IDEmployeе = new Guid((string)DataJson["IDEmployeе"]);

            Employee employee = db.Employeеs.First(p => p.IDEmployeе == IDEmployeе);
            if (employee != null)
            {
                var PositionList = DataJson["PositionList"];
                foreach (var item in PositionList)
                {
                    int selection       = (int)item["Position"];
                    Position position   = Position.Базовый;
                    switch (selection)
                    {
                        case 0:
                            position = Position.Базовый;
                            break;
                        case 1:
                            position = Position.Специалист;
                            break;
                        case 2:
                            position = Position.Эксперт;
                            break;
                        case 3:
                            position = Position.ЭкспертПлюс;
                            break;                        
                    }
                    DateTime Period     = (DateTime)item["Period"];                    
                    db.EmployeesPositions.Add(new EmployeesPosition { Employeе = employee, EmployeеID = employee.ID, Period = Period, Position = position });
                    db.SaveChanges();
                }

            }
            else
            {
                throw new Exception("Не найден пользователь с внешним идентификатором: " + IDEmployeе.ToString());
            }

            return "Выполнено";
        }

        [WebMethod(Description = "Получить список задач.")]
        public TasksEmployee[] GetTasksToEmployee()
        {
            try
            {
                TasksEmployee[] tasksEmployees = db.TasksEmployees.ToArray();
                return tasksEmployees;
            }
            catch
            {
                throw new Exception("Не выполнено");
            }

        }

        [WebMethod(Description = "Регистрация задач сотруднику по внешнему идентификатору('IDEmployeе').")]
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
                    meteringDevice.Owner = employee;
                    db.MeteringDevices.Add(meteringDevice);
                    db.SaveChanges();

                    device.IDEmployeе = IDEmployeе;
                    device.FactoryNumber = FactoryNumber;
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
