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
    [RoutePrefix("api/MailTemplateMapService")]
    public class MailTemplateMapServiceController : ApiController
    {
        public MailTemplateMapServiceController()
   {
   }
        [Inject]
        public IMailTemplateMapDataService _mailTemplateMapDataService { get; set; }        

        /// <summary>
        /// Get Action Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetMapsForMailTemplate")]
        [HttpPost]
        public HttpResponseMessage GetMapsForMailTemplate(HttpRequestMessage request, [FromBody] MailTemplateMapViewModel mailTemplateMapViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = mailTemplateMapViewModel.CurrentPageNumber;
            int pageSize = mailTemplateMapViewModel.PageSize;
            string sortExpression = mailTemplateMapViewModel.SortExpression;
            string sortDirection = mailTemplateMapViewModel.SortDirection;
            long mailTemplateId = mailTemplateMapViewModel.MailTemplateId;

            MailTemplateMapBusinessService mailTemplateMapBusinessService = new MailTemplateMapBusinessService(_mailTemplateMapDataService);
            List<MailTemplateMap> MailTemplateMaps = mailTemplateMapBusinessService.GetMapsForMailTemplate(mailTemplateId, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);

            if (transaction.ReturnStatus == false)
            {
                mailTemplateMapViewModel.ReturnStatus = false;
                mailTemplateMapViewModel.ReturnMessage = transaction.ReturnMessage;
                mailTemplateMapViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<MailTemplateMapViewModel>(HttpStatusCode.BadRequest, mailTemplateMapViewModel);
                return responseError;

            }

            mailTemplateMapViewModel.TotalPages = transaction.TotalPages;
            mailTemplateMapViewModel.TotalRows = transaction.TotalRows;
            mailTemplateMapViewModel.MailTemplateMaps = MailTemplateMaps;
            mailTemplateMapViewModel.ReturnStatus = true;
            mailTemplateMapViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<MailTemplateMapViewModel>(HttpStatusCode.OK, mailTemplateMapViewModel);
            return response;

        }


        /// <summary>
        /// Create
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateMailTemplateMap")]
        [HttpPost]
        public HttpResponseMessage CreateMailTemplateMap(HttpRequestMessage request, [FromBody] MailTemplateMapViewModel mailTemplateMapViewModel)
        {
            TransactionalInformation transaction;

            MailTemplateMap mailTemplateMap = new MailTemplateMap();
            mailTemplateMap.MailTemplateId = mailTemplateMapViewModel.MailTemplateId;
            mailTemplateMap.ActionTypeId = mailTemplateMapViewModel.ActionTypeId;

            MailTemplateMapBusinessService mailTemplateMapBusinessService = new MailTemplateMapBusinessService(_mailTemplateMapDataService);
            mailTemplateMapBusinessService.CreateMailTemplateMap(mailTemplateMap, out transaction);
            if (transaction.ReturnStatus == false)
            {
                mailTemplateMapViewModel.ReturnStatus = false;
                mailTemplateMapViewModel.ReturnMessage = transaction.ReturnMessage;
                mailTemplateMapViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<MailTemplateMapViewModel>(HttpStatusCode.BadRequest, mailTemplateMapViewModel);
                return responseError;

            }

            mailTemplateMapViewModel.Id = mailTemplateMap.Id;
            mailTemplateMapViewModel.ReturnStatus = true;
            mailTemplateMapViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<MailTemplateMapViewModel>(HttpStatusCode.OK, mailTemplateMapViewModel);
            return response;

        }

       
        [Route("DeleteMailTemplateMap")]
        [HttpPost]
        public HttpResponseMessage DeleteMailTemplateMap(HttpRequestMessage request, [FromBody] MailTemplateMapViewModel mailTemplateMapViewModel)
        {
            TransactionalInformation transaction;

            MailTemplateMap mailTemplateMap = new MailTemplateMap();
            mailTemplateMap.Id = mailTemplateMapViewModel.Id;

            MailTemplateMapBusinessService mailTemplateMapBusinessService = new MailTemplateMapBusinessService(_mailTemplateMapDataService);
            mailTemplateMapBusinessService.DeleteMailTemplateMap(mailTemplateMap, out transaction);
            if (transaction.ReturnStatus == false)
            {
                mailTemplateMapViewModel.ReturnStatus = false;
                mailTemplateMapViewModel.ReturnMessage = transaction.ReturnMessage;
                mailTemplateMapViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<MailTemplateMapViewModel>(HttpStatusCode.BadRequest, mailTemplateMapViewModel);
                return responseError;

            }

            mailTemplateMapViewModel.ReturnStatus = true;
            mailTemplateMapViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<MailTemplateMapViewModel>(HttpStatusCode.OK, mailTemplateMapViewModel);
            return response;

        }
    }
}