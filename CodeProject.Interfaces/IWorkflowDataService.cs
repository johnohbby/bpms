using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Product Data Service
    /// </summary>
    public interface IWorkflowDataService : IDataRepository, IDisposable
    {
        void CreateWorkflow(Workflow workflow);
        void UpdateWorkflow(Workflow workflow);
        Workflow GetWorkflow(long workflowID);
        List<Workflow> GetWorkflows(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
          
    }
}

