using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class ContentFormMapViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public long FormId { get; set; }
        public long ContentTypeId { get; set; }
        public long ContentId { get; set; }

        public List<ContentFormMap> ContentFormMaps { get; set; }
    }
}