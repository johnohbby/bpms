﻿using System;
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
    public class WorkflowTypeDataService : EntityFrameworkService, IWorkflowTypeDataService
    {

        /// <summary>
        /// Update Workflow Type
        /// </summary>
        /// <param name="product"></param>
        public void UpdateWorkflowType(WorkflowType workflowType)
        {
            
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
        public List<WorkflowType> GetWorkflowTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;
          
            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Products.Count();

            List<WorkflowType> workflowTypes = dbConnection.WorkflowTypes.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return workflowTypes;

        }


    }

}


