using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
        public List<CodeProject.Business.Entities.Action> GetActionsForUser(long userId, long workflowId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetActionsForUser", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt, 255).Value = userId;
                    cmd.Parameters.Add("@workflowId", SqlDbType.BigInt).Value = workflowId;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {


                        da.Fill(dt);
                    }
                }
            }
            List<Business.Entities.Action> actions = dt.ToList<Business.Entities.Action>().ToList<Business.Entities.Action>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Workflows.Count();

            // = dbConnection.Workflows.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return actions;
        }

        public List<CodeProject.Business.Entities.User> GetDelegated(long userId, long workflowId, long actionTypeId, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDelegated", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt, 255).Value = userId;
                    cmd.Parameters.Add("@workflowId", SqlDbType.BigInt).Value = workflowId;
                    cmd.Parameters.Add("@actionTypeId", SqlDbType.BigInt).Value = actionTypeId;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {


                        da.Fill(dt);
                    }
                }
            }
            List<Business.Entities.User> actions = dt.ToList<Business.Entities.User>().ToList<Business.Entities.User>();
            totalRows = 0;
            
            totalRows = dbConnection.Workflows.Count();
            
            return actions;
        }


        public List<CodeProject.Business.Entities.ActionType> GetNextActionTypesForUser(long userId, long workflowId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetNextActionTypesForUser", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt, 255).Value = userId;
                    cmd.Parameters.Add("@workflowId", SqlDbType.BigInt).Value = workflowId;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {


                        da.Fill(dt);
                    }
                }
            }
            List<Business.Entities.ActionType> actions = dt.ToList<Business.Entities.ActionType>().ToList<Business.Entities.ActionType>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Workflows.Count();

            // = dbConnection.Workflows.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

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

