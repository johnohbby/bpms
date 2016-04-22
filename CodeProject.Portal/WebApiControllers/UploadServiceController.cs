using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeProject.Portal.Models;
using CodeProject.Business.Entities;
using CodeProject.Business;
using CodeProject.Interfaces;
using Ninject;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System;
using Newtonsoft.Json;

namespace CodeProject.Portal.WebApiControllers
{
    [RoutePrefix("api/UploadService")]
    public class UploadServiceController : ApiController
    {

        [Inject]
        public IUploadDataService _workflowTypeDataService { get; set; }

        [Inject]
        public IDocumentDataService _documentDataService { get; set; }
        /// <summary>
        /// Create Workflow Type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="WorkflowTypeViewModel"></param>
        /// <returns></returns>
        /// 
        [Route("Upload")]
        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var originalFileName = GetDeserializedFileName(result.FileData[0]);
            string contentTypeName = result.FormData[1];
            var uploadedFileInfo = new System.IO.FileInfo(result.FileData[0].LocalFileName);
            var returnData = "ReturnTest";

            //CREATE DOCUMENT
            TransactionalInformation transaction;

            Document document = new Document();
            document.Name = originalFileName;
            document.Extension = ".pdf";
            document.ContentId = null;
            document.Created = DateTime.Now;
            document.ParentDocumentId = -1;
            document.NameOnServer = result.FileData[0].LocalFileName;

            document.ContentTypeName = contentTypeName;

            DocumentBusinessService documentBusinessService = new DocumentBusinessService(_documentDataService);
            long documentId = documentBusinessService.CreateDocument(document, out transaction);
            document.Id = documentId;
            return this.Request.CreateResponse(HttpStatusCode.OK, new { document });
        }


        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0)[0] ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}