using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServiceApplication.Models
{
    public class MeteringDevice : AbstractEntity
    {

        [Required(ErrorMessage = "Не указан владелец")]
        [Display(Name = "Владелец прибора учета")]
        [ForeignKey("OwnerID")]
        public virtual Employee Owner { get; set; }

        [Required(ErrorMessage = "Не указан пользователь")]
        [Display(Name = "Пользователь")]
        [HiddenInput(DisplayValue = false)]
        public int OwnerID { get; set; }

        [Display(Name = "Заводской номер")]
        public string FactoryNumber { get; set; }

        [Required(ErrorMessage = "Не присвоен идентификатор прибора учета")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Внешний идентификатор прибора учета")]
        public Guid IDMeteringDevice { get; set; }
        

        public MeteringDevice()
        {

        }

        public MeteringDevice(string FullName, string FactoryNumber) : base(FullName)
        {
            this.FactoryNumber = FactoryNumber;
        }
    }
}