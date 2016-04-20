using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class FormFieldViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }
       
        public long FormId { get; set; }

        public String  FieldType { get; set; }

        public List<FormField> FormFields { get; set; }
    }
}