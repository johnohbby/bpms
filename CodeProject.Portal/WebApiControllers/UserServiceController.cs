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
    [RoutePrefix("api/UserService")]
    public class UserServiceController : ApiController
    {

        [Inject]
        public IUserDataService _userDataService { get; set; }

        /// <summary>
        /// Create Workflow Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UserViewModel"></param>
        /// <returns></returns>
        [Route("CreateUser")]
        [HttpPost]
        public HttpResponseMessage CreateUser(HttpRequestMessage request, [FromBody] UserViewModel userViewModel)
        {
            TransactionalInformation transaction;

            User user = new User();
            user.Name = userViewModel.Name;
            //to do



            UserBusinessService userBusinessService = new UserBusinessService(_userDataService);
            userBusinessService.CreateUser(user, out transaction);
            if (transaction.ReturnStatus == false)
            {
                userViewModel.ReturnStatus = false;
                userViewModel.ReturnMessage = transaction.ReturnMessage;
                userViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<UserViewModel>(HttpStatusCode.BadRequest, userViewModel);
                return responseError;

            }

            userViewModel.Id = user.Id;
            userViewModel.ReturnStatus = true;
            userViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<UserViewModel>(HttpStatusCode.OK, userViewModel);
            return response;

        }

        [Route("UpdateUser")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(HttpRequestMessage request, [FromBody] UserViewModel userViewModel)
        {
            TransactionalInformation transaction;

            User user = new User();
            user.Id = userViewModel.Id;
            user.Name = userViewModel.Name;
                      

            UserBusinessService userBusinessService = new UserBusinessService(_userDataService);
            userBusinessService.UpdateUser(user, out transaction);
            if (transaction.ReturnStatus == false)
            {
                userViewModel.ReturnStatus = false;
                userViewModel.ReturnMessage = transaction.ReturnMessage;
                userViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<UserViewModel>(HttpStatusCode.BadRequest, userViewModel);
                return responseError;

            }

            userViewModel.ReturnStatus = true;
            userViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<UserViewModel>(HttpStatusCode.OK, userViewModel);
            return response;

        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [Route("GetUsers")]
        [HttpPost]
        public HttpResponseMessage GetUsers(HttpRequestMessage request, [FromBody] UserViewModel userViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = userViewModel.CurrentPageNumber;
            int pageSize = userViewModel.PageSize;
            string sortExpression = userViewModel.SortExpression;
            string sortDirection = userViewModel.SortDirection;

            UserBusinessService userBusinessService = new UserBusinessService(_userDataService);
            List<User> users = userBusinessService.GetUsers(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                userViewModel.ReturnStatus = false;
                userViewModel.ReturnMessage = transaction.ReturnMessage;
                userViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<UserViewModel>(HttpStatusCode.BadRequest, userViewModel);
                return responseError;

            }

            userViewModel.TotalPages = transaction.TotalPages;
            userViewModel.TotalRows = transaction.TotalRows;
            
            userViewModel.ReturnStatus = true;
            userViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<UserViewModel>(HttpStatusCode.OK, userViewModel);
            return response;

        }
        [Route("LoginTest")]
        [HttpPost]
        public HttpResponseMessage LoginTest(HttpRequestMessage request, [FromBody] UserViewModel userViewModel)
        {

            
            var response = Request.CreateResponse<UserViewModel>(HttpStatusCode.OK, userViewModel);
            return response;
        }
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [Route("GetUser")]
        [HttpPost]
        public HttpResponseMessage GetUser(HttpRequestMessage request, [FromBody] UserViewModel userViewModel)
        {

            TransactionalInformation transaction;

            long userID = userViewModel.Id;

            UserBusinessService userBusinessService = new UserBusinessService(_userDataService);
            User user = userBusinessService.GetUserByCredientials(userViewModel.Username, userViewModel.Password, out transaction);
            if (transaction.ReturnStatus == false)
            {
                userViewModel.ReturnStatus = false;
                userViewModel.ReturnMessage = transaction.ReturnMessage;
                userViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<UserViewModel>(HttpStatusCode.BadRequest, userViewModel);
                return responseError;

            }

            if(user != null) { 
                userViewModel.Id = user.Id;
                userViewModel.Name = user.Name;
                userViewModel.Email = user.Email;
            }

            userViewModel.ReturnStatus = true;
            userViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<UserViewModel>(HttpStatusCode.OK, userViewModel);
            return response;

        }



    }
}