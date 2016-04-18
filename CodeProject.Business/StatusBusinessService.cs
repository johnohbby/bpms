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
    public class StatusBusinessService
    {
        private IStatusDataService _statusDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public StatusBusinessService(IStatusDataService statusDataService)
        {
            _statusDataService = statusDataService;
        }

        /// <summary>
        /// Create Status
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public CodeProject.Business.Entities.Status CreateStatus(CodeProject.Business.Entities.Status status, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                StatusBusinessRules statusBusinessRules = new StatusBusinessRules();
                ValidationResult results = statusBusinessRules.Validate(status);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return status;
                }

                _statusDataService.CreateSession();
                _statusDataService.BeginTransaction();
                _statusDataService.CreateStatus(status);
                _statusDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Status successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _statusDataService.CloseSession();
            }

            return status;


        }

        /// <summary>
        /// Update Status
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateStatus(CodeProject.Business.Entities.Status status, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                StatusBusinessRules statusBusinessRules = new StatusBusinessRules();
                ValidationResult results = statusBusinessRules.Validate(status);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _statusDataService.CreateSession();
                _statusDataService.BeginTransaction();

                CodeProject.Business.Entities.Status existingStatus = _statusDataService.GetStatus(status.Id);

                existingStatus.Id = status.Id;
                existingStatus.Name = status.Name;

                _statusDataService.UpdateStatus(status);
                _statusDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Status was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _statusDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Statuses
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<CodeProject.Business.Entities.Status> GetStatuses(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<CodeProject.Business.Entities.Status> statuses = new List<CodeProject.Business.Entities.Status>();

            try
            {
                int totalRows;

                _statusDataService.CreateSession();
                statuses = _statusDataService.GetStatuses(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _statusDataService.CloseSession();

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
                _statusDataService.CloseSession();
            }

            return statuses;

        }

        /// <summary>
        /// Get Status
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public CodeProject.Business.Entities.Status GetStatus(long id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            CodeProject.Business.Entities.Status status = new CodeProject.Business.Entities.Status();

            try
            {

                _statusDataService.CreateSession();
                status = _statusDataService.GetStatus(id);
                _statusDataService.CloseSession();      
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
                _statusDataService.CloseSession();
            }

            return status;

        }

        /// Delete Status
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteStatus(Status status, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                StatusBusinessRules statusBusinessRules = new StatusBusinessRules();
                ValidationResult results = statusBusinessRules.Validate(status);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _statusDataService.CreateSession();
                _statusDataService.BeginTransaction();

                _statusDataService.DeleteStatus(status);
                _statusDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Status was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _statusDataService.CloseSession();
            }


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

                _statusDataService.CreateSession();
                _statusDataService.BeginTransaction();
                _statusDataService.CommitTransaction(true);
                _statusDataService.CloseSession();

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
                _statusDataService.CloseSession();
            }           

        }




    }
}
