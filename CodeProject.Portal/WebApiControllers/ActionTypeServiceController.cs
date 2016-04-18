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
    [RoutePrefix("api/ActionTypeService")]
    public class ActionTypeServiceController : ApiController
    {
        public ActionTypeServiceController()
   {
   }
        [Inject]
        public IActionTypeDataService _actionTypeDataService { get; set; }        

        /// <summary>
        /// Get Action Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetActionTypes")]
        [HttpPost]
        public HttpResponseMessage GetActionTypes(HttpRequestMessage request, [FromBody] ActionTypeViewModel actionTypeViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = actionTypeViewModel.CurrentPageNumber;
            int pageSize = actionTypeViewModel.PageSize;
            string sortExpression = actionTypeViewModel.SortExpression;
            string sortDirection = actionTypeViewModel.SortDirection;

            ActionTypeBusinessService actionTypeBusinessService = new ActionTypeBusinessService(_actionTypeDataService);
            List<ActionType> actionTypes = actionTypeBusinessService.GetActionTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);

            if (transaction.ReturnStatus == false)
            {
                actionTypeViewModel.ReturnStatus = false;
                actionTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                actionTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ActionTypeViewModel>(HttpStatusCode.BadRequest, actionTypeViewModel);
                return responseError;

            }

            actionTypeViewModel.TotalPages = transaction.TotalPages;
            actionTypeViewModel.TotalRows = transaction.TotalRows;
            actionTypeViewModel.ActionTypes = actionTypes;
            actionTypeViewModel.ReturnStatus = true;
            actionTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ActionTypeViewModel>(HttpStatusCode.OK, actionTypeViewModel);
            return response;

        }

        /// <summary>
        /// Create Action Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateActionType")]
        [HttpPost]
        public HttpResponseMessage CreateActionType(HttpRequestMessage request, [FromBody] ActionTypeViewModel actionTypeViewModel)
        {
            TransactionalInformation transaction;

            ActionType actionType = new ActionType();
            actionType.Name = actionTypeViewModel.Name;
            actionType.WorkflowTypeId = actionTypeViewModel.WorkflowTypeId;

            ActionTypeBusinessService actionTypeBusinessService = new ActionTypeBusinessService(_actionTypeDataService);
            actionTypeBusinessService.CreateActionType(actionType, out transaction);
            if (transaction.ReturnStatus == false)
            {
                actionTypeViewModel.ReturnStatus = false;
                actionTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                actionTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ActionTypeViewModel>(HttpStatusCode.BadRequest, actionTypeViewModel);
                return responseError;

            }

            actionTypeViewModel.Id = actionType.Id;
            actionTypeViewModel.ReturnStatus = true;
            actionTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ActionTypeViewModel>(HttpStatusCode.OK, actionTypeViewModel);
            return response;

        }

        [Route("UpdateActionType")]
        [HttpPost]
        public HttpResponseMessage UpdateActionType(HttpRequestMessage request, [FromBody] ActionTypeViewModel actionTypeViewModel)
        {
            TransactionalInformation transaction;

            ActionType actionType = new ActionType();
            actionType.Id = actionTypeViewModel.Id;
            actionType.Name = actionTypeViewModel.Name;
            actionType.WorkflowTypeId = actionTypeViewModel.WorkflowTypeId;


            ActionTypeBusinessService actionTypeBusinessService = new ActionTypeBusinessService(_actionTypeDataService);
            actionTypeBusinessService.UpdateActionType(actionType, out transaction);
            if (transaction.ReturnStatus == false)
            {
                actionTypeViewModel.ReturnStatus = false;
                actionTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                actionTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ActionTypeViewModel>(HttpStatusCode.BadRequest, actionTypeViewModel);
                return responseError;

            }

            actionTypeViewModel.ReturnStatus = true;
            actionTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ActionTypeViewModel>(HttpStatusCode.OK, actionTypeViewModel);
            return response;

        }

        [Route("DeleteActionType")]
        [HttpPost]
        public HttpResponseMessage DeleteActionType(HttpRequestMessage request, [FromBody] ActionTypeViewModel actionTypeViewModel)
        {
            TransactionalInformation transaction;

            ActionType actionType = new ActionType();
            actionType.Id = actionTypeViewModel.Id;
            actionType.Name = actionTypeViewModel.Name;
            actionType.WorkflowTypeId = actionTypeViewModel.WorkflowTypeId;

            ActionTypeBusinessService actionTypeBusinessService = new ActionTypeBusinessService(_actionTypeDataService);
            actionTypeBusinessService.DeleteActionType(actionType, out transaction);
            if (transaction.ReturnStatus == false)
            {
                actionTypeViewModel.ReturnStatus = false;
                actionTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                actionTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ActionTypeViewModel>(HttpStatusCode.BadRequest, actionTypeViewModel);
                return responseError;

            }

            actionTypeViewModel.ReturnStatus = true;
            actionTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ActionTypeViewModel>(HttpStatusCode.OK, actionTypeViewModel);
            return response;

        }
    }
}