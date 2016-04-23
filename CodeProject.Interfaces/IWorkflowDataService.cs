using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Workflow Data Service
    /// </summary>
    public interface IWorkflowDataService : IDataRepository, IDisposable
    {
        void CreateWorkflow(Workflow workflow);
        void UpdateWorkflow(Workflow workflow);
        void DeleteWorkflow(Workflow workflow);
        void DeleteAction(Business.Entities.Action action);
        Workflow GetWorkflow(long workflowID);
        List<Workflow> GetWorkflows(long folderId, long UserId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<WorkflowFolder> GetWorkflowFolders(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        long CreateAction(List<User> delegatedTo, Business.Entities.Action action);
    }
}

