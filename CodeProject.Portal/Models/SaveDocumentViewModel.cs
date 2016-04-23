using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class SaveDocumentViewModel : TransactionalInformation
    {
       public long ContentId { get; set; }
        public long ParentDocumentId { get; set; }

        public List<DocumentViewModel> Documents { get; set; }
    }
}