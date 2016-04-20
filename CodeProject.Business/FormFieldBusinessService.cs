using System;
using System.Collections.Generic;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;
using FluentValidation.Results;

namespace CodeProject.Business
{
    public class FormFieldBusinessService
    {
        private IFormFieldDataService _formFieldDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormFieldBusinessService(IFormFieldDataService formFieldDataService)
        {
            _formFieldDataService = formFieldDataService;
        }

        /// <summary>
        /// Create FormField
        /// </summary>
        /// <param name="FormFieldType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public FormField CreateFormField(FormField formField, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                FormFieldBusinessRules formFieldBusinessRules = new FormFieldBusinessRules();
                ValidationResult results = formFieldBusinessRules.Validate(formField);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return formField;
                }

                _formFieldDataService.CreateSession();
                _formFieldDataService.BeginTransaction();
                _formFieldDataService.CreateFormField(formField);
                _formFieldDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Form Field successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _formFieldDataService.CloseSession();
            }

            return formField;


        }

        /// <summary>
        /// Update FormField
        /// </summary>
        /// <param name="FormFieldType"></param>
        /// <param name="transaction"></param>
        public void UpdateFormField(FormField formField, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                FormFieldBusinessRules formFieldBusinessRules = new FormFieldBusinessRules();
                ValidationResult results = new FormFieldBusinessRules().Validate(formField);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _formFieldDataService.CreateSession();
                _formFieldDataService.BeginTransaction();

                FormField existingFormField = _formFieldDataService.GetFormField(formField.Id);

                existingFormField.Name = formField.Name;
                existingFormField.FormId = formField.FormId;
                existingFormField.FieldType = formField.FieldType;
                
                _formFieldDataService.UpdateFormField(formField);
                _formFieldDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Form Field was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _formFieldDataService.CloseSession();
            }


        }
        public FormField DeleteFormField(FormField formField, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                FormFieldBusinessRules formFieldBusinessRules = new FormFieldBusinessRules();
                

                _formFieldDataService.CreateSession();
                _formFieldDataService.BeginTransaction();
                _formFieldDataService.DeleteFormField(formField);
                _formFieldDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Form Field successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _formFieldDataService.CloseSession();
            }

            return formField;


        }
        /// <summary>
        /// Get FormFields
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<FormField> GetFormFields(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<FormField> formFields = new List<FormField>();

            try
            {
                int totalRows;

                _formFieldDataService.CreateSession();
                formFields = _formFieldDataService.GetFormFields(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _formFieldDataService.CloseSession();

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
                _formFieldDataService.CloseSession();
            }

            return formFields;

        }

        
   
        /// <summary>
        /// Get Form Field
        /// </summary>
        /// <param name="FormFieldTypeId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public FormField GetFormField(long formFieldId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            FormField formField = new FormField();

            try
            {

                _formFieldDataService.CreateSession();
                formField = _formFieldDataService.GetFormField(formFieldId);
                _formFieldDataService.CloseSession();      
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
                _formFieldDataService.CloseSession();
            }

            return formField;

        }

        /// <summary>
        /// GetStatusTranslationsForActionType
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<FormField> GetFormFieldsForForm(long formId, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<FormField> formFields = new List<FormField>();

            try
            {
                int totalRows;

                _formFieldDataService.CreateSession();
                formFields = _formFieldDataService.GetFormFieldsForForm(formId, currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _formFieldDataService.CloseSession();

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
                _formFieldDataService.CloseSession();
            }

            return formFields;

        }



    }
}

