using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Action Type Data Service
    /// </summary>
    public interface IActionDataService : IDataRepository, IDisposable
    {
        void CreateAction(CodeProject.Business.Entities.Action action);
        void UpdateAction(CodeProject.Business.Entities.Action action);
        CodeProject.Business.Entities.Action GetAction(long actionID);
        List<CodeProject.Business.Entities.Action> GetActions(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<CodeProject.Business.Entities.Action> GetActionsForUser(long userId, long workflowId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<CodeProject.Business.Entities.ActionType> GetNextActionTypesForUser(long userId, long workflowId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        List<CodeProject.Business.Entities.User> GetDelegated(long userId, long workflowId, long actionTypeId, out int totalRows);

    }
}

