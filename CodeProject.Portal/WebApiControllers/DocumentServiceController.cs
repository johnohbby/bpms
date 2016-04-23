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
using System.IO;

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
        [Route("UpdateDocumentsContentId")]
        [HttpPost]
        public HttpResponseMessage UpdateDocumentsContentId(HttpRequestMessage request, [FromBody] SaveDocumentViewModel documentViewModel)
        {
            TransactionalInformation transaction;

            List<Document> list = new List<Document>();

            foreach (DocumentViewModel doc in documentViewModel.Documents)
            {
                Document document = new Document();
                document.Id = doc.Id;
                document.ParentDocumentId = documentViewModel.ParentDocumentId;
                list.Add(document);
            }
            
           

            DocumentBusinessService documentBusinessService = new DocumentBusinessService(_documentDataService);
            documentBusinessService.UpdateDocumentsContentId(list, documentViewModel.ContentId, out transaction);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;

        }
        [Route("DownoadDocument")]
        [HttpGet]
        public HttpResponseMessage DownoadDocument()
        {
            HttpResponseMessage result = null;
            IEnumerable<string> headerValues = Request.Headers.GetValues("id");
            int id = Int32.Parse(headerValues.FirstOrDefault());

            TransactionalInformation transaction;
            DocumentBusinessService documentBusinessService = new DocumentBusinessService(_documentDataService);
            Document doc =  documentBusinessService.GetDocument(id, out transaction);


            var localFilePath = doc.NameOnServer;// HttpContext.Current.Server.MapPath("~/timetable.jpg");

            if (!File.Exists(localFilePath))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "SampleImg";
            }

            return result;
        }

        [Route("GetDocumentsByActionId")]
        [HttpPost]
        public HttpResponseMessage GetDocumentsByActionId(HttpRequestMessage request, [FromBody] ActionViewModel documentViewModel)
        {
            HttpResponseMessage result = null;
            

            TransactionalInformation transaction;
            DocumentBusinessService documentBusinessService = new DocumentBusinessService(_documentDataService);
            List<Document> docs = documentBusinessService.GetDocumentByActionId(documentViewModel.Id, out transaction);

            DocumentViewModel dvm = new DocumentViewModel();

            dvm.Documents = docs;
            var response = Request.CreateResponse<DocumentViewModel>(HttpStatusCode.OK, dvm);
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