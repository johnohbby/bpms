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
    public class ActionDataService : EntityFrameworkService, IActionDataService
    {

        /// <summary>
        /// Update Action Type
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateAction(CodeProject.Business.Entities.Action action)
        {

        }

        /// <summary>
        /// Create Action Type
        /// </summary>
        /// <param name="customer"></param>
        public void CreateAction(CodeProject.Business.Entities.Action action)
        {
            dbConnection.Actions.Add(action);
        }

        /// <summary>
        /// Get Action Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CodeProject.Business.Entities.Action GetAction(long actionID)
        {
            CodeProject.Business.Entities.Action action = dbConnection.Actions.Where(a => a.Id == actionID).FirstOrDefault();
            return action;
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
        public List<CodeProject.Business.Entities.Action> GetActions(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Customers.Count();

            List<CodeProject.Business.Entities.Action> actions = dbConnection.Actions.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return actions;
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

