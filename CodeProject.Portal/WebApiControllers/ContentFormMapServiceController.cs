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
    [RoutePrefix("api/ContentFormMapService")]
    public class ContentFormMapServiceController : ApiController
    {
        public ContentFormMapServiceController()
   {
   }
        [Inject]
        public IContentFormMapDataService _contentFormMapDataService { get; set; }
        

        /// <summary>
        /// Get Content Form Maps
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetContentFormMaps")]
        [HttpPost]
        public HttpResponseMessage GetContentFormMaps(HttpRequestMessage request, [FromBody] ContentFormMapViewModel contentFormMapViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = contentFormMapViewModel.CurrentPageNumber;
            int pageSize = contentFormMapViewModel.PageSize;
            string sortExpression = contentFormMapViewModel.SortExpression;
            string sortDirection = contentFormMapViewModel.SortDirection;

            ContentFormMapBusinessService contentFormMapBusinessService = new ContentFormMapBusinessService(_contentFormMapDataService);
            List<ContentFormMap> contentFormMaps = contentFormMapBusinessService.GetContentFormMaps(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                contentFormMapViewModel.ReturnStatus = false;
                contentFormMapViewModel.ReturnMessage = transaction.ReturnMessage;
                contentFormMapViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentFormMapViewModel>(HttpStatusCode.BadRequest, contentFormMapViewModel);
                return responseError;

            }

            contentFormMapViewModel.TotalPages = transaction.TotalPages;
            contentFormMapViewModel.TotalRows = transaction.TotalRows;
            contentFormMapViewModel.ContentFormMaps = contentFormMaps;
            contentFormMapViewModel.ReturnStatus = true;
            contentFormMapViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentFormMapViewModel>(HttpStatusCode.OK, contentFormMapViewModel);
            return response;

        }

        /// <summary>
        /// Create Content Form Map
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateContentFormMap")]
        [HttpPost]
        public HttpResponseMessage CreateContentFormMap(HttpRequestMessage request, [FromBody] ContentFormMapViewModel contentFormMapViewModel)
        {
            TransactionalInformation transaction;

            ContentFormMap contentFormMap = new ContentFormMap();
            contentFormMap.FormId = contentFormMapViewModel.FormId;
            contentFormMap.ContentTypeId = contentFormMapViewModel.ContentTypeId;
            contentFormMap.ContentId = contentFormMapViewModel.ContentId;

            ContentFormMapBusinessService contentFormMapBusinessService = new ContentFormMapBusinessService(_contentFormMapDataService);
            contentFormMapBusinessService.CreateContentFormMap(contentFormMap, out transaction);
            if (transaction.ReturnStatus == false)
            {
                contentFormMapViewModel.ReturnStatus = false;
                contentFormMapViewModel.ReturnMessage = transaction.ReturnMessage;
                contentFormMapViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentFormMapViewModel>(HttpStatusCode.BadRequest, contentFormMapViewModel);
                return responseError;

            }

            contentFormMapViewModel.Id = contentFormMap.Id;
            contentFormMapViewModel.ReturnStatus = true;
            contentFormMapViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentFormMapViewModel>(HttpStatusCode.OK, contentFormMapViewModel);
            return response;

        }

        [Route("UpdateContentFormMap")]
        [HttpPost]
        public HttpResponseMessage UpdateContentFormMap(HttpRequestMessage request, [FromBody] ContentFormMapViewModel contentFormMapViewModel)
        {
            TransactionalInformation transaction;

            ContentFormMap contentFormMap = new ContentFormMap();
            contentFormMap.Id = contentFormMapViewModel.Id;
            contentFormMap.FormId = contentFormMapViewModel.FormId;
            contentFormMap.ContentTypeId = contentFormMapViewModel.ContentTypeId;
            contentFormMap.ContentId = contentFormMapViewModel.ContentId;

            ContentFormMapBusinessService contentFormMapBusinessService = new ContentFormMapBusinessService(_contentFormMapDataService);
            contentFormMapBusinessService.UpdateContentFormMap(contentFormMap, out transaction);
            if (transaction.ReturnStatus == false)
            {
                contentFormMapViewModel.ReturnStatus = false;
                contentFormMapViewModel.ReturnMessage = transaction.ReturnMessage;
                contentFormMapViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentFormMapViewModel>(HttpStatusCode.BadRequest, contentFormMapViewModel);
                return responseError;

            }

            contentFormMapViewModel.ReturnStatus = true;
            contentFormMapViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentFormMapViewModel>(HttpStatusCode.OK, contentFormMapViewModel);
            return response;

        }

        [Route("DeleteContentFormMap")]
        [HttpPost]
        public HttpResponseMessage DeleteContentFormMap(HttpRequestMessage request, [FromBody] ContentFormMapViewModel contentFormMapViewModel)
        {
            TransactionalInformation transaction;

            ContentFormMap contentFormMap = new ContentFormMap();
            contentFormMap.Id = contentFormMapViewModel.Id;
            contentFormMap.FormId = contentFormMapViewModel.FormId;
            contentFormMap.ContentTypeId = contentFormMapViewModel.ContentTypeId;
            contentFormMap.ContentId = contentFormMapViewModel.ContentId;

            ContentFormMapBusinessService contentFormMapBusinessService = new ContentFormMapBusinessService(_contentFormMapDataService);
            contentFormMapBusinessService.DeleteContentFormMap(contentFormMap, out transaction);
           if (transaction.ReturnStatus == false)
            {
                contentFormMapViewModel.ReturnStatus = false;
                contentFormMapViewModel.ReturnMessage = transaction.ReturnMessage;
                contentFormMapViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentFormMapViewModel>(HttpStatusCode.BadRequest, contentFormMapViewModel);
                return responseError;

            }

            contentFormMapViewModel.ReturnStatus = true;
            contentFormMapViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentFormMapViewModel>(HttpStatusCode.OK, contentFormMapViewModel);
            return response;

        }
    }
}