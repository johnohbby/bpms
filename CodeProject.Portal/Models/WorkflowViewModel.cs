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

        public List<CodeProject.Business.Entities.Action> Actions { get; set; }
        public List<CodeProject.Business.Entities.Action> Actions1 { get; set; }
        public User User { get; set; }
        public WorkflowType WorkflowType { get; set; }
    }
}