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
    public class StatusTranslationBusinessService
    {
        private IStatusTranslationDataService _statusTranslationDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public StatusTranslationBusinessService(IStatusTranslationDataService statusTranslationDataService)
        {
            _statusTranslationDataService = statusTranslationDataService;
        }

        /// <summary>
        /// Create Status Translation
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public StatusTranslation CreateStatusTranslation(StatusTranslation statusTranslation, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                StatusTranslationBusinessRules statusTranslationBusinessRules = new StatusTranslationBusinessRules();
                ValidationResult results = statusTranslationBusinessRules.Validate(statusTranslation);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return statusTranslation;
                }

                _statusTranslationDataService.CreateSession();
                _statusTranslationDataService.BeginTransaction();
                _statusTranslationDataService.CreateStatusTranslation(statusTranslation);
                _statusTranslationDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Status Translation successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _statusTranslationDataService.CloseSession();
            }

            return statusTranslation;


        }

        /// <summary>
        /// Update Status Translation
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateStatusTranslation(StatusTranslation statusTranslation, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                StatusTranslationBusinessRules statusTranslationBusinessRules = new StatusTranslationBusinessRules();
                ValidationResult results = statusTranslationBusinessRules.Validate(statusTranslation);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _statusTranslationDataService.CreateSession();
                _statusTranslationDataService.BeginTransaction();

                StatusTranslation existingStatusTranslation = _statusTranslationDataService.GetStatusTranslation(statusTranslation.Id);

                existingStatusTranslation.Id = statusTranslation.Id;
                existingStatusTranslation.StatusIdFrom = statusTranslation.StatusIdFrom;
                existingStatusTranslation.StatusIdTo = statusTranslation.StatusIdTo;
                existingStatusTranslation.ActionTypeId = statusTranslation.ActionTypeId;

                _statusTranslationDataService.UpdateStatusTranslation(statusTranslation);
                _statusTranslationDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Status Translation was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _statusTranslationDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Status Translations
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<StatusTranslation> GetStatusTranslations(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<StatusTranslation> statusTranslations = new List<StatusTranslation>();

            try
            {
                int totalRows;

                _statusTranslationDataService.CreateSession();
                statusTranslations = _statusTranslationDataService.GetStatusTranslations(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _statusTranslationDataService.CloseSession();

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
                _statusTranslationDataService.CloseSession();
            }

            return statusTranslations;

        }

        /// <summary>
        /// Get Status Translation
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public StatusTranslation GetStatusTranslation(long id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            StatusTranslation statusTranslation = new StatusTranslation();

            try
            {

                _statusTranslationDataService.CreateSession();
                statusTranslation = _statusTranslationDataService.GetStatusTranslation(id);
                _statusTranslationDataService.CloseSession();      
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
                _statusTranslationDataService.CloseSession();
            }

            return statusTranslation;

        }

        /// Delete Status Translation
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteStatusTranslation(StatusTranslation statusTranslation, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                StatusTranslationBusinessRules StatusTranslationBusinessRules = new StatusTranslationBusinessRules();
                ValidationResult results = StatusTranslationBusinessRules.Validate(statusTranslation);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _statusTranslationDataService.CreateSession();
                _statusTranslationDataService.BeginTransaction();

                _statusTranslationDataService.DeleteStatusTranslation(statusTranslation);
                _statusTranslationDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Status Translation was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _statusTranslationDataService.CloseSession();
            }


        }

        /// <summary>
        /// GetStatusTranslationsForActionType
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<StatusTranslation> GetStatusTranslationsForActionType(long actionTypeId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<StatusTranslation> statusTranslations = new List<StatusTranslation>();

            try
            {
                int totalRows;

                _statusTranslationDataService.CreateSession();
                statusTranslations = _statusTranslationDataService.GetStatusTranslationsForActionType(actionTypeId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _statusTranslationDataService.CloseSession();

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
                _statusTranslationDataService.CloseSession();
            }

            return statusTranslations;

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Status status = new Status();

            try
            {

                _statusTranslationDataService.CreateSession();
                _statusTranslationDataService.BeginTransaction();
                _statusTranslationDataService.CommitTransaction(true);
                _statusTranslationDataService.CloseSession();

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
                _statusTranslationDataService.CloseSession();
            }           

        }




    }
}
