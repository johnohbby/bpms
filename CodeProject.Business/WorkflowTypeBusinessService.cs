using System;
using System.Collections.Generic;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;
using FluentValidation.Results;

namespace CodeProject.Business
{
    public class WorkflowTypeBusinessService
    {
        private IWorkflowTypeDataService _workflowTypeDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public WorkflowTypeBusinessService(IWorkflowTypeDataService workflowTypeDataService)
        {
            _workflowTypeDataService = workflowTypeDataService;
        }

        /// <summary>
        /// Create WorkflowType
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public WorkflowType CreateWorkflowType(WorkflowType workflowType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                WorkflowTypeBusinessRules workflowTypeBusinessRules = new WorkflowTypeBusinessRules();
                ValidationResult results = workflowTypeBusinessRules.Validate(workflowType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return workflowType;
                }

                _workflowTypeDataService.CreateSession();
                _workflowTypeDataService.BeginTransaction();
                _workflowTypeDataService.CreateWorkflowType(workflowType);
                _workflowTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Workflow type successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _workflowTypeDataService.CloseSession();
            }

            return workflowType;


        }

        /// <summary>
        /// Update Workflow type
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void UpdateWorkflowType(WorkflowType workflowType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                WorkflowTypeBusinessRules workflowTypeBusinessRules = new WorkflowTypeBusinessRules();
                ValidationResult results = new WorkflowTypeBusinessRules().Validate(workflowType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _workflowTypeDataService.CreateSession();
                _workflowTypeDataService.BeginTransaction();

                WorkflowType existingWorkflowType = _workflowTypeDataService.GetWorkflowType(workflowType.Id);

                existingWorkflowType.Name = workflowType.Name;
                existingWorkflowType.Description = workflowType.Description;
                

                _workflowTypeDataService.UpdateWorkflowType(workflowType);
                _workflowTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Workflow type was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _workflowTypeDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Workflow types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<WorkflowType> GetWorkflowTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<WorkflowType> workflowType = new List<WorkflowType>();

            try
            {
                int totalRows;

                _workflowTypeDataService.CreateSession();
                workflowType = _workflowTypeDataService.GetWorkflowTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _workflowTypeDataService.CloseSession();

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
                _workflowTypeDataService.CloseSession();
            }

            return workflowType;

        }


        /// <summary>
        /// Get Workflow Type
        /// </summary>
        /// <param name="workflowTypeId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public WorkflowType GetworkflowType(long workflowTypeId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            WorkflowType workflowType = new WorkflowType();

            try
            {

                _workflowTypeDataService.CreateSession();
                workflowType = _workflowTypeDataService.GetWorkflowType(workflowTypeId);
                _workflowTypeDataService.CloseSession();      
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
                _workflowTypeDataService.CloseSession();
            }

            return workflowType;

        }


        /// <summary>
        /// Delete Workflow type
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteWorkflowType(WorkflowType workflowType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                WorkflowTypeBusinessRules workflowTypeBusinessRules = new WorkflowTypeBusinessRules();
                ValidationResult results = workflowTypeBusinessRules.Validate(workflowType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _workflowTypeDataService.CreateSession();
                _workflowTypeDataService.BeginTransaction();

                _workflowTypeDataService.DeleteWorkflowType(workflowType);
                _workflowTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Right type was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _workflowTypeDataService.CloseSession();
            }


        }



    }
}

