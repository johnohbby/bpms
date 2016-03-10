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
    public interface IActionTypeDataService : IDataRepository, IDisposable
    {
        void CreateActionType(ActionType actionType);
        void UpdateActionType(ActionType actionType);
        ActionType GetActionType(long actionTypeID);
        List<ActionType> GetActionTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
          
    }
}

