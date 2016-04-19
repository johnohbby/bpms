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
    [RoutePrefix("api/UserGroupService")]
    public class UserGroupServiceController : ApiController
    {
        public UserGroupServiceController()
   {
   }
        [Inject]
        public IUserGroupDataService _userGroupDataService { get; set; }        

        /// <summary>
        /// Get Action Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetUserGroups")]
        [HttpPost]
        public HttpResponseMessage GetUserGroups(HttpRequestMessage request, [FromBody] UserGroupViewModel userGroupViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = userGroupViewModel.CurrentPageNumber;
            int pageSize = userGroupViewModel.PageSize;
            string sortExpression = userGroupViewModel.SortExpression;
            string sortDirection = userGroupViewModel.SortDirection;

            UserGroupBusinessService userGroupBusinessService = new UserGroupBusinessService(_userGroupDataService);
            List<UserGroup> userGroups = userGroupBusinessService.GetUserGroups(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);

            if (transaction.ReturnStatus == false)
            {
                userGroupViewModel.ReturnStatus = false;
                userGroupViewModel.ReturnMessage = transaction.ReturnMessage;
                userGroupViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<UserGroupViewModel>(HttpStatusCode.BadRequest, userGroupViewModel);
                return responseError;

            }

            userGroupViewModel.TotalPages = transaction.TotalPages;
            userGroupViewModel.TotalRows = transaction.TotalRows;
            userGroupViewModel.UserGroups = userGroups;
            userGroupViewModel.ReturnStatus = true;
            userGroupViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<UserGroupViewModel>(HttpStatusCode.OK, userGroupViewModel);
            return response;

        }

        /// <summary>
        /// Create User Group
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateUserGroup")]
        [HttpPost]
        public HttpResponseMessage CreateUserGroup(HttpRequestMessage request, [FromBody] UserGroupViewModel userGroupViewModel)
        {
            TransactionalInformation transaction;

            UserGroup userGroup = new UserGroup();
            userGroup.UserId = userGroupViewModel.UserId;
            userGroup.GroupId = userGroupViewModel.GroupId;

            UserGroupBusinessService userGroupBusinessService = new UserGroupBusinessService(_userGroupDataService);
            userGroupBusinessService.CreateUserGroup(userGroup, out transaction);
            if (transaction.ReturnStatus == false)
            {
                userGroupViewModel.ReturnStatus = false;
                userGroupViewModel.ReturnMessage = transaction.ReturnMessage;
                userGroupViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<UserGroupViewModel>(HttpStatusCode.BadRequest, userGroupViewModel);
                return responseError;

            }

            userGroupViewModel.Id = userGroup.Id;
            userGroupViewModel.ReturnStatus = true;
            userGroupViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<UserGroupViewModel>(HttpStatusCode.OK, userGroupViewModel);
            return response;

        }

        [Route("UpdateUserGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateUserGroup(HttpRequestMessage request, [FromBody] UserGroupViewModel userGroupViewModel)
        {
            TransactionalInformation transaction;

            UserGroup userGroup = new UserGroup();
            userGroup.Id = userGroupViewModel.Id;
            userGroup.UserId = userGroupViewModel.UserId;
            userGroup.GroupId = userGroupViewModel.GroupId;

            UserGroupBusinessService userGroupBusinessService = new UserGroupBusinessService(_userGroupDataService);
            userGroupBusinessService.UpdateUserGroup(userGroup, out transaction);
            if (transaction.ReturnStatus == false)
            {
                userGroupViewModel.ReturnStatus = false;
                userGroupViewModel.ReturnMessage = transaction.ReturnMessage;
                userGroupViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<UserGroupViewModel>(HttpStatusCode.BadRequest, userGroupViewModel);
                return responseError;

            }

            userGroupViewModel.ReturnStatus = true;
            userGroupViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<UserGroupViewModel>(HttpStatusCode.OK, userGroupViewModel);
            return response;

        }

        [Route("DeleteUserGroup")]
        [HttpPost]
        public HttpResponseMessage DeleteUserGroup(HttpRequestMessage request, [FromBody] UserGroupViewModel userGroupViewModel)
        {
            TransactionalInformation transaction;

            UserGroup userGroup = new UserGroup();
            userGroup.Id = userGroupViewModel.Id;
            userGroup.UserId = userGroupViewModel.UserId;
            userGroup.GroupId = userGroupViewModel.GroupId;

            UserGroupBusinessService userGroupBusinessService = new UserGroupBusinessService(_userGroupDataService);
            userGroupBusinessService.DeleteUserGroup(userGroup, out transaction);
            if (transaction.ReturnStatus == false)
            {
                userGroupViewModel.ReturnStatus = false;
                userGroupViewModel.ReturnMessage = transaction.ReturnMessage;
                userGroupViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<UserGroupViewModel>(HttpStatusCode.BadRequest, userGroupViewModel);
                return responseError;

            }

            userGroupViewModel.ReturnStatus = true;
            userGroupViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<UserGroupViewModel>(HttpStatusCode.OK, userGroupViewModel);
            return response;

        }
    }
}