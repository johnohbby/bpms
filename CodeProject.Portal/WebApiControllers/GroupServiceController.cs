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

       
    }
}