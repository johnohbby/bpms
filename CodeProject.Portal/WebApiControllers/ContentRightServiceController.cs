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
    [RoutePrefix("api/ContentRightService")]
    public class ContentRightServiceController : ApiController
    {
        public ContentRightServiceController()
   {
   }
        [Inject]
        public IContentRightDataService _contentRightDataService { get; set; }

        [Inject]
        public IContentTypeDataService _contentTypeDataService { get; set; }

        

        /// <summary>
        /// Get Content Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetContentTypes")]
        [HttpPost]
        public HttpResponseMessage GetContentTypes(HttpRequestMessage request, [FromBody] ContentTypeViewModel contentTypeViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = contentTypeViewModel.CurrentPageNumber;
            int pageSize = contentTypeViewModel.PageSize;
            string sortExpression = contentTypeViewModel.SortExpression;
            string sortDirection = contentTypeViewModel.SortDirection;

            ContentTypeBusinessService contentTypeBusinessService = new ContentTypeBusinessService(_contentTypeDataService);
            List<ContentType> contentTypes = contentTypeBusinessService.GetContentTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                contentTypeViewModel.ReturnStatus = false;
                contentTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                contentTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentTypeViewModel>(HttpStatusCode.BadRequest, contentTypeViewModel);
                return responseError;

            }

            contentTypeViewModel.TotalPages = transaction.TotalPages;
            contentTypeViewModel.TotalRows = transaction.TotalRows;
            contentTypeViewModel.ContentTypes = contentTypes;
            contentTypeViewModel.ReturnStatus = true;
            contentTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentTypeViewModel>(HttpStatusCode.OK, contentTypeViewModel);
            return response;

        }

        /// <summary>
        /// Get Content Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetContentRights")]
        [HttpPost]
        public HttpResponseMessage GetContentRights(HttpRequestMessage request, [FromBody] ContentRightViewModel contentRightViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = contentRightViewModel.CurrentPageNumber;
            int pageSize = contentRightViewModel.PageSize;
            string sortExpression = contentRightViewModel.SortExpression;
            string sortDirection = contentRightViewModel.SortDirection;

            ContentRightBusinessService contentRightBusinessService = new ContentRightBusinessService(_contentRightDataService);
            List<ContentRight> contentRights = contentRightBusinessService.GetContentRights(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                contentRightViewModel.ReturnStatus = false;
                contentRightViewModel.ReturnMessage = transaction.ReturnMessage;
                contentRightViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentRightViewModel>(HttpStatusCode.BadRequest, contentRightViewModel);
                return responseError;

            }

            contentRightViewModel.TotalPages = transaction.TotalPages;
            contentRightViewModel.TotalRows = transaction.TotalRows;
            contentRightViewModel.ContentRights = contentRights;
            contentRightViewModel.ReturnStatus = true;
            contentRightViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentRightViewModel>(HttpStatusCode.OK, contentRightViewModel);
            return response;

        }

        /// <summary>
        /// Create Content Right
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateContentRight")]
        [HttpPost]
        public HttpResponseMessage CreateContentRight(HttpRequestMessage request, [FromBody] ContentRightViewModel contentRightViewModel)
        {
            TransactionalInformation transaction;

            ContentRight contentRight = new ContentRight();
            contentRight.GroupId = contentRightViewModel.GroupId;
            contentRight.ContentTypeId = contentRightViewModel.ContentTypeId;
            contentRight.ContentId = contentRightViewModel.ContentId;
            contentRight.RightTypeId = contentRightViewModel.RightTypeId;

            ContentRightBusinessService contentRightBusinessService = new ContentRightBusinessService(_contentRightDataService);
            contentRightBusinessService.CreateContentRight(contentRight, out transaction);
            if (transaction.ReturnStatus == false)
            {
                contentRightViewModel.ReturnStatus = false;
                contentRightViewModel.ReturnMessage = transaction.ReturnMessage;
                contentRightViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentRightViewModel>(HttpStatusCode.BadRequest, contentRightViewModel);
                return responseError;

            }

            contentRightViewModel.Id = contentRight.Id;
            contentRightViewModel.ReturnStatus = true;
            contentRightViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentRightViewModel>(HttpStatusCode.OK, contentRightViewModel);
            return response;

        }

        [Route("UpdateContentRight")]
        [HttpPost]
        public HttpResponseMessage UpdateContentRight(HttpRequestMessage request, [FromBody] ContentRightViewModel contentRightViewModel)
        {
            TransactionalInformation transaction;

            ContentRight contentRight = new ContentRight();
            contentRight.Id = contentRightViewModel.Id;
            contentRight.GroupId = contentRightViewModel.GroupId;
            contentRight.ContentTypeId = contentRightViewModel.ContentTypeId;
            contentRight.ContentId = contentRightViewModel.ContentId;
            contentRight.RightTypeId = contentRightViewModel.RightTypeId;

            ContentRightBusinessService contentRightBusinessService = new ContentRightBusinessService(_contentRightDataService);
            contentRightBusinessService.UpdateContentRight(contentRight, out transaction);
            if (transaction.ReturnStatus == false)
            {
                contentRightViewModel.ReturnStatus = false;
                contentRightViewModel.ReturnMessage = transaction.ReturnMessage;
                contentRightViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentRightViewModel>(HttpStatusCode.BadRequest, contentRightViewModel);
                return responseError;

            }

            contentRightViewModel.ReturnStatus = true;
            contentRightViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentRightViewModel>(HttpStatusCode.OK, contentRightViewModel);
            return response;

        }

        [Route("DeleteContentRight")]
        [HttpPost]
        public HttpResponseMessage DeleteContentRight(HttpRequestMessage request, [FromBody] ContentRightViewModel contentRightViewModel)
        {
            TransactionalInformation transaction;

            ContentRight contentRight = new ContentRight();
            contentRight.Id = contentRightViewModel.Id;
            contentRight.GroupId = contentRightViewModel.GroupId;
            contentRight.ContentTypeId = contentRightViewModel.ContentTypeId;
            contentRight.ContentId = contentRightViewModel.ContentId;
            contentRight.RightTypeId = contentRightViewModel.RightTypeId;

            ContentRightBusinessService contentRightBusinessService = new ContentRightBusinessService(_contentRightDataService);
            contentRightBusinessService.DeleteContentRight(contentRight, out transaction);
           if (transaction.ReturnStatus == false)
            {
                contentRightViewModel.ReturnStatus = false;
                contentRightViewModel.ReturnMessage = transaction.ReturnMessage;
                contentRightViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ContentRightViewModel>(HttpStatusCode.BadRequest, contentRightViewModel);
                return responseError;

            }

            contentRightViewModel.ReturnStatus = true;
            contentRightViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ContentRightViewModel>(HttpStatusCode.OK, contentRightViewModel);
            return response;

        }
    }
}