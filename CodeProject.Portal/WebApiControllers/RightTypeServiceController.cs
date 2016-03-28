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
    [RoutePrefix("api/RightTypeService")]
    public class RightTypeServiceController : ApiController
    {
        public RightTypeServiceController()
   {
   }
        [Inject]
        public IRightTypeDataService _rightTypeDataService { get; set; }

        

        /// <summary>
        /// Get Right Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetRightTypes")]
        [HttpPost]
        public HttpResponseMessage GetRightTypes(HttpRequestMessage request, [FromBody] RightTypeViewModel rightTypeViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = rightTypeViewModel.CurrentPageNumber;
            int pageSize = rightTypeViewModel.PageSize;
            string sortExpression = rightTypeViewModel.SortExpression;
            string sortDirection = rightTypeViewModel.SortDirection;

            RightTypeBusinessService rightTypeBusinessService = new RightTypeBusinessService(_rightTypeDataService);
            List<RightType> rightTypes = rightTypeBusinessService.GetRightTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                rightTypeViewModel.ReturnStatus = false;
                rightTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                rightTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<RightTypeViewModel>(HttpStatusCode.BadRequest, rightTypeViewModel);
                return responseError;

            }

            rightTypeViewModel.TotalPages = transaction.TotalPages;
            rightTypeViewModel.TotalRows = transaction.TotalRows;
            rightTypeViewModel.RightTypes = rightTypes;
            rightTypeViewModel.ReturnStatus = true;
            rightTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<RightTypeViewModel>(HttpStatusCode.OK, rightTypeViewModel);
            return response;

        }

        /// <summary>
        /// Create Right Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateRightType")]
        [HttpPost]
        public HttpResponseMessage CreateRightType(HttpRequestMessage request, [FromBody] RightTypeViewModel rightTypeViewModel)
        {
            TransactionalInformation transaction;

            RightType rightType = new RightType();
            rightType.Code = rightTypeViewModel.Code;
            rightType.Name = rightTypeViewModel.Name;
            rightType.Description = rightTypeViewModel.Description;

            RightTypeBusinessService rightTypeBusinessService = new RightTypeBusinessService(_rightTypeDataService);
            rightTypeBusinessService.CreateRightType(rightType, out transaction);
            if (transaction.ReturnStatus == false)
            {
                rightTypeViewModel.ReturnStatus = false;
                rightTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                rightTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<RightTypeViewModel>(HttpStatusCode.BadRequest, rightTypeViewModel);
                return responseError;

            }

            rightTypeViewModel.Id = rightType.Id;
            rightTypeViewModel.ReturnStatus = true;
            rightTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<RightTypeViewModel>(HttpStatusCode.OK, rightTypeViewModel);
            return response;

        }

        [Route("UpdateRightType")]
        [HttpPost]
        public HttpResponseMessage UpdateRightType(HttpRequestMessage request, [FromBody] RightTypeViewModel rightTypeViewModel)
        {
            TransactionalInformation transaction;

            RightType rightType = new RightType();
            rightType.Id = rightTypeViewModel.Id;
            rightType.Name = rightTypeViewModel.Name;
            rightType.Code = rightTypeViewModel.Code;
            rightType.Description = rightTypeViewModel.Description;


            RightTypeBusinessService rightTypeBusinessService = new RightTypeBusinessService(_rightTypeDataService);
            rightTypeBusinessService.UpdateRightType(rightType, out transaction);
            if (transaction.ReturnStatus == false)
            {
                rightTypeViewModel.ReturnStatus = false;
                rightTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                rightTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<RightTypeViewModel>(HttpStatusCode.BadRequest, rightTypeViewModel);
                return responseError;

            }

            rightTypeViewModel.ReturnStatus = true;
            rightTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<RightTypeViewModel>(HttpStatusCode.OK, rightTypeViewModel);
            return response;

        }
    }
}