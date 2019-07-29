using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;


namespace WebServiceApplication.Models
{
    public enum StatusTask
    {        
        [XmlAttribute("Status")]
        [Display(Name = "Выполнено")]
        Done,
        
        [XmlAttribute("Status")]
        [Display(Name = "Не выполнено")]
        NotDone
    }
     
}