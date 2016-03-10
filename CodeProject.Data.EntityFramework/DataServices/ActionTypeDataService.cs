using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// Account Data Service
    /// </summary>
    public class ActionTypeDataService : EntityFrameworkService, IActionTypeDataService
    {

        /// <summary>
        /// Update Action Type
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateActionType(ActionType actionType)
        {

        }

        /// <summary>
        /// Create Action Type
        /// </summary>
        /// <param name="customer"></param>
        public void CreateActionType(ActionType actionType)
        {
            dbConnection.ActionTypes.Add(actionType);
        }

        /// <summary>
        /// Get Action Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public ActionType GetActionType(long actionTypeID)
        {
            ActionType actionType = dbConnection.ActionTypes.Where(a => a.Id == actionTypeID).FirstOrDefault();
            return actionType;
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<ActionType> GetActionTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<ActionType> actionTypes = dbConnection.ActionTypes.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return actionTypes;
        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        public void InitializeData()
        {

        }

        /// <summary>
        /// Load Data
        /// </summary>
        public void LoadData()
        {
        }


    }

}

