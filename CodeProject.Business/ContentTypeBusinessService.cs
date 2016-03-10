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
    public class ContentTypeBusinessService
    {
        private IContentTypeDataService _contentTypeDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContentTypeBusinessService(IContentTypeDataService contentTypeDataService)
        {
            _contentTypeDataService = contentTypeDataService;
        }

        /// <summary>
        /// Create Content Type
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ContentType CreateContentType(ContentType contentType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ContentTypeBusinessRules contentTypeBusinessRules = new ContentTypeBusinessRules();
                ValidationResult results = contentTypeBusinessRules.Validate(contentType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return contentType;
                }

                _contentTypeDataService.CreateSession();
                _contentTypeDataService.BeginTransaction();
                _contentTypeDataService.CreateContentType(contentType);
                _contentTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Content successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _contentTypeDataService.CloseSession();
            }

            return contentType;


        }

        /// <summary>
        /// Update Action Type
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateContentType(ContentType contentType, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                ContentTypeBusinessRules contentTypeBusinessRules = new ContentTypeBusinessRules();
                ValidationResult results = contentTypeBusinessRules.Validate(contentType);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _contentTypeDataService.CreateSession();
                _contentTypeDataService.BeginTransaction();

                ContentType existingContentType = _contentTypeDataService.GetContentType(contentType.Id);

                existingContentType.Id = contentType.Id;
                existingContentType.Name = contentType.Name;
                existingContentType.Description = contentType.Description;

                _contentTypeDataService.UpdateContentType(contentType);
                _contentTypeDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Content Type was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _contentTypeDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Content Types
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<ContentType> GetContentTypes(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<ContentType> contentTypes = new List<ContentType>();

            try
            {
                int totalRows;

                _contentTypeDataService.CreateSession();
                contentTypes = _contentTypeDataService.GetContentTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _contentTypeDataService.CloseSession();

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
                _contentTypeDataService.CloseSession();
            }

            return contentTypes;

        }

        /// <summary>
        /// Get Content Type
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ContentType GetContentType(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            ContentType contentType = new ContentType();

            try
            {

                _contentTypeDataService.CreateSession();
                contentType = _contentTypeDataService.GetContentType(id);
                _contentTypeDataService.CloseSession();      
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
                _contentTypeDataService.CloseSession();
            }

            return contentType;

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            ContentType contentType = new ContentType();

            try
            {

                _contentTypeDataService.CreateSession();
                _contentTypeDataService.BeginTransaction();
                _contentTypeDataService.CommitTransaction(true);
                _contentTypeDataService.CloseSession();

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
                _contentTypeDataService.CloseSession();
            }           

        }




    }
}
