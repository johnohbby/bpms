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
    public class MailTemplateMapBusinessService
    {
        private IMailTemplateMapDataService _mailTemplateMapDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public MailTemplateMapBusinessService(IMailTemplateMapDataService mailTemplateMapDataService)
        {
            _mailTemplateMapDataService = mailTemplateMapDataService;
        }

        /// <summary>
        /// Create Action Type
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public MailTemplateMap CreateMailTemplateMap(MailTemplateMap mailTemplateMap, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                MailTemplateMapBusinessRules MailTemplateMapBusinessRules = new MailTemplateMapBusinessRules();
                ValidationResult results = MailTemplateMapBusinessRules.Validate(mailTemplateMap);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return mailTemplateMap;
                }

                _mailTemplateMapDataService.CreateSession();
                _mailTemplateMapDataService.BeginTransaction();
                _mailTemplateMapDataService.CreateMailTemplateMap(mailTemplateMap);
                _mailTemplateMapDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Mail Template Map successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateMapDataService.CloseSession();
            }

            return mailTemplateMap;

        }

      

        /// <summary>
        /// Get Maps For Mail Template
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<MailTemplateMap> GetMapsForMailTemplate(long mailTemplateId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<MailTemplateMap> mailTemplateMaps = new List<MailTemplateMap>();

            try
            {
                int totalRows;

                _mailTemplateMapDataService.CreateSession();
                mailTemplateMaps = _mailTemplateMapDataService.GetMapsForMailTemplate(mailTemplateId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _mailTemplateMapDataService.CloseSession();

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
                _mailTemplateMapDataService.CloseSession();
            }

            return mailTemplateMaps;

        }

        
        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="workflowType"></param>
        /// <param name="transaction"></param>
        public void DeleteMailTemplateMap(MailTemplateMap mailTemplateMap, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                MailTemplateMapBusinessRules mailTemplateMapBusinessRules = new MailTemplateMapBusinessRules();
                ValidationResult results = mailTemplateMapBusinessRules.Validate(mailTemplateMap);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _mailTemplateMapDataService.CreateSession();
                _mailTemplateMapDataService.BeginTransaction();

                _mailTemplateMapDataService.DeleteMailTemplateMap(mailTemplateMap);
                _mailTemplateMapDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Mail Template Map was successfully deleted.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _mailTemplateMapDataService.CloseSession();
            }


        }


        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            MailTemplateMap mailTemplateMap = new MailTemplateMap();

            try
            {

                _mailTemplateMapDataService.CreateSession();
                _mailTemplateMapDataService.BeginTransaction();
                _mailTemplateMapDataService.CommitTransaction(true);
                _mailTemplateMapDataService.CloseSession();

                _mailTemplateMapDataService.CreateSession();
                _mailTemplateMapDataService.BeginTransaction();
                _mailTemplateMapDataService.CommitTransaction(true);
                _mailTemplateMapDataService.CloseSession();

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
                _mailTemplateMapDataService.CloseSession();
            }           

        }




    }
}
