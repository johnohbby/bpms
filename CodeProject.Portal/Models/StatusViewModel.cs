using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class StatusViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<StatusTranslation> StatusTranslations { get; set; }
        public List<StatusTranslation> StatusTranslations1 { get; set; }
        public List<Status> Statuses { get; set; }
    }
}