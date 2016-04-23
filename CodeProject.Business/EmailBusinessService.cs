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
    public class EmailBusinessService
    {
        private IEmailDataService _emailDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public EmailBusinessService(IEmailDataService emailDataService)
        {
            _emailDataService = emailDataService;
        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="transaction"></param>
        public void SendEmail(Email email, out TransactionalInformation transaction)
        {

            transaction = new TransactionalInformation();

            try
            {

                EmailBusinessRules emailBusinessRules = new EmailBusinessRules();
                ValidationResult results = new EmailBusinessRules().Validate(email);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }
                _emailDataService.SendEmail(email);

                transaction.ReturnMessage.Add("Email was successfully send.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }

        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData()
        {
            
        }




    }
}
