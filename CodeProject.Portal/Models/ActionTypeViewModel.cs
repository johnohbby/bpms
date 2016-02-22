using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class ActionTypeViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long StatusTranslationId { get; set; }
        public long WorkflowTypeId { get; set; }

        public ICollection<CodeProject.Business.Entities.Action> Actions { get; set; }
        public statusTranslation StatusTranslation { get; set; }
        public workflowType WorkflowType { get; set; }
    }
}