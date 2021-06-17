using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
 public   class Template
    {
     public int    EmailTemplateMasterId     {get;set;}
     public string    TemplateCode              {get;set;}
     public string    EmailSubject              {get;set;}
     public string Body { get; set; }
    }
}
