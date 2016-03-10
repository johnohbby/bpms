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
    /// Workflow Data Service
    /// </summary>
    public class WorkflowDataService : EntityFrameworkService, IWorkflowDataService
    {

        /// <summary>
        /// Update Workflow
        /// </summary>
        /// <param name="product"></param>
        public void UpdateWorkflow(Workflow workflow)
        {
            
        }

        /// <summary>
        /// Create Workflow
        /// </summary>
        /// <param name="product"></param>
        public void CreateWorkflow(Workflow workflow)
        {
            dbConnection.Workflows.Add(workflow);
        }

        /// <summary>
        /// Get Workflow
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Workflow GetWorkflow(long workflowID)
        {
            Workflow workflow = dbConnection.Workflows.Where(c => c.Id == workflowID).FirstOrDefault();
            return workflow;
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
        public List<Workflow> GetWorkflows(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;
          
            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Products.Count();

            List<Workflow> workflows = dbConnection.Workflows.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return workflows;

        }


    }

}


