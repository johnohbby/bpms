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
    [RoutePrefix("api/WorkflowTypeService")]
    public class WorkflowTypeServiceController : ApiController
    {

        [Inject]
        public IWorkflowTypeDataService _workflowTypeDataService { get; set; }

        /// <summary>
        /// Create Workflow Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="WorkflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("CreateWorkflowType")]
        [HttpPost]
        public HttpResponseMessage CreateWorkflowType(HttpRequestMessage request, [FromBody] WorkflowTypeViewModel workflowTypeViewModel)
        {
            TransactionalInformation transaction;

            WorkflowType workflowType = new WorkflowType();
            workflowType.Name = workflowTypeViewModel.Name;
            workflowType.Description = workflowTypeViewModel.Description;

            WorkflowTypeBusinessService workflowTypeBusinessService = new WorkflowTypeBusinessService(_workflowTypeDataService);
            workflowTypeBusinessService.CreateWorkflowType(workflowType, out transaction);
            if (transaction.ReturnStatus == false)
            {
                workflowTypeViewModel.ReturnStatus = false;
                workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                workflowTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.BadRequest, workflowTypeViewModel);
                return responseError;

            }

            workflowTypeViewModel.Id = workflowType.Id;
            workflowTypeViewModel.ReturnStatus = true;
            workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.OK, workflowTypeViewModel);
            return response;

        }

        [Route("UpdateWorkflowType")]
        [HttpPost]
        public HttpResponseMessage UpdateWorkflowType(HttpRequestMessage request, [FromBody] WorkflowTypeViewModel workflowTypeViewModel)
        {
            TransactionalInformation transaction;

            WorkflowType workflowType = new WorkflowType();
            workflowType.Id = workflowTypeViewModel.Id;
            workflowType.Name = workflowTypeViewModel.Name;
            workflowType.Description = workflowTypeViewModel.Description;

            WorkflowTypeBusinessService workflowTypeBusinessService = new WorkflowTypeBusinessService(_workflowTypeDataService);
            workflowTypeBusinessService.UpdateWorkflowType(workflowType, out transaction);
            if (transaction.ReturnStatus == false)
            {
                workflowTypeViewModel.ReturnStatus = false;
                workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                workflowTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.BadRequest, workflowTypeViewModel);
                return responseError;

            }

            workflowTypeViewModel.ReturnStatus = true;
            workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.OK, workflowTypeViewModel);
            return response;

        }

        /// <summary>
        /// Get WorkflowTypes
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetWorkflowTypes")]
        [HttpPost]
        public HttpResponseMessage GetWorkflowTypes(HttpRequestMessage request, [FromBody] WorkflowTypeViewModel workflowTypeViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = workflowTypeViewModel.CurrentPageNumber;
            int pageSize = workflowTypeViewModel.PageSize;
            string sortExpression = workflowTypeViewModel.SortExpression;
            string sortDirection = workflowTypeViewModel.SortDirection;

            WorkflowTypeBusinessService workflowTypeBusinessService = new WorkflowTypeBusinessService(_workflowTypeDataService);
            List<WorkflowType> workflowTypes = workflowTypeBusinessService.GetAllWorkflowTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                workflowTypeViewModel.ReturnStatus = false;
                workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                workflowTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.BadRequest, workflowTypeViewModel);
                return responseError;

            }

            workflowTypeViewModel.TotalPages = transaction.TotalPages;
            workflowTypeViewModel.TotalRows = transaction.TotalRows;
            workflowTypeViewModel.WorkflowTypes = workflowTypes;
            workflowTypeViewModel.ReturnStatus = true;
            workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.OK, workflowTypeViewModel);
            return response;

        }
       
        /// <summary>
        /// Get WorkflowType
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetWorkflowType")]
        [HttpPost]
        public HttpResponseMessage GetWorkflowType(HttpRequestMessage request, [FromBody] WorkflowTypeViewModel workflowTypeViewModel)
        {

            TransactionalInformation transaction;

            long workflowTypeID = workflowTypeViewModel.Id;

            WorkflowTypeBusinessService workflowTypeBusinessService = new WorkflowTypeBusinessService(_workflowTypeDataService);
            WorkflowType workflowType = workflowTypeBusinessService.GetworkflowType(workflowTypeID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                workflowTypeViewModel.ReturnStatus = false;
                workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                workflowTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.BadRequest, workflowTypeViewModel);
                return responseError;

            }

            workflowTypeViewModel.Id = workflowType.Id;
            workflowTypeViewModel.Name = workflowType.Name;
            workflowTypeViewModel.Description = workflowType.Description;
                     
            workflowTypeViewModel.ReturnStatus = true;
            workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.OK, workflowTypeViewModel);
            return response;

        }

        [Route("DeleteWorkflowType")]
        [HttpPost]
        public HttpResponseMessage DeleteWorkflowType(HttpRequestMessage request, [FromBody] WorkflowTypeViewModel workflowTypeViewModel)
        {
            TransactionalInformation transaction;

            WorkflowType workflowType = new WorkflowType();
            workflowType.Id = workflowTypeViewModel.Id;
            workflowType.Name = workflowTypeViewModel.Name;
            workflowType.Description = workflowTypeViewModel.Description;


            WorkflowTypeBusinessService workflowTypeBusinessService = new WorkflowTypeBusinessService(_workflowTypeDataService);
            workflowTypeBusinessService.DeleteWorkflowType(workflowType, out transaction);
            if (transaction.ReturnStatus == false)
            {
                workflowTypeViewModel.ReturnStatus = false;
                workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                workflowTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.BadRequest, workflowTypeViewModel);
                return responseError;

            }

            workflowTypeViewModel.ReturnStatus = true;
            workflowTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<WorkflowTypeViewModel>(HttpStatusCode.OK, workflowTypeViewModel);
            return response;

        }



    }
}