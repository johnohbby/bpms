using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class MailTemplateMapViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public long MailTemplateId { get; set; }
        public long ActionTypeId { get; set; }

        public List<MailTemplateMap> MailTemplateMaps { get; set; }
    }
}