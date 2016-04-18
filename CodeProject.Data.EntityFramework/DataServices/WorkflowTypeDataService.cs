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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// Account Data Service
    /// </summary>
    public class WorkflowTypeDataService : EntityFrameworkService, IWorkflowTypeDataService
    {

        /// <summary>
        /// Update Workflow Type
        /// </summary>
        /// <param name="product"></param>
        public void UpdateWorkflowType(WorkflowType workflowType)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("WorkflowTypeUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = workflowType.Id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = workflowType.Name;
                    cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = workflowType.Description;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        /// <summary>
        /// Create Workflow Type
        /// </summary>
        /// <param name="product"></param>
        public void CreateWorkflowType(WorkflowType workflowType)
        {
            dbConnection.WorkflowTypes.Add(workflowType);
        }

        /// <summary>
        /// Get Workflow Type
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public WorkflowType GetWorkflowType(long workflowTypeID)
        {
            WorkflowType workflowType = dbConnection.WorkflowTypes.Where(c => c.Id == workflowTypeID).FirstOrDefault();
            return workflowType;
        }

        /// <summary>
        /// Get Workflow Types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<WorkflowType> GetWorkflowTypes(long UserId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetWorkflowTypesForUser", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = UserId;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<WorkflowType> workflows = dt.ToList<WorkflowType>().ToList<WorkflowType>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Workflows.Count();
            
            return workflows;
        }

        public List<WorkflowType> GetAllWorkflowTypes( int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetWorkflowTypes", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<WorkflowType> workflows = dt.ToList<WorkflowType>().ToList<WorkflowType>();
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Workflows.Count();

            return workflows;
        }

        /// <summary>
        /// Delete Workflow Type
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteWorkflowType(WorkflowType workflowType)
        {
            dbConnection.WorkflowTypes.Attach(workflowType);
            dbConnection.WorkflowTypes.Remove(workflowType);
        }

    }

}


