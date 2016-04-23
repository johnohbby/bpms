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
    /// Workflow Data Service
    /// </summary>
    public class WorkflowDataService : EntityFrameworkService, IWorkflowDataService
    {
        #region WORKFLOWS
        /// <summary>
        /// Update Workflow
        /// </summary>
        /// <param name="product"></param>
        public void UpdateWorkflow(Workflow workflow)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                sqlcon.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Workflow_Update", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = workflow.Id;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = workflow.Name;
                    
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                }
            }
        }

        /// <summary>
        /// Create Workflow
        /// </summary>
        /// <param name="product"></param>
        public void CreateWorkflow(Workflow workflow)
        {
            //dbConnection.Workflows.Add(workflow);
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                sqlcon.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Workflow_Create", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = workflow.Name;
                    cmd.Parameters.Add("@workflowTypeId", SqlDbType.BigInt).Value = workflow.WorkflowTypeId;
                    cmd.Parameters.Add("@createdBy", SqlDbType.BigInt).Value = workflow.CreatedBy;
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                }
            }
        }
        public void DeleteWorkflow(Workflow workflow)
        {
            //dbConnection.Workflows.Add(workflow);
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                sqlcon.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Workflow_Delete", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = workflow.Id;
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                }
            }
        }

        public void DeleteAction(Business.Entities.Action action)
        {
            //dbConnection.Workflows.Add(workflow);
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                sqlcon.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Action_Delete", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = action.Id;
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                }
            }
        }
        /// <summary>
        /// Get Workflow
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Workflow GetWorkflow(long workflowID)
        {
            DataTable dt = new DataTable();
            
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetWorkflowById", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = workflowID;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            List<Workflow> workflows = dt.ToList<Workflow>().ToList<Workflow>();
         
            return workflows[0];
        }

        /// <summary>
        /// Get Workflows
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Workflow> GetWorkflows(long folderId, long userId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            DataTable dt = new DataTable();
            WorkflowFolder wf = GetWorkfloFolder(folderId);
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(wf.Procedure, sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = userId;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        

                        da.Fill(dt);
                    }
                }
            }
            List<Workflow> workflows = dt.ToList<Workflow>().ToList<Workflow>();
            totalRows = 0;
          
            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Workflows.Count();

            // = dbConnection.Workflows.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return workflows;

        }

        public long CreateAction(List<User> delegatedTo, Business.Entities.Action action)
        {
            //dbConnection.Workflows.Add(workflow);
            long retunvalue = -1;
            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["CodeProjectDatabase"].ConnectionString))
            {
                sqlcon.Open();
                
                for (int i = 0; i < delegatedTo.Count; i++ )
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.Action_Create", sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@workflowId", SqlDbType.NVarChar, 255).Value = action.WorkflowId;
                        cmd.Parameters.Add("@actionTypeId", SqlDbType.BigInt).Value = action.ActionTypeId;
                        cmd.Parameters.Add("@createdBy", SqlDbType.BigInt).Value = action.CreatedBy;
                        cmd.Parameters.Add("@delegatedId", SqlDbType.BigInt).Value = delegatedTo[i].Id;

                        SqlParameter pvNewId = new SqlParameter();
                        pvNewId.ParameterName = "@insertedId";
                        pvNewId.DbType = DbType.Int64;
                        pvNewId.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pvNewId);
                        cmd.ExecuteNonQuery();
                        retunvalue = (long)cmd.Parameters["@insertedId"].Value;

                    }
                }
               
                sqlcon.Close();
            }

            return retunvalue;
        }
        #endregion

        #region WORKFLOW FOLDERS
        public List<WorkflowFolder> GetWorkflowFolders(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Workflows.Count();

            List<WorkflowFolder> workflows = dbConnection.WorkflowFolders.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return workflows;

        }

        public WorkflowFolder GetWorkfloFolder(long workflowFolderID)
        {
            WorkflowFolder workflowFolder = dbConnection.WorkflowFolders.Where(c => c.Id == workflowFolderID).FirstOrDefault();
            return workflowFolder;
        }

        #endregion


    }

}


