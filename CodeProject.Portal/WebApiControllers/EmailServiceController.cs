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
    [RoutePrefix("api/EmailService")]
    public class EmailServiceController : ApiController
    {
        public EmailServiceController()
   {
   }
        [Inject]
        public IEmailDataService _emailDataService { get; set; }        

        [Route("SendEmail")]
        [HttpPost]
        public HttpResponseMessage SendEmail(HttpRequestMessage request, [FromBody] EmailViewModel emailViewModel)
        {
            TransactionalInformation transaction;

            Email email = new Email();
            email.From = emailViewModel.From;
            email.To = emailViewModel.To;
            email.Subject = emailViewModel.Subject;
            email.MailBody = emailViewModel.MailBody;


            EmailBusinessService emailBusinessService = new EmailBusinessService(_emailDataService);
            emailBusinessService.SendEmail(email, out transaction);
            if (transaction.ReturnStatus == false)
            {
                emailViewModel.ReturnStatus = false;
                emailViewModel.ReturnMessage = transaction.ReturnMessage;
                emailViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<EmailViewModel>(HttpStatusCode.BadRequest, emailViewModel);
                return responseError;

            }

            emailViewModel.ReturnStatus = true;
            emailViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<EmailViewModel>(HttpStatusCode.OK, emailViewModel);
            return response;

        }
    }
}