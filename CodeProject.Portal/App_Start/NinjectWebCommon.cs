[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CodeProject.Portal.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CodeProject.Portal.App_Start.NinjectWebCommon), "Stop")]

namespace CodeProject.Portal.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<CodeProject.Interfaces.ICustomerDataService>().To<CodeProject.Data.EntityFramework.CustomerDataService>();
            kernel.Bind<CodeProject.Interfaces.IProductDataService>().To<CodeProject.Data.EntityFramework.ProductDataService>();
            kernel.Bind<CodeProject.Interfaces.IWorkflowTypeDataService>().To<CodeProject.Data.EntityFramework.WorkflowTypeDataService>();
            kernel.Bind<CodeProject.Interfaces.IActionTypeDataService>().To<CodeProject.Data.EntityFramework.ActionTypeDataService>();
            kernel.Bind<CodeProject.Interfaces.IActionDataService>().To<CodeProject.Data.EntityFramework.ActionDataService>();
            kernel.Bind<CodeProject.Interfaces.IGroupTypeDataService>().To<CodeProject.Data.EntityFramework.GroupTypeDataService>();
            kernel.Bind<CodeProject.Interfaces.IGroupDataService>().To<CodeProject.Data.EntityFramework.GroupDataService>();
            kernel.Bind<CodeProject.Interfaces.IRightTypeDataService>().To<CodeProject.Data.EntityFramework.RightTypeDataService>();
            kernel.Bind<CodeProject.Interfaces.IContentTypeDataService>().To<CodeProject.Data.EntityFramework.ContentTypeDataService>();
            kernel.Bind<CodeProject.Interfaces.IUserDataService>().To<CodeProject.Data.EntityFramework.UserDataService>();
            kernel.Bind<CodeProject.Interfaces.IUserGroupDataService>().To<CodeProject.Data.EntityFramework.UserGroupDataService>();
            kernel.Bind<CodeProject.Interfaces.IContentRightDataService>().To<CodeProject.Data.EntityFramework.ContentRightDataService>();
            kernel.Bind<CodeProject.Interfaces.IWorkflowDataService>().To<CodeProject.Data.EntityFramework.WorkflowDataService>();
            kernel.Bind<CodeProject.Interfaces.IStatusDataService>().To<CodeProject.Data.EntityFramework.StatusDataService>();
            kernel.Bind<CodeProject.Interfaces.IStatusTranslationDataService>().To<CodeProject.Data.EntityFramework.StatusTranslationDataService>();
            kernel.Bind<CodeProject.Interfaces.IFormDataService>().To<CodeProject.Data.EntityFramework.FormDataService>();
            kernel.Bind<CodeProject.Interfaces.IFormFieldDataService>().To<CodeProject.Data.EntityFramework.FormFieldFieldDataService>();
            kernel.Bind<CodeProject.Interfaces.IContentFormMapDataService>().To<CodeProject.Data.EntityFramework.ContentFormMapDataService>();
            kernel.Bind<CodeProject.Interfaces.IUploadDataService>().To<CodeProject.Data.EntityFramework.UploadDataService>();
            kernel.Bind<CodeProject.Interfaces.IFolderDataService>().To<CodeProject.Data.EntityFramework.FolderDataService>();
            kernel.Bind<CodeProject.Interfaces.IDocumentDataService>().To<CodeProject.Data.EntityFramework.DocumentDataService>();
            kernel.Bind<CodeProject.Interfaces.IEmailDataService>().To<CodeProject.Data.EntityFramework.EmailDataService>();
            kernel.Bind<CodeProject.Interfaces.IMailTemplateDataService>().To<CodeProject.Data.EntityFramework.MailTemplateDataService>();
            kernel.Bind<CodeProject.Interfaces.IMailTemplateMapDataService>().To<CodeProject.Data.EntityFramework.MailTemplateMapDataService>();
        }        
    }
}
