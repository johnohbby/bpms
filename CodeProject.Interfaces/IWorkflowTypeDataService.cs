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
    public interface IWorkflowTypeDataService : IDataRepository, IDisposable
    {
        void CreateWorkflowType(WorkflowType workflowType);
        void UpdateWorkflowType(WorkflowType workflowType);
        WorkflowType GetWorkflowType(long workflowTypeID);
        List<WorkflowType> GetWorkflowTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
          
    }
}

