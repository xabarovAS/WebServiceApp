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
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Не указан пользователь")]
        [Display(Name = "Пользователь")]
        public Employee Employeе { get; set; }

        [Display(Name = "Задача")]
        [Required(ErrorMessage = "Не указана задача")]
        public TasksEmployee TasksEmployee { get; set; }

        [Display(Name = "Статус")]
        [Required(ErrorMessage = "Не указан статус")]
        public StatusTask StatusTask { get; set; }


        public void GetTaskStatuses(Guid IDEmployee)
        {
            //Код для получения статуса задач сотруднику
        }

        public void GetTaskStatuses(Guid IDEmployee, int IDTasks)
        {
            //Код для получения стуса конкретной задачи по сотруднику
        }
    }
}