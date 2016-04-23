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
    [RoutePrefix("api/MailTemplateService")]
    public class MailTemplateServiceController : ApiController
    {
        public MailTemplateServiceController()
   {
   }
        [Inject]
        public IMailTemplateDataService _mailTemplateDataService { get; set; }        

        /// <summary>
        /// Get Action Types
        /// </summary>
        /// <param name="request"></param>
        /// <param name="workflowTypeViewModel"></param>
        /// <returns></returns>
        [Route("GetMailTemplates")]
        [HttpPost]
        public HttpResponseMessage GetMailTemplates(HttpRequestMessage request, [FromBody] MailTemplateViewModel mailTemplateViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = mailTemplateViewModel.CurrentPageNumber;
            int pageSize = mailTemplateViewModel.PageSize;
            string sortExpression = mailTemplateViewModel.SortExpression;
            string sortDirection = mailTemplateViewModel.SortDirection;

            MailTemplateBusinessService mailTemplateBusinessService = new MailTemplateBusinessService(_mailTemplateDataService);
            List<MailTemplate> MailTemplates = mailTemplateBusinessService.GetMailTemplates(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);

            if (transaction.ReturnStatus == false)
            {
                mailTemplateViewModel.ReturnStatus = false;
                mailTemplateViewModel.ReturnMessage = transaction.ReturnMessage;
                mailTemplateViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<MailTemplateViewModel>(HttpStatusCode.BadRequest, mailTemplateViewModel);
                return responseError;

            }

            mailTemplateViewModel.TotalPages = transaction.TotalPages;
            mailTemplateViewModel.TotalRows = transaction.TotalRows;
            mailTemplateViewModel.MailTemplates = MailTemplates;
            mailTemplateViewModel.ReturnStatus = true;
            mailTemplateViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<MailTemplateViewModel>(HttpStatusCode.OK, mailTemplateViewModel);
            return response;

        }


        /// <summary>
        /// Create Action Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [Route("CreateMailTemplate")]
        [HttpPost]
        public HttpResponseMessage CreateMailTemplate(HttpRequestMessage request, [FromBody] MailTemplateViewModel mailTemplateViewModel)
        {
            TransactionalInformation transaction;

            MailTemplate MailTemplate = new MailTemplate();
            MailTemplate.Body = mailTemplateViewModel.Body;
            MailTemplate.Subject = mailTemplateViewModel.Subject;

            MailTemplateBusinessService MailTemplateBusinessService = new MailTemplateBusinessService(_mailTemplateDataService);
            MailTemplateBusinessService.CreateMailTemplate(MailTemplate, out transaction);
            if (transaction.ReturnStatus == false)
            {
                mailTemplateViewModel.ReturnStatus = false;
                mailTemplateViewModel.ReturnMessage = transaction.ReturnMessage;
                mailTemplateViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<MailTemplateViewModel>(HttpStatusCode.BadRequest, mailTemplateViewModel);
                return responseError;

            }

            mailTemplateViewModel.Id = MailTemplate.Id;
            mailTemplateViewModel.ReturnStatus = true;
            mailTemplateViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<MailTemplateViewModel>(HttpStatusCode.OK, mailTemplateViewModel);
            return response;

        }

        [Route("UpdateMailTemplate")]
        [HttpPost]
        public HttpResponseMessage UpdateMailTemplate(HttpRequestMessage request, [FromBody] MailTemplateViewModel mailTemplateViewModel)
        {
            TransactionalInformation transaction;

            MailTemplate MailTemplate = new MailTemplate();
            MailTemplate.Id = mailTemplateViewModel.Id;
            MailTemplate.Body = mailTemplateViewModel.Body;
            MailTemplate.Subject = mailTemplateViewModel.Subject;


            MailTemplateBusinessService MailTemplateBusinessService = new MailTemplateBusinessService(_mailTemplateDataService);
            MailTemplateBusinessService.UpdateMailTemplate(MailTemplate, out transaction);
            if (transaction.ReturnStatus == false)
            {
                mailTemplateViewModel.ReturnStatus = false;
                mailTemplateViewModel.ReturnMessage = transaction.ReturnMessage;
                mailTemplateViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<MailTemplateViewModel>(HttpStatusCode.BadRequest, mailTemplateViewModel);
                return responseError;

            }

            mailTemplateViewModel.ReturnStatus = true;
            mailTemplateViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<MailTemplateViewModel>(HttpStatusCode.OK, mailTemplateViewModel);
            return response;

        }

        [Route("DeleteMailTemplate")]
        [HttpPost]
        public HttpResponseMessage DeleteMailTemplate(HttpRequestMessage request, [FromBody] MailTemplateViewModel mailTemplateViewModel)
        {
            TransactionalInformation transaction;

            MailTemplate MailTemplate = new MailTemplate();
            MailTemplate.Id = mailTemplateViewModel.Id;

            MailTemplateBusinessService MailTemplateBusinessService = new MailTemplateBusinessService(_mailTemplateDataService);
            MailTemplateBusinessService.DeleteMailTemplate(MailTemplate, out transaction);
            if (transaction.ReturnStatus == false)
            {
                mailTemplateViewModel.ReturnStatus = false;
                mailTemplateViewModel.ReturnMessage = transaction.ReturnMessage;
                mailTemplateViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<MailTemplateViewModel>(HttpStatusCode.BadRequest, mailTemplateViewModel);
                return responseError;

            }

            mailTemplateViewModel.ReturnStatus = true;
            mailTemplateViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<MailTemplateViewModel>(HttpStatusCode.OK, mailTemplateViewModel);
            return response;

        }
    }
}