using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeProject.Portal.Models;
using CodeProject.Business.Entities;
using CodeProject.Business;
using CodeProject.Interfaces;
using Ninject;

namespace CodeProject.Portal.WebApiControllers
{
    [RoutePrefix("api/DocumentService")]
    public class DocumentServiceController : ApiController
    {
        public DocumentServiceController()
   {
   }
        [Inject]
        public IDocumentDataService _documentDataService { get; set; }        

        /// <summary>
        /// Get Documents
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetDocuments")]
        [HttpPost]
        public HttpResponseMessage GetDocuments(HttpRequestMessage request, [FromBody] DocumentViewModel documentViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = documentViewModel.CurrentPageNumber;
            int pageSize = documentViewModel.PageSize;
            string sortExpression = documentViewModel.SortExpression;
            string sortDirection = documentViewModel.SortDirection;

            DocumentBusinessService documentBusinessService = new DocumentBusinessService(_documentDataService);
            List<Document> documents = documentBusinessService.GetDocuments(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                documentViewModel.ReturnStatus = false;
                documentViewModel.ReturnMessage = transaction.ReturnMessage;
                documentViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.BadRequest, documentViewModel);
                return responseError;

            }

            documentViewModel.TotalPages = transaction.TotalPages;
            documentViewModel.TotalRows = transaction.TotalRows;
            documentViewModel.Documents = documents;
            documentViewModel.ReturnStatus = true;
            documentViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.OK, documentViewModel);
            return response;

        }

        /// <summary>
        /// Create Document
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateDocument")]
        [HttpPost]
        public HttpResponseMessage CreateDocument(HttpRequestMessage request, [FromBody] DocumentViewModel documentViewModel)
        {
            TransactionalInformation transaction;

            Document document = new Document();
            document.Name = documentViewModel.Name;
            document.Extension = documentViewModel.Extension;
            document.ContentId = documentViewModel.ContentId;
            document.Created = documentViewModel.Created;
            document.ParentDocumentId = documentViewModel.ParentDocumentId;
            document.NameOnServer = documentViewModel.NameOnServer;
            document.ContentTypeId = documentViewModel.ContentTypeId;

            DocumentBusinessService documentBusinessService = new DocumentBusinessService(_documentDataService);
            documentBusinessService.CreateDocument(document, out transaction);
            if (transaction.ReturnStatus == false)
            {
                documentViewModel.ReturnStatus = false;
                documentViewModel.ReturnMessage = transaction.ReturnMessage;
                documentViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.BadRequest, documentViewModel);
                return responseError;

            }

            documentViewModel.Id = document.Id;
            documentViewModel.ReturnStatus = true;
            documentViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.OK, documentViewModel);
            return response;

        }

        [Route("DeleteDocument")]
        [HttpPost]
        public HttpResponseMessage DeleteDocument(HttpRequestMessage request, [FromBody] DocumentViewModel documentViewModel)
        {
            TransactionalInformation transaction;

            Document document = new Document();
            document.Id = documentViewModel.Id;

            DocumentBusinessService documentBusinessService = new DocumentBusinessService(_documentDataService);
            documentBusinessService.DeleteDocument(document, out transaction);
            if (transaction.ReturnStatus == false)
            {
                documentViewModel.ReturnStatus = false;
                documentViewModel.ReturnMessage = transaction.ReturnMessage;
                documentViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.BadRequest, documentViewModel);
                return responseError;

            }

            documentViewModel.ReturnStatus = true;
            documentViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.OK, documentViewModel);
            return response;

        }

        /// <summary>
        /// Get Documents for Folder
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetDocumentsForContent")]
        [HttpPost]
        public HttpResponseMessage GetDocumentsForContent(HttpRequestMessage request, [FromBody] DocumentViewModel documentViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = documentViewModel.CurrentPageNumber;
            int pageSize = documentViewModel.PageSize;
            string sortExpression = documentViewModel.SortExpression;
            string sortDirection = documentViewModel.SortDirection;
            long contentId = documentViewModel.ContentId;
            long contentTypeId = documentViewModel.ContentTypeId;

            DocumentBusinessService documentBusinessService = new DocumentBusinessService(_documentDataService);
            List<Document> documents = documentBusinessService.GetDocumentsForContent(contentTypeId, contentId, currentPageNumber,pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                documentViewModel.ReturnStatus = false;
                documentViewModel.ReturnMessage = transaction.ReturnMessage;
                documentViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.BadRequest, documentViewModel);
                return responseError;

            }

            documentViewModel.TotalPages = transaction.TotalPages;
            documentViewModel.TotalRows = transaction.TotalRows;
            documentViewModel.Documents = documents;
            documentViewModel.ReturnStatus = true;
            documentViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.OK, documentViewModel);
            return response;

        }
    }
}