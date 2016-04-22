using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class DocumentViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameOnServer { get; set; }
        public string Extension { get; set; }
        public long ContentId { get; set; }
        public long ParentDocumentId { get; set; }
        public DateTime Created { get; set; }
        public long ContentTypeId { get; set; }

        public List<Document> Documents { get; set; }
    }
}