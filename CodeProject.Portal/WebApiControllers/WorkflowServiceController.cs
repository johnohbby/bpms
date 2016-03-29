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


        

        /// <summary>
        /// Get Right Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
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
            List<Workflow> workflows = workflowBusinessService.GetWorkflows(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
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
    }
}