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
    public class ContentFormMapBusinessService
    {
        private IContentFormMapDataService _contentFormMapDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContentFormMapBusinessService(IContentFormMapDataService contentFormMapDataService)
        {
            _contentFormMapDataService = contentFormMapDataService;
        }

        /// <summary>
        /// Create Content Form Map
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ContentFormMap CreateContentFormMap(ContentFormMap contentFormMap, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ContentFormMapBusinessRules contentFormMapBusinessRules = new ContentFormMapBusinessRules();
                ValidationResult results = contentFormMapBusinessRules.Validate(contentFormMap);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return contentFormMap;
                }

                _contentFormMapDataService.CreateSession();
                _contentFormMapDataService.BeginTransaction();
                _contentFormMapDataService.CreateContentFormMap(contentFormMap);
                _contentFormMapDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Content Form Map successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _contentFormMapDataService.CloseSession();
            }

            return contentFormMap;


        }

        /// <summary>
        /// Update Content Form Map
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateContentFormMap(ContentFormMap contentFormMap, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                ContentFormMapBusinessRules contentFormMapBusinessRules = new ContentFormMapBusinessRules();
                ValidationResult results = contentFormMapBusinessRules.Validate(contentFormMap);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _contentFormMapDataService.CreateSession();
                _contentFormMapDataService.BeginTransaction();

                ContentFormMap existingContentFormMap = _contentFormMapDataService.GetContentFormMap(contentFormMap.Id);

                existingContentFormMap.Id = contentFormMap.Id;
                existingContentFormMap.FormId = contentFormMap.FormId;
                existingContentFormMap.ContentTypeId = contentFormMap.ContentTypeId;
                existingContentFormMap.ContentId = contentFormMap.ContentId;

                _contentFormMapDataService.UpdateContentFormMap(contentFormMap);
                _contentFormMapDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Content Form Map was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _contentFormMapDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Content Form Maps
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<ContentFormMap> GetContentFormMaps(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<ContentFormMap> contentFormMaps = new List<ContentFormMap>();

            try
            {
                int totalRows;

                _contentFormMapDataService.CreateSession();
                contentFormMaps = _contentFormMapDataService.GetContentFormMaps(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _contentFormMapDataService.CloseSession();

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
                _contentFormMapDataService.CloseSession();
            }

            return contentFormMaps;

        }

        /// <summary>
        /// Get Content Form Map
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ContentFormMap GetContentFormMap(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            ContentFormMap contentFormMap = new ContentFormMap();

            try
            {

                _contentFormMapDataService.CreateSession();
                contentFormMap = _contentFormMapDataService.GetContentFormMap(id);
                _contentFormMapDataService.CloseSession();      
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
                _contentFormMapDataService.CloseSession();
            }

            return contentFormMap;

        }

        /// <summary>
        /// Delete Content Form Map
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteContentFormMap(ContentFormMap contentFormMap, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                ContentFormMapBusinessRules contentFormMapBusinessRules = new ContentFormMapBusinessRules();
                ValidationResult results = contentFormMapBusinessRules.Validate(contentFormMap);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _contentFormMapDataService.CreateSession();
                _contentFormMapDataService.BeginTransaction();

                _contentFormMapDataService.DeleteContentFormMap(contentFormMap);
                _contentFormMapDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Content Form Map was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _contentFormMapDataService.CloseSession();
            }


        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            ContentFormMap contentFormMap = new ContentFormMap();

            try
            {

                _contentFormMapDataService.CreateSession();
                _contentFormMapDataService.BeginTransaction();
                _contentFormMapDataService.CommitTransaction(true);
                _contentFormMapDataService.CloseSession();

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
                _contentFormMapDataService.CloseSession();
            }           

        }




    }
}
