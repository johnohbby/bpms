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
    }
}