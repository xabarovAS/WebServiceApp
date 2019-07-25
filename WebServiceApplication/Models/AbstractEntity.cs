using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace WebServiceApplication.Models
{
    public abstract class AbstractEntity
    {
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlAttribute("ID")]
        [XmlIgnore]
        public int ID { get; set; }

        [Display(Name = "Наименование")]
        [XmlAttribute("FullName")]
        public string FullName { get; set; }


        public AbstractEntity()
        {
        }
        public AbstractEntity(string FullName)
        {
            this.FullName = FullName;
        }
    }
}