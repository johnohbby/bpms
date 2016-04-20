using System;
using System.Collections.Generic;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;
using FluentValidation.Results;

namespace CodeProject.Business
{
    public class FormBusinessService
    {
        private IFormDataService _formDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormBusinessService(IFormDataService formDataService)
        {
            _formDataService = formDataService;
        }

        /// <summary>
        /// Create Form
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Form CreateForm(Form form, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                FormBusinessRules formBusinessRules = new FormBusinessRules();
                ValidationResult results = formBusinessRules.Validate(form);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return form;
                }

                _formDataService.CreateSession();
                _formDataService.BeginTransaction();
                _formDataService.CreateForm(form);
                _formDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Form successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _formDataService.CloseSession();
            }

            return form;


        }

        /// <summary>
        /// Update Form
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="transaction"></param>
        public void UpdateForm(Form form, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                FormBusinessRules formBusinessRules = new FormBusinessRules();
                ValidationResult results = new FormBusinessRules().Validate(form);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _formDataService.CreateSession();
                _formDataService.BeginTransaction();

                Form existingForm = _formDataService.GetForm(form.Id);

                existingForm.Name = form.Name;
                existingForm.TableName = form.TableName;
                
                

                _formDataService.UpdateForm(form);
                _formDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Form was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _formDataService.CloseSession();
            }


        }
        public Form DeleteForm(Form form, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                FormBusinessRules formBusinessRules = new FormBusinessRules();
                

                _formDataService.CreateSession();
                _formDataService.BeginTransaction();
                _formDataService.DeleteForm(form);
                _formDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Form successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _formDataService.CloseSession();
            }

            return form;


        }
        /// <summary>
        /// Get Forms
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Form> GetForms(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<Form> form = new List<Form>();

            try
            {
                int totalRows;

                _formDataService.CreateSession();
                form = _formDataService.GetForms(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _formDataService.CloseSession();

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
                _formDataService.CloseSession();
            }

            return form;

        }

        
   
        /// <summary>
        /// Get Form
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Form Getform(long formId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Form form = new Form();

            try
            {

                _formDataService.CreateSession();
                form = _formDataService.GetForm(formId);
                _formDataService.CloseSession();      
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
                _formDataService.CloseSession();
            }

            return form;

        }


        public List<Form> GetFormByActionTypeId(long actionTypeId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<Form> form = new List<Form>();

            try
            {
                int totalRows;

                _formDataService.CreateSession();
                form = _formDataService.GetFormByActionTypeId(actionTypeId, out totalRows);
                _formDataService.CloseSession();

                //transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
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
                _formDataService.CloseSession();
            }

            return form;

        }


        public List<FormField> GetFormFieldsByFormId(long formId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<FormField> form = new List<FormField>();

            try
            {
                int totalRows;

                _formDataService.CreateSession();
                form = _formDataService.GetFormFieldsByFormId(formId, out totalRows);
                _formDataService.CloseSession();

                //transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
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
                _formDataService.CloseSession();
            }

            return form;

        }

        public void InsertData(long formId, string allValues, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
        

            try
            {
                int totalRows;

                _formDataService.CreateSession();
                 _formDataService.InsertData(formId, allValues, out totalRows);
                _formDataService.CloseSession();

                //transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
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
                _formDataService.CloseSession();
            }
        }

        public void CreateTable(long formId, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();


            try
            {
                int totalRows;

                _formDataService.CreateSession();
                _formDataService.CreateTable(formId, out totalRows);
                _formDataService.CloseSession();

                //transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
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
                _formDataService.CloseSession();
            }



        }
    }
}

