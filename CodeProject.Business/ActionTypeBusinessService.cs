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
    public class ActionTypeBusinessService
    {
        private IActionTypeDataService _actionTypeDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionTypeBusinessService(IActionTypeDataService actionTypeDataService)
        {
            _actionTypeDataService = actionTypeDataService;
        }

        /// <summary>
        /// Create Action Type
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ActionType CreateActionType(ActionType actionType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ActionTypeBusinessRules actionTypeBusinessRules = new ActionTypeBusinessRules();
                ValidationResult results = actionTypeBusinessRules.Validate(actionType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return actionType;
                }

                _actionTypeDataService.CreateSession();
                _actionTypeDataService.BeginTransaction();
                _actionTypeDataService.CreateActionType(actionType);
                _actionTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Customer successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _actionTypeDataService.CloseSession();
            }

            return actionType;


        }

        /// <summary>
        /// Update Action Type
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateActionType(ActionType actionType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                ActionTypeBusinessRules actionTypeBusinessRules = new ActionTypeBusinessRules();
                ValidationResult results = actionTypeBusinessRules.Validate(actionType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _actionTypeDataService.CreateSession();
                _actionTypeDataService.BeginTransaction();

                ActionType existingActionType = _actionTypeDataService.GetActionType(actionType.Id);

                existingActionType.Id = actionType.Id;
                existingActionType.Name = actionType.Name;
                existingActionType.WorkflowTypeId = actionType.WorkflowTypeId;

                _actionTypeDataService.UpdateActionType(actionType);
                _actionTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Action Type was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _actionTypeDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Action Types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<ActionType> GetActionTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<ActionType> actionTypes = new List<ActionType>();

            try
            {
                int totalRows;

                _actionTypeDataService.CreateSession();
                actionTypes = _actionTypeDataService.GetActionTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _actionTypeDataService.CloseSession();

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
                _actionTypeDataService.CloseSession();
            }

            return actionTypes;

        }

        /// <summary>
        /// Get Action Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ActionType GetActionType(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            ActionType actionType = new ActionType();

            try
            {

                _actionTypeDataService.CreateSession();
                actionType = _actionTypeDataService.GetActionType(id);
                _actionTypeDataService.CloseSession();      
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
                _actionTypeDataService.CloseSession();
            }

            return actionType;

        }

        /// <summary>
        /// Delete Right type
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteActionType(ActionType actionType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                ActionTypeBusinessRules actionTypeBusinessRules = new ActionTypeBusinessRules();
                ValidationResult results = actionTypeBusinessRules.Validate(actionType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _actionTypeDataService.CreateSession();
                _actionTypeDataService.BeginTransaction();

                _actionTypeDataService.DeleteActionType(actionType);
                _actionTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Action type was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _actionTypeDataService.CloseSession();
            }


        }


        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            ActionType actionType = new ActionType();

            try
            {

                _actionTypeDataService.CreateSession();
                _actionTypeDataService.BeginTransaction();
                _actionTypeDataService.CommitTransaction(true);
                _actionTypeDataService.CloseSession();

                _actionTypeDataService.CreateSession();
                _actionTypeDataService.BeginTransaction();
                _actionTypeDataService.CommitTransaction(true);
                _actionTypeDataService.CloseSession();

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
                _actionTypeDataService.CloseSession();
            }           

        }




    }
}
