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
    [RoutePrefix("api/StatusTranslationService")]
    public class StatusTranslationServiceController : ApiController
    {

        [Inject]
        public IStatusTranslationDataService _statusTranslationDataService { get; set; }

        /// <summary>
        /// Create Status Translation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="StatusTranslationViewModel"></param>
        /// <returns></returns>
        [Route("CreateStatusTranslation")]
        [HttpPost]
        public HttpResponseMessage CreateStatusTranslation(HttpRequestMessage request, [FromBody] StatusTranslationViewModel statusTranslationViewModel)
        {
            TransactionalInformation transaction;

            StatusTranslation statusTranslation = new StatusTranslation();
            statusTranslation.StatusIdFrom = statusTranslationViewModel.StatusIdFrom;
            statusTranslation.StatusIdTo = statusTranslationViewModel.StatusIdTo;
            statusTranslation.ActionTypeId = statusTranslationViewModel.ActionTypeId;

            StatusTranslationBusinessService statusTranslationBusinessService = new StatusTranslationBusinessService(_statusTranslationDataService);
            statusTranslationBusinessService.CreateStatusTranslation(statusTranslation, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusTranslationViewModel.ReturnStatus = false;
                statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;
                statusTranslationViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.BadRequest, statusTranslationViewModel);
                return responseError;

            }

            statusTranslationViewModel.Id = statusTranslation.Id;
            statusTranslationViewModel.ReturnStatus = true;
            statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.OK, statusTranslationViewModel);
            return response;

        }

        [Route("UpdateStatusTranslation")]
        [HttpPost]
        public HttpResponseMessage UpdateStatusTranslation(HttpRequestMessage request, [FromBody] StatusTranslationViewModel statusTranslationViewModel)
        {
            TransactionalInformation transaction;

            StatusTranslation statusTranslation = new StatusTranslation();
            statusTranslation.Id = statusTranslationViewModel.Id;
            statusTranslation.StatusIdFrom = statusTranslationViewModel.StatusIdFrom;
            statusTranslation.StatusIdTo = statusTranslationViewModel.StatusIdTo;
            statusTranslation.ActionTypeId = statusTranslationViewModel.ActionTypeId;

            StatusTranslationBusinessService statusTranslationBusinessService = new StatusTranslationBusinessService(_statusTranslationDataService);
            statusTranslationBusinessService.UpdateStatusTranslation(statusTranslation, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusTranslationViewModel.ReturnStatus = false;
                statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;
                statusTranslationViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.BadRequest, statusTranslationViewModel);
                return responseError;

            }

            statusTranslationViewModel.ReturnStatus = true;
            statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.OK, statusTranslationViewModel);
            return response;

        }

        /// <summary>
        /// Get Status Translations
        /// </summary>
        /// <param name="request"></param>
        /// <param name="StatusTranslationViewModel"></param>
        /// <returns></returns>
        [Route("GetStatusTranslations")]
        [HttpPost]
        public HttpResponseMessage GetStatusTranslations(HttpRequestMessage request, [FromBody] StatusTranslationViewModel statusTranslationViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = statusTranslationViewModel.CurrentPageNumber;
            int pageSize = statusTranslationViewModel.PageSize;
            string sortExpression = statusTranslationViewModel.SortExpression;
            string sortDirection = statusTranslationViewModel.SortDirection;

            StatusTranslationBusinessService statusTranslationBusinessService = new StatusTranslationBusinessService(_statusTranslationDataService);
            List<StatusTranslation> statusTranslations =statusTranslationBusinessService.GetStatusTranslations(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusTranslationViewModel.ReturnStatus = false;
                statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;
                statusTranslationViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.BadRequest, statusTranslationViewModel);
                return responseError;

            }

            statusTranslationViewModel.TotalPages = transaction.TotalPages;
            statusTranslationViewModel.TotalRows = transaction.TotalRows;
            statusTranslationViewModel.StatusTranslations = statusTranslations;
            statusTranslationViewModel.ReturnStatus = true;
            statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.OK, statusTranslationViewModel);
            return response;

        }
       
        /// <summary>
        /// Get Status Translation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="StatusTranslationViewModel"></param>
        /// <returns></returns>
        [Route("GetStatusTranslation")]
        [HttpPost]
        public HttpResponseMessage GetStatusTranslation(HttpRequestMessage request, [FromBody] StatusTranslationViewModel statusTranslationViewModel)
        {

            TransactionalInformation transaction;

            long statusTranslationID = statusTranslationViewModel.Id;

            StatusTranslationBusinessService statusTranslationBusinessService = new StatusTranslationBusinessService(_statusTranslationDataService);
            StatusTranslation statusTranslation = statusTranslationBusinessService.GetStatusTranslation(statusTranslationID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusTranslationViewModel.ReturnStatus = false;
                statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;
                statusTranslationViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.BadRequest, statusTranslationViewModel);
                return responseError;

            }

            statusTranslationViewModel.Id = statusTranslation.Id;
            statusTranslation.StatusIdFrom = statusTranslationViewModel.StatusIdFrom;
            statusTranslation.StatusIdTo = statusTranslationViewModel.StatusIdTo;
            statusTranslation.ActionTypeId = statusTranslationViewModel.ActionTypeId;

                     
            statusTranslationViewModel.ReturnStatus = true;
            statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.OK, statusTranslationViewModel);
            return response;

        }

        [Route("DeleteStatusTranslation")]
        [HttpPost]
        public HttpResponseMessage DeleteStatusTranslation(HttpRequestMessage request, [FromBody] StatusTranslationViewModel statusTranslationViewModel)
        {
            TransactionalInformation transaction;

            StatusTranslation statusTranslation = new StatusTranslation();
            statusTranslation.Id = statusTranslationViewModel.Id;
            statusTranslation.StatusIdFrom = statusTranslationViewModel.StatusIdFrom;
            statusTranslation.StatusIdTo = statusTranslationViewModel.StatusIdTo;
            statusTranslation.ActionTypeId = statusTranslationViewModel.ActionTypeId;

            StatusTranslationBusinessService statusTranslationBusinessService = new StatusTranslationBusinessService(_statusTranslationDataService);
            statusTranslationBusinessService.DeleteStatusTranslation(statusTranslation, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusTranslationViewModel.ReturnStatus = false;
                statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;
                statusTranslationViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.BadRequest, statusTranslationViewModel);
                return responseError;

            }

            statusTranslationViewModel.ReturnStatus = true;
            statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.OK, statusTranslationViewModel);
            return response;

        }

        /// <summary>
        /// GetStatusTranslationsForActionType
        /// </summary>
        /// <param name="request"></param>
        /// <param name="StatusTranslationViewModel"></param>
        /// <returns></returns>
        [Route("GetStatusTranslationsForActionType")]
        [HttpPost]
        public HttpResponseMessage GetStatusTranslationsForActionType(HttpRequestMessage request, [FromBody] StatusTranslationViewModel statusTranslationViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = statusTranslationViewModel.CurrentPageNumber;
            int pageSize = statusTranslationViewModel.PageSize;
            string sortExpression = statusTranslationViewModel.SortExpression;
            string sortDirection = statusTranslationViewModel.SortDirection;
            long actionTypeId = statusTranslationViewModel.ActionTypeId;

            StatusTranslationBusinessService statusTranslationBusinessService = new StatusTranslationBusinessService(_statusTranslationDataService);
            List<StatusTranslation> statusTranslations = statusTranslationBusinessService.GetStatusTranslationsForActionType(actionTypeId, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                statusTranslationViewModel.ReturnStatus = false;
                statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;
                statusTranslationViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.BadRequest, statusTranslationViewModel);
                return responseError;

            }

            statusTranslationViewModel.TotalPages = transaction.TotalPages;
            statusTranslationViewModel.TotalRows = transaction.TotalRows;
            statusTranslationViewModel.StatusTranslations = statusTranslations;
            statusTranslationViewModel.ReturnStatus = true;
            statusTranslationViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<StatusTranslationViewModel>(HttpStatusCode.OK, statusTranslationViewModel);
            return response;

        }

    }
}