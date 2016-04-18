using System.Collections.Generic;
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
    [RoutePrefix("api/StatusService")]
    public class StatusServiceController : ApiController
    {

        [Inject]
        public IStatusDataService _statusDataService { get; set; }

        /// <summary>
        /// Create Status
        /// </summary>
        /// <param name="request"></param>
        /// <param name="statusViewModel"></param>
        /// <returns></returns>
        [Route("CreateStatus")]
        [HttpPost]
        public HttpResponseMessage CreateStatus(HttpRequestMessage request, [FromBody] StatusViewModel statusViewModel)
        {
            TransactionalInformation transaction;

            Status status = new Status();
            status.Name = statusViewModel.Name;

            StatusBusinessService statusBusinessService = new StatusBusinessService(_statusDataService);
            statusBusinessService.CreateStatus(status, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusViewModel.ReturnStatus = false;
                statusViewModel.ReturnMessage = transaction.ReturnMessage;
                statusViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusViewModel>(HttpStatusCode.BadRequest, statusViewModel);
                return responseError;

            }

            statusViewModel.Id = status.Id;
            statusViewModel.ReturnStatus = true;
            statusViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusViewModel>(HttpStatusCode.OK, statusViewModel);
            return response;

        }

        [Route("UpdateStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateStatus(HttpRequestMessage request, [FromBody] StatusViewModel statusViewModel)
        {
            TransactionalInformation transaction;

            Status status = new Status();
            status.Id = statusViewModel.Id;
            status.Name = statusViewModel.Name;

            StatusBusinessService statusBusinessService = new StatusBusinessService(_statusDataService);
            statusBusinessService.UpdateStatus(status, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusViewModel.ReturnStatus = false;
                statusViewModel.ReturnMessage = transaction.ReturnMessage;
                statusViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusViewModel>(HttpStatusCode.BadRequest, statusViewModel);
                return responseError;

            }

            statusViewModel.ReturnStatus = true;
            statusViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusViewModel>(HttpStatusCode.OK, statusViewModel);
            return response;

        }

        /// <summary>
        /// Get Statuses
        /// </summary>
        /// <param name="request"></param>
        /// <param name="statusViewModel"></param>
        /// <returns></returns>
        [Route("GetStatuses")]
        [HttpPost]
        public HttpResponseMessage GetStatuses(HttpRequestMessage request, [FromBody] StatusViewModel statusViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = statusViewModel.CurrentPageNumber;
            int pageSize = statusViewModel.PageSize;
            string sortExpression = statusViewModel.SortExpression;
            string sortDirection = statusViewModel.SortDirection;

            StatusBusinessService statusBusinessService = new StatusBusinessService(_statusDataService);
            List<Status> statuses = statusBusinessService.GetStatuses(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusViewModel.ReturnStatus = false;
                statusViewModel.ReturnMessage = transaction.ReturnMessage;
                statusViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusViewModel>(HttpStatusCode.BadRequest, statusViewModel);
                return responseError;

            }

            statusViewModel.TotalPages = transaction.TotalPages;
            statusViewModel.TotalRows = transaction.TotalRows;
            statusViewModel.Statuses = statuses;
            statusViewModel.ReturnStatus = true;
            statusViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusViewModel>(HttpStatusCode.OK, statusViewModel);
            return response;

        }
       
        /// <summary>
        /// Get Status
        /// </summary>
        /// <param name="request"></param>
        /// <param name="statusViewModel"></param>
        /// <returns></returns>
        [Route("GetStatus")]
        [HttpPost]
        public HttpResponseMessage GetStatus(HttpRequestMessage request, [FromBody] StatusViewModel statusViewModel)
        {

            TransactionalInformation transaction;

            long statusID = statusViewModel.Id;

            StatusBusinessService statusBusinessService = new StatusBusinessService(_statusDataService);
            Status status = statusBusinessService.GetStatus(statusID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusViewModel.ReturnStatus = false;
                statusViewModel.ReturnMessage = transaction.ReturnMessage;
                statusViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusViewModel>(HttpStatusCode.BadRequest, statusViewModel);
                return responseError;

            }

            statusViewModel.Id = status.Id;
            statusViewModel.Name = status.Name;
                     
            statusViewModel.ReturnStatus = true;
            statusViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusViewModel>(HttpStatusCode.OK, statusViewModel);
            return response;

        }

        [Route("DeleteStatus")]
        [HttpPost]
        public HttpResponseMessage DeleteStatus(HttpRequestMessage request, [FromBody] StatusViewModel statusViewModel)
        {
            TransactionalInformation transaction;

            Status status = new Status();
            status.Id = statusViewModel.Id;
            status.Name = statusViewModel.Name;

            StatusBusinessService statusBusinessService = new StatusBusinessService(_statusDataService);
            statusBusinessService.DeleteStatus(status, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusViewModel.ReturnStatus = false;
                statusViewModel.ReturnMessage = transaction.ReturnMessage;
                statusViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusViewModel>(HttpStatusCode.BadRequest, statusViewModel);
                return responseError;

            }

            statusViewModel.ReturnStatus = true;
            statusViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusViewModel>(HttpStatusCode.OK, statusViewModel);
            return response;

        }

    }
}