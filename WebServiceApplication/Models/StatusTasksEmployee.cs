using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServiceApplication.Models
{
   
    public class StatusTasksEmployee
    {
        private static MatchingContext db = new MatchingContext();

        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int ID { get; set; }

        [Required(ErrorMessage = "Не указан пользователь")]
        [Display(Name = "Пользователь")]
        [ForeignKey("EmployeеID")]
        public virtual Employee Employeе { get; set; }

        [Required(ErrorMessage = "Не указан пользователь")]
        [Display(Name = "Пользователь")]
        [HiddenInput(DisplayValue = false)]
        public int EmployeеID { get; set; }

        [Display(Name = "Задача")]
        [Required(ErrorMessage = "Не указана задача")]
        public TasksEmployee TasksEmployee { get; set; }

        [Display(Name = "Статус")]
        [Required(ErrorMessage = "Не указан статус")]
        public StatusTask StatusTask { get; set; }


        public StatusTasksEmployee()
        {

        }


        public void GetTaskStatuses(Guid IDEmployee)
        {
            //Код для получения статуса задач сотруднику
        }

        public void GetTaskStatuses(Guid IDEmployee, int IDTasks)
        {
            //Код для получения стуса конкретной задачи по сотруднику
        }

        public static void AddTaskStatuses(int id)
        {
            var ListTasks = db.TasksEmployees.ToList();
            Employee employee = db.Employeеs.Find(id);
            foreach (var t in ListTasks)
            {
                db.StatusTasksEmployees.Add(new StatusTasksEmployee { Employeе = employee, EmployeеID = id, StatusTask = StatusTask.NotDone, TasksEmployee = t });
                db.SaveChanges();
            }

        }
    }
}