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
        public string Name { get; set; }
        public string CaseNumber { get; set; }
        public long WorkflowTypeId { get; set; }
        public Nullable<long> LastActionId { get; set; }
        public System.DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public List<Workflow> Workflows { get; set; }
    }
}