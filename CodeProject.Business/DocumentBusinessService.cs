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
    public class DocumentBusinessService
    {
        private IDocumentDataService _documentDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentBusinessService(IDocumentDataService documentDataService)
        {
            _documentDataService = documentDataService;
        }

        /// <summary>
        /// Create Document
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long CreateDocument(Document document, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            long insertedId = -1;
            try
            {
                DocumentBusinessRules documentBusinessRules = new DocumentBusinessRules();
                ValidationResult results = documentBusinessRules.Validate(document);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return insertedId;
                }

                _documentDataService.CreateSession();
                _documentDataService.BeginTransaction();
                insertedId = _documentDataService.CreateDocument(document);
                _documentDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Document successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _documentDataService.CloseSession();
            }

            return insertedId;


        }


        /// <summary>
        /// Get Documents
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Document> GetDocuments(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<Document> documents = new List<Document>();

            try
            {
                int totalRows;

                _documentDataService.CreateSession();
                documents = _documentDataService.GetDocuments(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _documentDataService.CloseSession();

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
                _documentDataService.CloseSession();
            }

            return documents;

        }

        /// <summary>
        /// Get Document
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Document GetDocument(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Document document = new Document();

            try
            {

                _documentDataService.CreateSession();
                document = _documentDataService.GetDocument(id);
                _documentDataService.CloseSession();      
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
                _documentDataService.CloseSession();
            }

            return document;

        }

        /// <summary>
        /// Delete Document
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteDocument(Document document, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                DocumentBusinessRules documentBusinessRules = new DocumentBusinessRules();
                ValidationResult results = documentBusinessRules.Validate(document);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _documentDataService.CreateSession();
                _documentDataService.BeginTransaction();

                _documentDataService.DeleteDocument(document);
                _documentDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Document was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _documentDataService.CloseSession();
            }


        }

        
             /// <summary>
        /// Get Documents for Folder
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Document> GetDocumentsForContent(long contentTypeId, long contentId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<Document> documents = new List<Document>();

            try
            {
                int totalRows;

                _documentDataService.CreateSession();
                documents = _documentDataService.GetDocumentsForContent(contentTypeId, contentId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _documentDataService.CloseSession();

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
                _documentDataService.CloseSession();
            }

            return documents;

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Document document = new Document();

            try
            {

                _documentDataService.CreateSession();
                _documentDataService.BeginTransaction();
                _documentDataService.CommitTransaction(true);
                _documentDataService.CloseSession();

                _documentDataService.CreateSession();
                _documentDataService.BeginTransaction();
                _documentDataService.CommitTransaction(true);
                _documentDataService.CloseSession();

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
                _documentDataService.CloseSession();
            }           

        }




    }
}
