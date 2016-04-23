using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class EmailViewModel : TransactionalInformation
    {
        public String To { get; set; }
        public String From { get; set; }
        public String MailBody { get; set; }
        public String Subject { get; set; }
    }
}