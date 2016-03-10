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
    public class ContentRightBusinessService
    {
        private IContentRightDataService _contentRightDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContentRightBusinessService(IContentRightDataService contentRightDataService)
        {
            _contentRightDataService = contentRightDataService;
        }

        /// <summary>
        /// Create Content Right
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ContentRight CreateContentRight(ContentRight contentRight, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ContentRightBusinessRules contentRightBusinessRules = new ContentRightBusinessRules();
                ValidationResult results = contentRightBusinessRules.Validate(contentRight);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return contentRight;
                }

                _contentRightDataService.CreateSession();
                _contentRightDataService.BeginTransaction();
                _contentRightDataService.CreateContentRight(contentRight);
                _contentRightDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Content Right successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _contentRightDataService.CloseSession();
            }

            return contentRight;


        }

        /// <summary>
        /// Update Content Right
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateContentRight(ContentRight contentRight, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                ContentRightBusinessRules contentRightBusinessRules = new ContentRightBusinessRules();
                ValidationResult results = contentRightBusinessRules.Validate(contentRight);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _contentRightDataService.CreateSession();
                _contentRightDataService.BeginTransaction();

                ContentRight existingContentRight = _contentRightDataService.GetContentRight(contentRight.Id);

                existingContentRight.Id = contentRight.Id;
                existingContentRight.GroupId = contentRight.GroupId;
                existingContentRight.ContentTypeId = contentRight.ContentTypeId;
                existingContentRight.ContentId = contentRight.ContentId;
                existingContentRight.RightId = contentRight.RightId;

                _contentRightDataService.UpdateContentRight(contentRight);
                _contentRightDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Content Right was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _contentRightDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Content Rights
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<ContentRight> GetContentRights(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<ContentRight> contentRights = new List<ContentRight>();

            try
            {
                int totalRows;

                _contentRightDataService.CreateSession();
                contentRights = _contentRightDataService.GetContentRights(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _contentRightDataService.CloseSession();

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
                _contentRightDataService.CloseSession();
            }

            return contentRights;

        }

        /// <summary>
        /// Get Content Right
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ContentRight GetContentRight(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            ContentRight contentRight = new ContentRight();

            try
            {

                _contentRightDataService.CreateSession();
                contentRight = _contentRightDataService.GetContentRight(id);
                _contentRightDataService.CloseSession();      
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
                _contentRightDataService.CloseSession();
            }

            return contentRight;

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

                _contentRightDataService.CreateSession();
                _contentRightDataService.BeginTransaction();
                _contentRightDataService.CommitTransaction(true);
                _contentRightDataService.CloseSession();

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
                _contentRightDataService.CloseSession();
            }           

        }




    }
}
