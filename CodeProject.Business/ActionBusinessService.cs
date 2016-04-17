using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;

using FluentValidation;
using FluentValidation.Results;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net.Mail;

namespace CodeProject.Business
{
    public class ActionBusinessService
    {
        private IActionDataService _actionDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionBusinessService(IActionDataService actionDataService)
        {
            _actionDataService = actionDataService;
        }

        /// <summary>
        /// Create Action
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public CodeProject.Business.Entities.Action CreateAction(CodeProject.Business.Entities.Action action, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ActionBusinessRules actionBusinessRules = new ActionBusinessRules();
                ValidationResult results = actionBusinessRules.Validate(action);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return action;
                }

                _actionDataService.CreateSession();
                _actionDataService.BeginTransaction();
                _actionDataService.CreateAction(action);
                _actionDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Action successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _actionDataService.CloseSession();
            }

            return action;


        }

        /// <summary>
        /// Update Action
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateAction(CodeProject.Business.Entities.Action action, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                ActionBusinessRules actionBusinessRules = new ActionBusinessRules();
                ValidationResult results = actionBusinessRules.Validate(action);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _actionDataService.CreateSession();
                _actionDataService.BeginTransaction();

                CodeProject.Business.Entities.Action existingAction = _actionDataService.GetAction(action.Id);

                existingAction.Id = action.Id;
                existingAction.ActionTypeId = action.ActionTypeId;
                existingAction.WorkflowId = action.WorkflowId;
                existingAction.IsDeleted = action.IsDeleted;

                _actionDataService.UpdateAction(action);
                _actionDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Action was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _actionDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Action
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<CodeProject.Business.Entities.Action> GetActions(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<CodeProject.Business.Entities.Action> actions = new List<CodeProject.Business.Entities.Action>();

            try
            {
                int totalRows;

                _actionDataService.CreateSession();
                actions = _actionDataService.GetActions(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _actionDataService.CloseSession();

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
                _actionDataService.CloseSession();
            }

            return actions;

        }
        public List<CodeProject.Business.Entities.Action> GetActionsForUser(long userId, long workflowId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<CodeProject.Business.Entities.Action> actions = new List<CodeProject.Business.Entities.Action>();

            try
            {
                int totalRows;

                _actionDataService.CreateSession();
                actions = _actionDataService.GetActionsForUser(userId, workflowId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _actionDataService.CloseSession();

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
                _actionDataService.CloseSession();
            }

            return actions;

        }
        public List<CodeProject.Business.Entities.ActionType> GetNextActionTypesForUser(long userId, long workflowId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<CodeProject.Business.Entities.ActionType> actions = new List<CodeProject.Business.Entities.ActionType>();

            try
            {
                int totalRows;

                _actionDataService.CreateSession();
                actions = _actionDataService.GetNextActionTypesForUser(userId, workflowId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _actionDataService.CloseSession();

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
                _actionDataService.CloseSession();
            }

            return actions;

        }

        public List<CodeProject.Business.Entities.User> GetDelegated(long userId, long workflowId, long actionTypeId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<CodeProject.Business.Entities.User> actions = new List<CodeProject.Business.Entities.User>();

            try
            {
                int totalRows;

                _actionDataService.CreateSession();
                actions = _actionDataService.GetDelegated(userId, workflowId, actionTypeId, out totalRows);
                _actionDataService.CloseSession();

                
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
                _actionDataService.CloseSession();
            }

            return actions;

        }
        /// <summary>
        /// Get Action
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public CodeProject.Business.Entities.Action GetAction(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            CodeProject.Business.Entities.Action action = new CodeProject.Business.Entities.Action();

            try
            {

                _actionDataService.CreateSession();
                action = _actionDataService.GetAction(id);
                _actionDataService.CloseSession();      
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
                _actionDataService.CloseSession();
            }

            return action;

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Customer customer = new Customer();

            try
            {

                _actionDataService.CreateSession();
                _actionDataService.BeginTransaction();
                _actionDataService.CommitTransaction(true);
                _actionDataService.CloseSession();

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
                _actionDataService.CloseSession();
            }           

        }




    }
}
