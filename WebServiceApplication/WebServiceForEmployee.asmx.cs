using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
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

        [WebMethod]
        //[SoapHeader("FirstName")]
        public Employee Authorization(string FirstName, string SecondName, string ThirdName)
        {
            string Mistake = "";
            if (FirstName == "")
            {
                Mistake = "Не указан атрибут FirstName."; 
            }
            if (SecondName == "")
            {
                Mistake = Mistake + " Не указан атрибут SecondName.";
            }
            if (ThirdName == "")
            {
                Mistake = Mistake + " Не указан атрибут ThirdName.";
            }

            if (Mistake != "")
            {
                throw new Exception(Mistake);
            }

            Employee employeе = new Employee() { FirstName = FirstName, SecondName = SecondName, ThirdName = ThirdName };
            db.Employeеs.Add(employeе);

            return employeе;
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
        public MeteringDevice AddMeteringDeviceEmployee(Guid IDEmployeе, MeteringDevice meteringDevice)
        {
            try
            {
                Employee employee = db.Employeеs.FirstOrDefault(p => p.IDEmployeе == IDEmployeе);
                if (employee != null)
                {
                    meteringDevice.Owner = employee;
                    db.MeteringDevices.Add(meteringDevice);
                }
                else
                {
                    throw new Exception("Не найден пользователь с внешним идентификатором: " + IDEmployeе.ToString());
                }
                return meteringDevice;
            }
            catch
            {
                throw new Exception("Не выполнено");
            }

        }
    }
}
