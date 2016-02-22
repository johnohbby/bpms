using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class ActionViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public long ActionTypeId { get; set; }
        public long WorkflowId { get; set; }
        public System.DateTime Created { get; set; }
        public long CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public ActionType ActionType { get; set; }
        public user User { get; set; }
        public workflow Workflow { get; set; }
        public workflow Workflow1 { get; set; }
    }
}