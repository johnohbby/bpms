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
    [RoutePrefix("api/FolderService")]
    public class FolderServiceController : ApiController
    {
        public FolderServiceController()
   {
   }
        [Inject]
        public IFolderDataService _folderDataService { get; set; }        

        /// <summary>
        /// Get Action Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetFolders")]
        [HttpPost]
        public HttpResponseMessage GetFolders(HttpRequestMessage request, [FromBody] FolderViewModel folderViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = folderViewModel.CurrentPageNumber;
            int pageSize = folderViewModel.PageSize;
            string sortExpression = folderViewModel.SortExpression;
            string sortDirection = folderViewModel.SortDirection;

            FolderBusinessService folderBusinessService = new FolderBusinessService(_folderDataService);
            List<Folder> folders = folderBusinessService.GetFolders(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                folderViewModel.ReturnStatus = false;
                folderViewModel.ReturnMessage = transaction.ReturnMessage;
                folderViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FolderViewModel>(HttpStatusCode.BadRequest, folderViewModel);
                return responseError;

            }

            folderViewModel.TotalPages = transaction.TotalPages;
            folderViewModel.TotalRows = transaction.TotalRows;
            folderViewModel.Folders = folders;
            folderViewModel.ReturnStatus = true;
            folderViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FolderViewModel>(HttpStatusCode.OK, folderViewModel);
            return response;

        }

        /// <summary>
        /// Create Folder
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateFolder")]
        [HttpPost]
        public HttpResponseMessage CreateFolder(HttpRequestMessage request, [FromBody] FolderViewModel folderViewModel)
        {
            TransactionalInformation transaction;

            Folder folder = new Folder();
            folder.Name = folderViewModel.Name;
            folder.ParentFolderId = folderViewModel.ParentFolderId;


            FolderBusinessService folderBusinessService = new FolderBusinessService(_folderDataService);
            folderBusinessService.CreateFolder(folder, folderViewModel.UserId, out transaction);
            if (transaction.ReturnStatus == false)
            {
                folderViewModel.ReturnStatus = false;
                folderViewModel.ReturnMessage = transaction.ReturnMessage;
                folderViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FolderViewModel>(HttpStatusCode.BadRequest, folderViewModel);
                return responseError;

            }

            folderViewModel.Id = folder.Id;
            folderViewModel.ReturnStatus = true;
            folderViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FolderViewModel>(HttpStatusCode.OK, folderViewModel);
            return response;

        }

        [Route("DeleteFolder")]
        [HttpPost]
        public HttpResponseMessage DeleteFolder(HttpRequestMessage request, [FromBody] FolderViewModel folderViewModel)
        {
            TransactionalInformation transaction;

            Folder folder = new Folder();
            folder.Id = folderViewModel.Id;

            FolderBusinessService folderBusinessService = new FolderBusinessService(_folderDataService);
            folderBusinessService.DeleteFolder(folder, out transaction);
            if (transaction.ReturnStatus == false)
            {
                folderViewModel.ReturnStatus = false;
                folderViewModel.ReturnMessage = transaction.ReturnMessage;
                folderViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FolderViewModel>(HttpStatusCode.BadRequest, folderViewModel);
                return responseError;

            }

            folderViewModel.ReturnStatus = true;
            folderViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FolderViewModel>(HttpStatusCode.OK, folderViewModel);
            return response;

        }

        /// <summary>
        /// Get Action Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetFoldersForUser")]
        [HttpPost]
        public HttpResponseMessage GetFoldersForUser(HttpRequestMessage request, [FromBody] FolderViewModel folderViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = folderViewModel.CurrentPageNumber;
            int pageSize = folderViewModel.PageSize;
            string sortExpression = folderViewModel.SortExpression;
            string sortDirection = folderViewModel.SortDirection;
            long userId = folderViewModel.UserId;

            FolderBusinessService folderBusinessService = new FolderBusinessService(_folderDataService);
            List<Folder> folders = folderBusinessService.GetFoldersForUser(userId, currentPageNumber,pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                folderViewModel.ReturnStatus = false;
                folderViewModel.ReturnMessage = transaction.ReturnMessage;
                folderViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FolderViewModel>(HttpStatusCode.BadRequest, folderViewModel);
                return responseError;

            }

            folderViewModel.TotalPages = transaction.TotalPages;
            folderViewModel.TotalRows = transaction.TotalRows;
            folderViewModel.Folders = folders;
            folderViewModel.ReturnStatus = true;
            folderViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FolderViewModel>(HttpStatusCode.OK, folderViewModel);
            return response;

        }
    }
}