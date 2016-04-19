using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class FormViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }
       
        public string TableName { get; set; }

        public long  ActionTypeId { get; set; }

        public List<FormField> FormFields { get; set; }
        public Object Forms { get; set; }
    }
}