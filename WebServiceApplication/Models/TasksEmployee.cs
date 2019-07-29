using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace WebServiceApplication.Models
{
    public class TasksEmployee : AbstractEntity
    {
        [Required(ErrorMessage = "Не указано описание задачи")]
        [Display(Name = "Описание")]
        [XmlAttribute("Description")]
        public string Description { get; set; }


        public TasksEmployee(string Description, string FullName) : base(FullName)
        {
            this.Description = Description;
        }

        public TasksEmployee()
        {
            
        }

        public static void AddTasksToEmployee(Guid IDEmployee)
        {
            using (MatchingContext db = new MatchingContext())
            {
                Employee employee = db.Employeеs.FirstOrDefault(p => p.IDEmployeе == IDEmployee);
                if (employee != null)
                {
                    var tasksEmployee                                   = db.TasksEmployees.ToList();
                    StatusTask statusTask                               = StatusTask.NotDone;
                    StatusTask statusDone                               = StatusTask.Done;
                    List<StatusTasksEmployee> ListStatusTasksEmployee   = new List<StatusTasksEmployee>();

                    int i = 1;
                    foreach (var t in tasksEmployee)
                    {
                        StatusTasksEmployee statusTasksEmployee = new StatusTasksEmployee() { EmployeеID = employee.ID, Employeе = employee, StatusTask = statusTask, TasksEmployee = t };
                        
                        if (i == 1 ^ i == 2 ^ i == 3)
                        {
                            statusTasksEmployee.StatusTask = statusDone;
                        }
                        ListStatusTasksEmployee.Add(statusTasksEmployee);
                        i = i++;
                    }

                    db.StatusTasksEmployees.AddRange(ListStatusTasksEmployee);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Не найден пользователь с внешним идентификатором: " + IDEmployee.ToString());
                }

            }
        }
    }
}