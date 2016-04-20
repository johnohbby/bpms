using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class WorkflowViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String CaseNumber { get; set; }
        public long WorkflowTypeId { get; set; }
        public long LastActionId { get; set; }
        public System.DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public long FolderId { get; set; }
        public long UserId { get; set; }
        public List<Workflow> Workflows { get; set; }
    }
}