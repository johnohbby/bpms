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
        public string Type { get; set; }
        public long FolderId { get; set; }
        public long ParentDocumentId { get; set; }
        public DateTime Created { get; set; }

        public List<Document> Documents { get; set; }
    }
}