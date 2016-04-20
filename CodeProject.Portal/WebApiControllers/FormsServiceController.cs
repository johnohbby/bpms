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
    [RoutePrefix("api/FormService")]
    public class FormServiceController : ApiController
    {

        [Inject]
        public IFormDataService _formsDataService { get; set; }

        /// <summary>
        /// Create Workflow Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="FormViewModel"></param>
        /// <returns></returns>
        [Route("CreateForm")]
        [HttpPost]
        public HttpResponseMessage CreateForm(HttpRequestMessage request, [FromBody] FormViewModel formsViewModel)
        {
            TransactionalInformation transaction;

            Form forms = new Form();
            forms.Name = formsViewModel.Name;
            forms.TableName = formsViewModel.TableName;

            FormBusinessService formsBusinessService = new FormBusinessService(_formsDataService);
            formsBusinessService.CreateForm(forms, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formsViewModel.ReturnStatus = false;
                formsViewModel.ReturnMessage = transaction.ReturnMessage;
                formsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormViewModel>(HttpStatusCode.BadRequest, formsViewModel);
                return responseError;

            }

            formsViewModel.Id = forms.Id;
            formsViewModel.ReturnStatus = true;
            formsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormViewModel>(HttpStatusCode.OK, formsViewModel);
            return response;

        }

        [Route("UpdateForm")]
        [HttpPost]
        public HttpResponseMessage UpdateForm(HttpRequestMessage request, [FromBody] FormViewModel formsViewModel)
        {
            TransactionalInformation transaction;

            Form forms = new Form();
            forms.Id = formsViewModel.Id;
            forms.Name = formsViewModel.Name;
            forms.TableName = formsViewModel.TableName;

            FormBusinessService formsBusinessService = new FormBusinessService(_formsDataService);
            formsBusinessService.UpdateForm(forms, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formsViewModel.ReturnStatus = false;
                formsViewModel.ReturnMessage = transaction.ReturnMessage;
                formsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormViewModel>(HttpStatusCode.BadRequest, formsViewModel);
                return responseError;

            }

            formsViewModel.ReturnStatus = true;
            formsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormViewModel>(HttpStatusCode.OK, formsViewModel);
            return response;

        }

        /// <summary>
        /// Get Forms
        /// </summary>
        /// <param name="request"></param>
        /// <param name="formsViewModel"></param>
        /// <returns></returns>
        [Route("GetForms")]
        [HttpPost]
        public HttpResponseMessage GetForms(HttpRequestMessage request, [FromBody] FormViewModel formsViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = formsViewModel.CurrentPageNumber;
            int pageSize = formsViewModel.PageSize;
            string sortExpression = formsViewModel.SortExpression;
            string sortDirection = formsViewModel.SortDirection;

            FormBusinessService formsBusinessService = new FormBusinessService(_formsDataService);
            List<Form> forms = formsBusinessService.GetForms(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formsViewModel.ReturnStatus = false;
                formsViewModel.ReturnMessage = transaction.ReturnMessage;
                formsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormViewModel>(HttpStatusCode.BadRequest, formsViewModel);
                return responseError;

            }

            formsViewModel.TotalPages = transaction.TotalPages;
            formsViewModel.TotalRows = transaction.TotalRows;
            formsViewModel.Forms = forms;
            formsViewModel.ReturnStatus = true;
            formsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormViewModel>(HttpStatusCode.OK, formsViewModel);
            return response;

        }

        [Route("GetFormData")]
        [HttpPost]
        public HttpResponseMessage GetFormsData(HttpRequestMessage request, [FromBody] FormViewModel formsViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = formsViewModel.CurrentPageNumber;
            int pageSize = formsViewModel.PageSize;
            string sortExpression = formsViewModel.SortExpression;
            string sortDirection = formsViewModel.SortDirection;

            FormBusinessService formsBusinessService = new FormBusinessService(_formsDataService);
            List<Form> f = formsBusinessService.GetFormByActionTypeId(formsViewModel.ActionTypeId, out transaction);
            List<FormField> ff = new List<FormField>();
            if(f.Count > 0)
                ff = formsBusinessService.GetFormFieldsByFormId(f[0].Id, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formsViewModel.ReturnStatus = false;
                formsViewModel.ReturnMessage = transaction.ReturnMessage;
                formsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormViewModel>(HttpStatusCode.BadRequest, formsViewModel);
                return responseError;

            }

            formsViewModel.TotalPages = transaction.TotalPages;
            formsViewModel.TotalRows = transaction.TotalRows;
            formsViewModel.FormFields = ff;
            formsViewModel.ReturnStatus = true;
            formsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormViewModel>(HttpStatusCode.OK, formsViewModel);
            return response;

        }


        [Route("SaveForms")]
        [HttpPost]
        public HttpResponseMessage SaveForms(HttpRequestMessage request, [FromBody] FormViewModel formsViewModel)
        {

            TransactionalInformation transaction;

            FormBusinessService formsBusinessService = new FormBusinessService(_formsDataService);
            List<FormField> ff = formsBusinessService.GetFormFieldsByFormId(formsViewModel.Id,   out transaction);
            string allFields = "";
            foreach (var item in ff)
            {
                string name = item.Name;
                name = name.Replace(" ", "");
                string value = (string)(((Newtonsoft.Json.Linq.JObject)formsViewModel.Forms)).GetValue(name);
                allFields += name + "=" + value + ";";
            }


           formsBusinessService.InsertData(formsViewModel.Id, formsViewModel.ContentTypeName, formsViewModel.ContentId, allFields, out transaction);
            
            if (transaction.ReturnStatus == false)
            {
                formsViewModel.ReturnStatus = false;
                formsViewModel.ReturnMessage = transaction.ReturnMessage;
                formsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormViewModel>(HttpStatusCode.BadRequest, formsViewModel);
                return responseError;

            }

            formsViewModel.TotalPages = transaction.TotalPages;
            formsViewModel.TotalRows = transaction.TotalRows;
            formsViewModel.FormFields = ff;
            formsViewModel.ReturnStatus = true;
            formsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormViewModel>(HttpStatusCode.OK, formsViewModel);
            return response;

        }


        [Route("DeleteForm")]
        [HttpPost]
        public HttpResponseMessage DeleteForm(HttpRequestMessage request, [FromBody] FormViewModel formsViewModel)
        {
            TransactionalInformation transaction;

            Form forms = new Form();
            forms.Id = formsViewModel.Id;
            forms.Name = formsViewModel.Name;
            forms.TableName = formsViewModel.TableName;


            FormBusinessService formsBusinessService = new FormBusinessService(_formsDataService);
            formsBusinessService.DeleteForm(forms, out transaction);
            if (transaction.ReturnStatus == false)
            {
                formsViewModel.ReturnStatus = false;
                formsViewModel.ReturnMessage = transaction.ReturnMessage;
                formsViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormViewModel>(HttpStatusCode.BadRequest, formsViewModel);
                return responseError;

            }

            formsViewModel.ReturnStatus = true;
            formsViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormViewModel>(HttpStatusCode.OK, formsViewModel);
            return response;

        }

        [Route("CreateTable")]
        [HttpPost]
        public HttpResponseMessage CreateTable(HttpRequestMessage request, [FromBody] FormViewModel formViewModel)
        {

            TransactionalInformation transaction;

            FormBusinessService formsBusinessService = new FormBusinessService(_formsDataService);

            formsBusinessService.CreateTable(formViewModel.Id, out transaction);

            if (transaction.ReturnStatus == false)
            {
                formViewModel.ReturnStatus = false;
                formViewModel.ReturnMessage = transaction.ReturnMessage;
                formViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<FormViewModel>(HttpStatusCode.BadRequest, formViewModel);
                return responseError;

            }

            formViewModel.TotalPages = transaction.TotalPages;
            formViewModel.TotalRows = transaction.TotalRows;
            formViewModel.ReturnStatus = true;
            formViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<FormViewModel>(HttpStatusCode.OK, formViewModel);
            return response;

        }

    }
}