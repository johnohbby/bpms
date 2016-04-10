using CodeProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeProject.Portal.Models
{
    public class WorkflowDataViewModel : TransactionalInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<WorkflowFolder> WorkflowFolders { get; set; }
        public List<WorkflowType> WorkflowTypes { get; set; }
    }
}