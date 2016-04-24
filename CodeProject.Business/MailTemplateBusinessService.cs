using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;

using FluentValidation;
using FluentValidation.Results;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net.Mail;

namespace CodeProject.Business
{
    public class MailTemplateBusinessService
    {
        private IMailTemplateDataService _mailTemplateDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public MailTemplateBusinessService(IMailTemplateDataService mailTemplateDataService)
        {
            _mailTemplateDataService = mailTemplateDataService;
        }

        /// <summary>
        /// Create Action Type
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public MailTemplate CreateMailTemplate(MailTemplate mailTemplate, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                MailTemplateBusinessRules mailTemplateBusinessRules = new MailTemplateBusinessRules();
                ValidationResult results = mailTemplateBusinessRules.Validate(mailTemplate);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return mailTemplate;
                }

                _mailTemplateDataService.CreateSession();
                _mailTemplateDataService.BeginTransaction();
                _mailTemplateDataService.CreateMailTemplate(mailTemplate);
                _mailTemplateDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Mail Template successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateDataService.CloseSession();
            }

            return mailTemplate;


        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        public void UpdateMailTemplate(MailTemplate mailTemplate, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                MailTemplateBusinessRules mailTemplateBusinessRules = new MailTemplateBusinessRules();
                ValidationResult results = mailTemplateBusinessRules.Validate(mailTemplate);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _mailTemplateDataService.CreateSession();
                _mailTemplateDataService.BeginTransaction();

                MailTemplate existingMailTemplate = _mailTemplateDataService.GetMailTemplate(mailTemplate.Id);

                existingMailTemplate.Id = mailTemplate.Id;
                existingMailTemplate.Body = mailTemplate.Body;
                existingMailTemplate.Subject = mailTemplate.Subject;

                _mailTemplateDataService.UpdateMailTemplate(mailTemplate);
                _mailTemplateDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Mail Template was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateDataService.CloseSession();
            }


        }

        /// <summary>
        /// Get Mail Templates
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<MailTemplate> GetMailTemplates(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<MailTemplate> mailTemplates = new List<MailTemplate>();

            try
            {
                int totalRows;

                _mailTemplateDataService.CreateSession();
                mailTemplates = _mailTemplateDataService.GetMailTemplates(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _mailTemplateDataService.CloseSession();

                transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
                transaction.TotalRows = totalRows;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateDataService.CloseSession();
            }

            return mailTemplates;

        }

        /// <summary>
        /// Get Mail Template
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public MailTemplate GetMailTemplate(int id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            MailTemplate mailTemplate = new MailTemplate();

            try
            {

                _mailTemplateDataService.CreateSession();
                mailTemplate = _mailTemplateDataService.GetMailTemplate(id);
                _mailTemplateDataService.CloseSession();      
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateDataService.CloseSession();
            }

            return mailTemplate;

        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteMailTemplate(MailTemplate mailTemplate, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                MailTemplateBusinessRules mailTemplateBusinessRules = new MailTemplateBusinessRules();
                ValidationResult results = mailTemplateBusinessRules.Validate(mailTemplate);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _mailTemplateDataService.CreateSession();
                _mailTemplateDataService.BeginTransaction();

                _mailTemplateDataService.DeleteMailTemplate(mailTemplate);
                _mailTemplateDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Mail Template was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateDataService.CloseSession();
            }


        }

        public MailTemplate GetMailTemplateForAction(long actionId)
        {
           TransactionalInformation transaction = new TransactionalInformation();

            MailTemplate mailTemplate = new MailTemplate();

            try
            {

                _mailTemplateDataService.CreateSession();
                mailTemplate = _mailTemplateDataService.GetMailTemplateForAction(actionId);
                _mailTemplateDataService.CloseSession();
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateDataService.CloseSession();
            }

            return mailTemplate;
        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            MailTemplate mailTemplate = new MailTemplate();

            try
            {

                _mailTemplateDataService.CreateSession();
                _mailTemplateDataService.BeginTransaction();
                _mailTemplateDataService.CommitTransaction(true);
                _mailTemplateDataService.CloseSession();

                _mailTemplateDataService.CreateSession();
                _mailTemplateDataService.BeginTransaction();
                _mailTemplateDataService.CommitTransaction(true);
                _mailTemplateDataService.CloseSession();

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateDataService.CloseSession();
            }           

        }




    }
}
