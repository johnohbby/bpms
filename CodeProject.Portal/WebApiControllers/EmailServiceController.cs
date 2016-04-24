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

        [Inject]
        public IWorkflowDataService _workflowDataService { get; set; }

        [Inject]
        public IActionDataService _actionDataService { get; set; }
        
        [Inject]
        public IMailTemplateDataService _mailTemplateDataService { get; set; }

        [Inject]
        public IUserDataService _userDataService { get; set; }

        [Inject]
        public IActionTypeDataService _actionTypeDataService { get; set; }

        [Inject]
        public IWorkflowTypeDataService _workflowTypeDataService { get; set; } 

        [Route("SendEmail")]
        [HttpPost]
        public HttpResponseMessage SendEmail(HttpRequestMessage request, [FromBody] EmailViewModel emailViewModel)
        {
            TransactionalInformation transaction;

            int actionId = (int) emailViewModel.ActionId;

            ActionBusinessService actionBusinessService = new ActionBusinessService(_actionDataService);
            CodeProject.Business.Entities.Action action = actionBusinessService.GetAction(actionId, out transaction);

            WorkflowBusinessService workflowBusinessService = new WorkflowBusinessService(_workflowDataService);
            Workflow workflow = workflowBusinessService.Getworkflow(action.WorkflowId, out transaction);

            MailTemplateBusinessService mailTemplateBusinessService = new MailTemplateBusinessService(_mailTemplateDataService);
            MailTemplate mailTemplate = mailTemplateBusinessService.GetMailTemplateForAction(actionId);

            UserBusinessService userBusinessService = new UserBusinessService(_userDataService);
            User userDelegated = userBusinessService.GetUser(action.DelegatedTo, out transaction);
            User userSent = userBusinessService.GetUser(action.CreatedBy, out transaction);

            ActionTypeBusinessService actionTypeBusinessService = new ActionTypeBusinessService(_actionTypeDataService);
            ActionType actionType = actionTypeBusinessService.GetActionType((int) action.ActionTypeId, out transaction);

            WorkflowTypeBusinessService workflowTypeBusinessService = new WorkflowTypeBusinessService(_workflowTypeDataService);
            WorkflowType workflowType = workflowTypeBusinessService.GetworkflowType((int) workflow.WorkflowTypeId, out transaction);

            mailTemplate.Body = mailTemplate.Body.Replace("$WORKFLOW_NAME", workflow.Name);
            mailTemplate.Body = mailTemplate.Body.Replace("$SENDER_NAME", userSent.Name + " " + userSent.Surname);
            mailTemplate.Body = mailTemplate.Body.Replace("$ACTIONTYPE_NAME", actionType.Name);
            mailTemplate.Body = mailTemplate.Body.Replace("$WORKFLOWTYPE_NAME", workflowType.Name);

            mailTemplate.Subject = mailTemplate.Subject.Replace("$WORKFLOW_NAME", workflow.Name);
            mailTemplate.Subject = mailTemplate.Subject.Replace("$SENDER_NAME", userSent.Name + " " + userSent.Surname);
            mailTemplate.Subject = mailTemplate.Subject.Replace("$ACTIONTYPE_NAME", actionType.Name);
            mailTemplate.Subject = mailTemplate.Subject.Replace("$WORKFLOWTYPE_NAME", workflowType.Name);

            Email email = new Email();
            email.From = emailViewModel.From;
            email.To = userDelegated.Email;
            email.Subject = mailTemplate.Subject;
            email.MailBody = mailTemplate.Body;


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