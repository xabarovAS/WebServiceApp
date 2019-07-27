using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebServiceApplication.Models
{
    public enum StatusTask
    {
        [Display(Name = "Выполнено")]
        [XmlAttribute("Status")]
        Done,

        [Display(Name = "Не выполнено")]
        [XmlAttribute("Status")]
        NotDone
    }
}