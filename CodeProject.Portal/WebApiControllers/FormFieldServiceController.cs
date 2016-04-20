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
    [RoutePrefix("api/FormFieldService")]
    public class FormFieldFieldServiceController : ApiController
    {

        [Inject]
        public IFormFieldDataService _formFieldsDataService { get; set; }

        /// <summary>
        /// Create Workflow Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="FormFieldViewModel"></param>
        /// <returns></returns>
        [Route("CreateFormField")]
        [HttpPost]
        public HttpResponseMessage CreateFormField(HttpRequestMessage request, [FromBody] FormFieldViewModel formFieldsViewModel)
        {
            TransactionalInformation transaction;

            FormField formField = new FormField();
            formField.Name = formFieldsViewModel.Name;
            formField.FormId = formFieldsViewModel.FormId;
            formField.FieldType = formFieldsViewModel.FieldType;

            FormFieldBusinessService formFieldsBusinessService = new FormFieldBusinessService(_formFieldsDataService);
            formFieldsBusinessService.CreateFormField(formField, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formFieldsViewModel.ReturnStatus = false;
                formFieldsViewModel.ReturnMessage = transaction.ReturnMessage;
                formFieldsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.BadRequest, formFieldsViewModel);
                return responseError;

            }

            formFieldsViewModel.Id = formField.Id;
            formFieldsViewModel.ReturnStatus = true;
            formFieldsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.OK, formFieldsViewModel);
            return response;

        }

        [Route("UpdateFormField")]
        [HttpPost]
        public HttpResponseMessage UpdateFormField(HttpRequestMessage request, [FromBody] FormFieldViewModel formFieldsViewModel)
        {
            TransactionalInformation transaction;

            FormField formField = new FormField();
            formField.Id = formFieldsViewModel.Id;
            formField.Name = formFieldsViewModel.Name;
            formField.FormId = formFieldsViewModel.FormId;
            formField.FieldType = formFieldsViewModel.FieldType;

            FormFieldBusinessService formFieldsBusinessService = new FormFieldBusinessService(_formFieldsDataService);
            formFieldsBusinessService.UpdateFormField(formField, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formFieldsViewModel.ReturnStatus = false;
                formFieldsViewModel.ReturnMessage = transaction.ReturnMessage;
                formFieldsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.BadRequest, formFieldsViewModel);
                return responseError;

            }

            formFieldsViewModel.ReturnStatus = true;
            formFieldsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.OK, formFieldsViewModel);
            return response;
        }


        [Route("DeleteFormField")]
        [HttpPost]
        public HttpResponseMessage DeleteFormField(HttpRequestMessage request, [FromBody] FormFieldViewModel formFieldsViewModel)
        {
            TransactionalInformation transaction;

            FormField formField = new FormField();
            formField.Id = formFieldsViewModel.Id;
            formField.Name = formFieldsViewModel.Name;
            formField.FormId = formFieldsViewModel.FormId;
            formField.FieldType = formFieldsViewModel.FieldType;

            FormFieldBusinessService formFieldsBusinessService = new FormFieldBusinessService(_formFieldsDataService);
            formFieldsBusinessService.DeleteFormField(formField, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formFieldsViewModel.ReturnStatus = false;
                formFieldsViewModel.ReturnMessage = transaction.ReturnMessage;
                formFieldsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.BadRequest, formFieldsViewModel);
                return responseError;

            }

            formFieldsViewModel.ReturnStatus = true;
            formFieldsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.OK, formFieldsViewModel);
            return response;
        }

        /// <summary>
        /// Get FormFields
        /// </summary>
        /// <param name="request"></param>
        /// <param name="FormFieldsViewModel"></param>
        /// <returns></returns>
        [Route("GetFormFields")]
        [HttpPost]
        public HttpResponseMessage GetFormFields(HttpRequestMessage request, [FromBody] FormFieldViewModel formFieldsViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = formFieldsViewModel.CurrentPageNumber;
            int pageSize = formFieldsViewModel.PageSize;
            string sortExpression = formFieldsViewModel.SortExpression;
            string sortDirection = formFieldsViewModel.SortDirection;

            FormFieldBusinessService FormFieldsBusinessService = new FormFieldBusinessService(_formFieldsDataService);
            List<FormField> formFields = FormFieldsBusinessService.GetFormFields(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formFieldsViewModel.ReturnStatus = false;
                formFieldsViewModel.ReturnMessage = transaction.ReturnMessage;
                formFieldsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.BadRequest, formFieldsViewModel);
                return responseError;

            }

            formFieldsViewModel.TotalPages = transaction.TotalPages;
            formFieldsViewModel.TotalRows = transaction.TotalRows;
            formFieldsViewModel.FormFields = formFields;
            formFieldsViewModel.ReturnStatus = true;
            formFieldsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.OK, formFieldsViewModel);
            return response;

        }

        /// <summary>
        /// GetStatusTranslationsForActionType
        /// </summary>
        /// <param name="request"></param>
        /// <param name="StatusTranslationViewModel"></param>
        /// <returns></returns>
        [Route("GetFormFieldsForForm")]
        [HttpPost]
        public HttpResponseMessage GetFormFieldsForForm(HttpRequestMessage request, [FromBody] FormFieldViewModel formFieldViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = formFieldViewModel.CurrentPageNumber;
            int pageSize = formFieldViewModel.PageSize;
            string sortExpression = formFieldViewModel.SortExpression;
            string sortDirection = formFieldViewModel.SortDirection;
            long formId = formFieldViewModel.FormId;

            FormFieldBusinessService formFieldBusinessService = new FormFieldBusinessService(_formFieldsDataService);
            List<FormField> formFields = formFieldBusinessService.GetFormFieldsForForm(formId, currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formFieldViewModel.ReturnStatus = false;
                formFieldViewModel.ReturnMessage = transaction.ReturnMessage;
                formFieldViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.BadRequest, formFieldViewModel);
                return responseError;

            }

            formFieldViewModel.TotalPages = transaction.TotalPages;
            formFieldViewModel.TotalRows = transaction.TotalRows;
            formFieldViewModel.FormFields = formFields;
            formFieldViewModel.ReturnStatus = true;
            formFieldViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormFieldViewModel>(HttpStatusCode.OK, formFieldViewModel);
            return response;

        }
    }
}