﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebServiceApplication.Models
{
    public class MeteringDevice : AbstractEntity
    {

        [Required(ErrorMessage = "Не указан владелец")]
        [Display(Name = "Владелец прибора учета")]
        public Employee Owner { get; set; }

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