using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class MailTemplateViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }

        public List<MailTemplate> MailTemplates { get; set; }
    }
}