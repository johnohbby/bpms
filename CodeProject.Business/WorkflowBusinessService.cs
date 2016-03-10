﻿using System;
using System.Collections.Generic;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;
using FluentValidation.Results;

namespace CodeProject.Business
{
    public class WorkflowBusinessService
    {
        private IWorkflowDataService _workflowDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public WorkflowBusinessService(IWorkflowDataService workflowDataService)
        {
            _workflowDataService = workflowDataService;
        }

        /// <summary>
        /// Create Workflow
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Workflow CreateWorkflow(Workflow workflow, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                WorkflowBusinessRules workflowBusinessRules = new WorkflowBusinessRules();
                ValidationResult results = workflowBusinessRules.Validate(workflow);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return workflow;
                }

                _workflowDataService.CreateSession();
                _workflowDataService.BeginTransaction();
                _workflowDataService.CreateWorkflow(workflow);
                _workflowDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Workflow successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _workflowDataService.CloseSession();
            }

            return workflow;


        }

        /// <summary>
        /// Update Workflow
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void UpdateWorkflow(Workflow workflow, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                WorkflowBusinessRules workflowBusinessRules = new WorkflowBusinessRules();
                ValidationResult results = new WorkflowBusinessRules().Validate(workflow);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _workflowDataService.CreateSession();
                _workflowDataService.BeginTransaction();

                Workflow existingWorkflow = _workflowDataService.GetWorkflow(workflow.Id);

                existingWorkflow.Name = workflow.Name;
                existingWorkflow.CaseNumber = workflow.CaseNumber;
                existingWorkflow.WorkflowTypeId = workflow.WorkflowTypeId;
                existingWorkflow.LastActionId = workflow.LastActionId;
                existingWorkflow.IsDeleted = workflow.IsDeleted;
                

                _workflowDataService.UpdateWorkflow(workflow);
                _workflowDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Workflow was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _workflowDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Workflows
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Workflow> GetWorkflows(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<Workflow> workflow = new List<Workflow>();

            try
            {
                int totalRows;

                _workflowDataService.CreateSession();
                workflow = _workflowDataService.GetWorkflows(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _workflowDataService.CloseSession();

                transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
                transaction.TotalRows = totalRows;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _workflowDataService.CloseSession();
            }

            return workflow;

        }


        /// <summary>
        /// Get Workflow
        /// </summary>
        /// <param name="workflowTypeId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Workflow Getworkflow(long workflowId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Workflow workflow = new Workflow();

            try
            {

                _workflowDataService.CreateSession();
                workflow = _workflowDataService.GetWorkflow(workflowId);
                _workflowDataService.CloseSession();      
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _workflowDataService.CloseSession();
            }

            return workflow;

        }






    }
}
