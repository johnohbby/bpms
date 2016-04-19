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
    [RoutePrefix("api/GroupService")]
    public class GroupServiceController : ApiController
    {
        public GroupServiceController()
   {
   }
        [Inject]
        public IGroupDataService _groupDataService { get; set; }

        [Inject]
        public IGroupTypeDataService _groupTypeDataService { get; set; }


        

        /// <summary>
        /// Get Groups
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetGroups")]
        [HttpPost]
        public HttpResponseMessage GetGroups(HttpRequestMessage request, [FromBody] GroupViewModel groupViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = groupViewModel.CurrentPageNumber;
            int pageSize = groupViewModel.PageSize;
            string sortExpression = groupViewModel.SortExpression;
            string sortDirection = groupViewModel.SortDirection;

            GroupBusinessService groupBusinessService = new GroupBusinessService(_groupDataService);
            List<Group> groups = groupBusinessService.GetGroups(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                groupViewModel.ReturnStatus = false;
                groupViewModel.ReturnMessage = transaction.ReturnMessage;
                groupViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<GroupViewModel>(HttpStatusCode.BadRequest, groupViewModel);
                return responseError;

            }

            groupViewModel.TotalPages = transaction.TotalPages;
            groupViewModel.TotalRows = transaction.TotalRows;
            groupViewModel.Groups = groups;
            groupViewModel.ReturnStatus = true;
            groupViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<GroupViewModel>(HttpStatusCode.OK, groupViewModel);
            return response;

        }

        /// <summary>
        /// Create Workflow Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="groupViewModel"></param>
        /// <returns></returns>
        [Route("CreateGroup")]
        [HttpPost]
        public HttpResponseMessage CreateGroup(HttpRequestMessage request, [FromBody] GroupViewModel groupViewModel)
        {
            TransactionalInformation transaction;

            Group group = new Group();
            group.Name = groupViewModel.Name;
            group.Email = groupViewModel.Email;
            group.GroupTypeId = groupViewModel.GroupTypeId;

            GroupBusinessService groupBusinessService = new GroupBusinessService(_groupDataService);
            groupBusinessService.CreateGroup(group, out transaction);
            if (transaction.ReturnStatus == false)
            {
                groupViewModel.ReturnStatus = false;
                groupViewModel.ReturnMessage = transaction.ReturnMessage;
                groupViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<GroupViewModel>(HttpStatusCode.BadRequest, groupViewModel);
                return responseError;

            }

            groupViewModel.Id = group.Id;
            groupViewModel.ReturnStatus = true;
            groupViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<GroupViewModel>(HttpStatusCode.OK, groupViewModel);
            return response;

        }

        [Route("UpdateGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateGroup(HttpRequestMessage request, [FromBody] GroupViewModel groupViewModel)
        {
            TransactionalInformation transaction;

            Group group = new Group();
            group.Id = groupViewModel.Id;
            group.Name = groupViewModel.Name;
            group.Email = groupViewModel.Email;
            group.GroupTypeId = groupViewModel.GroupTypeId;

            GroupBusinessService groupBusinessService = new GroupBusinessService(_groupDataService);
            groupBusinessService.UpdateGroup(group, out transaction);
            if (transaction.ReturnStatus == false)
            {
                groupViewModel.ReturnStatus = false;
                groupViewModel.ReturnMessage = transaction.ReturnMessage;
                groupViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<GroupViewModel>(HttpStatusCode.BadRequest, groupViewModel);
                return responseError;

            }

            groupViewModel.ReturnStatus = true;
            groupViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<GroupViewModel>(HttpStatusCode.OK, groupViewModel);
            return response;

        }

        [Route("DeleteGroup")]
        [HttpPost]
        public HttpResponseMessage DeleteGroup(HttpRequestMessage request, [FromBody] GroupViewModel groupViewModel)
        {
            TransactionalInformation transaction;

            Group group = new Group();
            group.Id = groupViewModel.Id;
            group.Name = groupViewModel.Name;
            group.Email = groupViewModel.Email;
            group.GroupTypeId = groupViewModel.GroupTypeId;

            GroupBusinessService groupBusinessService = new GroupBusinessService(_groupDataService);
            groupBusinessService.DeleteGroup(group, out transaction);
            if (transaction.ReturnStatus == false)
            {
                groupViewModel.ReturnStatus = false;
                groupViewModel.ReturnMessage = transaction.ReturnMessage;
                groupViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<GroupViewModel>(HttpStatusCode.BadRequest, groupViewModel);
                return responseError;

            }

            groupViewModel.ReturnStatus = true;
            groupViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<GroupViewModel>(HttpStatusCode.OK, groupViewModel);
            return response;

        }

        /// <summary>
        /// Get Group Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetGroupTypes")]
        [HttpPost]
        public HttpResponseMessage GetGroupTypes(HttpRequestMessage request, [FromBody] GroupTypeViewModel groupTypeViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = groupTypeViewModel.CurrentPageNumber;
            int pageSize = groupTypeViewModel.PageSize;
            string sortExpression = groupTypeViewModel.SortExpression;
            string sortDirection = groupTypeViewModel.SortDirection;

            GroupTypeBusinessService groupTypeBusinessService = new GroupTypeBusinessService(_groupTypeDataService);
            List<GroupType> groupTypes = groupTypeBusinessService.GetGroupTypes(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                groupTypeViewModel.ReturnStatus = false;
                groupTypeViewModel.ReturnMessage = transaction.ReturnMessage;
                groupTypeViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<GroupTypeViewModel>(HttpStatusCode.BadRequest, groupTypeViewModel);
                return responseError;

            }

            groupTypeViewModel.TotalPages = transaction.TotalPages;
            groupTypeViewModel.TotalRows = transaction.TotalRows;
            groupTypeViewModel.GroupTypes = groupTypes;
            groupTypeViewModel.ReturnStatus = true;
            groupTypeViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<GroupTypeViewModel>(HttpStatusCode.OK, groupTypeViewModel);
            return response;

        }
    }
}