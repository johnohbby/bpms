using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

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
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ActionTypeUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = actionType.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = actionType.Name;
                    cmd.Parameters.Add("@workflowTypeId", SqlDbType.BigInt).Value = actionType.WorkflowTypeId;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

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
        /// Delete Action Type
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteActionType(ActionType actionType)
        {
            dbConnection.ActionTypes.Attach(actionType);
            dbConnection.ActionTypes.Remove(actionType);
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

