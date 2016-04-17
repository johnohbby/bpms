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
    [RoutePrefix("api/WorkflowService")]
    public class WorkflowServiceController : ApiController
    {
        public WorkflowServiceController()
   {
   }
        [Inject]
        public IWorkflowDataService _workflowDataService { get; set; }

        [Inject]
        public IWorkflowTypeDataService _workflowTypeDataService { get; set; }

        [Inject]
        public IActionDataService _actionDataService { get; set; }

        [Route("CreateWorkflow")]
        [HttpPost]
        public HttpResponseMessage CreateWorkflow(HttpRequestMessage request, [FromBody] WorkflowViewModel wfViewModel)
        {
            TransactionalInformation transaction;

            Workflow wf = new Workflow();
            wf.Name = wfViewModel.Name;
            wf.WorkflowTypeId = wfViewModel.WorkflowTypeId;
            wf.CreatedBy = wfViewModel.CreatedBy;

            WorkflowBusinessService wfBusinessService = new WorkflowBusinessService(_workflowDataService);
            wfBusinessService.CreateWorkflow(wf, out transaction);
            WorkflowViewModel wd = new WorkflowViewModel();
            if (transaction.ReturnStatus == false)
            {
                wd.ReturnStatus = false;
                wd.ReturnMessage = transaction.ReturnMessage;
                wd.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowViewModel>(HttpStatusCode.BadRequest, wd);
                return responseError;

            }

            wd.Id = wf.Id;
            wd.ReturnStatus = true;
            wd.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowViewModel>(HttpStatusCode.OK, wfViewModel);
            return response;

        }
        [Route("CreateAction")]
        [HttpPost]
        public HttpResponseMessage CreateAction(HttpRequestMessage request, [FromBody] ActionViewModel action)
        {
            TransactionalInformation transaction;

            Business.Entities.Action a = new Business.Entities.Action();
            a.WorkflowId = action.WorkflowId;
            a.ActionTypeId = action.ActionTypeId;
            a.CreatedBy = action.CreatedBy;
            

            WorkflowBusinessService wfBusinessService = new WorkflowBusinessService(_workflowDataService);
            wfBusinessService.CreateAction(action.Delegated , a, out transaction);
            ActionViewModel wd = new ActionViewModel();
            if (transaction.ReturnStatus == false)
            {
                wd.ReturnStatus = false;
                wd.ReturnMessage = transaction.ReturnMessage;
                wd.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ActionViewModel>(HttpStatusCode.BadRequest, wd);
                return responseError;

            }

            wd.Id = a.Id;
            wd.ReturnStatus = true;
            wd.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ActionViewModel>(HttpStatusCode.OK, action);
            return response;

        }
        [Route("DeleteWorkflow")]
        [HttpPost]
        public HttpResponseMessage DeleteWorkflow(HttpRequestMessage request, [FromBody] WorkflowViewModel wfViewModel)
        {
            TransactionalInformation transaction;

            Workflow wf = new Workflow();
            wf.Id = wfViewModel.Id;      

            WorkflowBusinessService wfBusinessService = new WorkflowBusinessService(_workflowDataService);
            wfBusinessService.DeleteWorkflow(wf, out transaction);
            WorkflowViewModel wd = new WorkflowViewModel();
            if (transaction.ReturnStatus == false)
            {
                wd.ReturnStatus = false;
                wd.ReturnMessage = transaction.ReturnMessage;
                wd.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowViewModel>(HttpStatusCode.BadRequest, wd);
                return responseError;

            }

            wd.Id = wf.Id;
            wd.ReturnStatus = true;
            wd.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowViewModel>(HttpStatusCode.OK, wfViewModel);
            return response;


        }


        [Route("UpdateWorkflow")]
        [HttpPost]
        public HttpResponseMessage UpdateWorkflow(HttpRequestMessage request, [FromBody] WorkflowViewModel wfViewModel)
        {
            TransactionalInformation transaction;

            Workflow wf = new Workflow();
            wf.Id = wfViewModel.Id;
            wf.Name = wfViewModel.Name;
            wf.CaseNumber = wfViewModel.CaseNumber;
            wf.LastActionId = wfViewModel.LastActionId;
            wf.WorkflowTypeId = wfViewModel.WorkflowTypeId;

            WorkflowBusinessService wfBusinessService = new WorkflowBusinessService(_workflowDataService);
            wfBusinessService.UpdateWorkflow(wf, out transaction);
            WorkflowViewModel wd = new WorkflowViewModel();
            if (transaction.ReturnStatus == false)
            {
                wd.ReturnStatus = false;
                wd.ReturnMessage = transaction.ReturnMessage;
                wd.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowViewModel>(HttpStatusCode.BadRequest, wd);
                return responseError;

            }

            wd.Id = wf.Id;
            wd.ReturnStatus = true;
            wd.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowViewModel>(HttpStatusCode.OK, wfViewModel);
            return response;


        }


        [Route("GetWorkflows")]
        [HttpPost]
        public HttpResponseMessage GetWorkflows(HttpRequestMessage request, [FromBody] WorkflowViewModel workflowViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = workflowViewModel.CurrentPageNumber;
            int pageSize = workflowViewModel.PageSize;
            string sortExpression = workflowViewModel.SortExpression;
            string sortDirection = workflowViewModel.SortDirection;

            WorkflowBusinessService workflowBusinessService = new WorkflowBusinessService(_workflowDataService);
            List<Workflow> workflows = workflowBusinessService.GetWorkflows(workflowViewModel.FolderId, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                workflowViewModel.ReturnStatus = false;
                workflowViewModel.ReturnMessage = transaction.ReturnMessage;
                workflowViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowViewModel>(HttpStatusCode.BadRequest, workflowViewModel);
                return responseError;

            }

            workflowViewModel.TotalPages = transaction.TotalPages;
            workflowViewModel.TotalRows = transaction.TotalRows;
            workflowViewModel.Workflows = workflows;
            workflowViewModel.ReturnStatus = true;
            workflowViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowViewModel>(HttpStatusCode.OK, workflowViewModel);
            return response;

        }

        [Route("GetWorkflowData")]
        [HttpPost]
        public HttpResponseMessage GetWorkflowData(HttpRequestMessage request, [FromBody] WorkflowDataViewModel workflowDataViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = workflowDataViewModel.CurrentPageNumber;
            int pageSize = workflowDataViewModel.PageSize;
            string sortExpression = workflowDataViewModel.SortExpression;
            string sortDirection = workflowDataViewModel.SortDirection;

            WorkflowBusinessService workflowBusinessService = new WorkflowBusinessService(_workflowDataService);
            WorkflowTypeBusinessService workflowTypeBusinessService = new WorkflowTypeBusinessService(_workflowTypeDataService);
            List<WorkflowFolder> workflowFolders = workflowBusinessService.GetWorkflowFolders(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            List<WorkflowType> workflowTypes = workflowTypeBusinessService.GetWorkflowTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                workflowDataViewModel.ReturnStatus = false;
                workflowDataViewModel.ReturnMessage = transaction.ReturnMessage;
                workflowDataViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowDataViewModel>(HttpStatusCode.BadRequest, workflowDataViewModel);
                return responseError;

            }

            workflowDataViewModel.TotalPages = transaction.TotalPages;
            workflowDataViewModel.TotalRows = transaction.TotalRows;
            workflowDataViewModel.WorkflowFolders = workflowFolders;
            workflowDataViewModel.WorkflowTypes = workflowTypes;
            workflowDataViewModel.ReturnStatus = true;
            workflowDataViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowDataViewModel>(HttpStatusCode.OK, workflowDataViewModel);
            return response;

        }

        [Route("GetWorkflowDataById")]
        [HttpPost]
        public HttpResponseMessage GetWorkflowDataById(HttpRequestMessage request, [FromBody] WorkflowDataViewModel workflowDataViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = workflowDataViewModel.CurrentPageNumber;
            int pageSize = workflowDataViewModel.PageSize;
            string sortExpression = workflowDataViewModel.SortExpression;
            string sortDirection = workflowDataViewModel.SortDirection;
            long id = workflowDataViewModel.Id;
            long userId = workflowDataViewModel.UserId;
            ActionBusinessService actionBusinessService = new ActionBusinessService(_actionDataService);
            List<Business.Entities.Action> actions = actionBusinessService.GetActionsForUser(userId, id, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            List<Business.Entities.ActionType> actionTypes = actionBusinessService.GetNextActionTypesForUser(userId, id, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                workflowDataViewModel.ReturnStatus = false;
                workflowDataViewModel.ReturnMessage = transaction.ReturnMessage;
                workflowDataViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowDataViewModel>(HttpStatusCode.BadRequest, workflowDataViewModel);
                return responseError;

            }

            workflowDataViewModel.TotalPages = transaction.TotalPages;
            workflowDataViewModel.TotalRows = transaction.TotalRows;

            workflowDataViewModel.Actions = actions;
            workflowDataViewModel.NextActionTypes = actionTypes;
            workflowDataViewModel.ReturnStatus = true;
            workflowDataViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowDataViewModel>(HttpStatusCode.OK, workflowDataViewModel);
            return response;

        }

        [Route("GetDelegated")]
        [HttpPost]
        public HttpResponseMessage GetDelegated(HttpRequestMessage request, [FromBody] ActionViewModel actionViewModel)
        {

            TransactionalInformation transaction;
            
            long workflowId = actionViewModel.WorkflowId;
            long userId = actionViewModel.CreatedBy;
            long actionTypeId = actionViewModel.ActionTypeId;

            ActionBusinessService actionBusinessService = new ActionBusinessService(_actionDataService);
            List<Business.Entities.User> delegated = actionBusinessService.GetDelegated(userId, workflowId, actionTypeId, out transaction);
            if (transaction.ReturnStatus == false)
            {
                actionViewModel.ReturnStatus = false;
                actionViewModel.ReturnMessage = transaction.ReturnMessage;
                actionViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<ActionViewModel>(HttpStatusCode.BadRequest, actionViewModel);
                return responseError;

            }

            actionViewModel.TotalPages = transaction.TotalPages;
            actionViewModel.TotalRows = transaction.TotalRows;

            actionViewModel.Delegated = delegated;
            
            actionViewModel.ReturnStatus = true;
            actionViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<ActionViewModel>(HttpStatusCode.OK, actionViewModel);
            return response;

        }
    }
}