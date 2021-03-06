﻿using System.Web;
using System.Web.Optimization;

namespace CodeProject.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/SortableGrid.css",
                      "~/Content/angular-block-ui.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/trNgGrid.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(                   
               "~/Scripts/angular-file-upload-shim.js",
               "~/Scripts/angular.min.js",
               "~/Scripts/angular-file-upload.js",
               "~/Scripts/angular-route.min.js",
               "~/Scripts/angular-sanitize.min.js",
               "~/Scripts/angular-ui.min.js",
               "~/Scripts/angular-ui/ui-bootstrap.min.js",
               "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
               "~/Scripts/angular-ui.min.js",       
               "~/Scripts/angular-block-ui.js",
               "~/Scripts/angular-cookies.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/trNgGrid").Include(
               "~/Scripts/trNgGrid.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/shared").Include(               
               "~/Views/Shared/App.js",
                "~/Views/Shared/AjaxService.js",
                "~/Views/Shared/AlertService.js",
                "~/Views/Shared/LoginService.js",
                "~/Views/Shared/DocumentService.js",
                "~/Views/Shared/DataGridService.js",
                "~/Views/Shared/MasterController.js",
                "~/Views/Shared/ModalDirective.js",
                "~/Views/Shared/UploadController.js",
                 "~/Views/Shared/EmailService.js"

           ));

            bundles.Add(new ScriptBundle("~/bundles/routing-debug").Include(
              "~/Views/Shared/CodeProjectRouting-debug.js"
          ));

            bundles.Add(new ScriptBundle("~/bundles/routing-production").Include(
             "~/Views/Shared/CodeProjectRouting-production.js"
         ));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                
                "~/Views/Home/IndexController.js",
                "~/Views/Home/InitializeDataController.js"
         ));


            bundles.Add(new ScriptBundle("~/bundles/customers").Include(
              "~/Views/Customers/CustomerMaintenanceController.js",
              "~/Views/Customers/CustomerInquiryController.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/products").Include(      
                "~/Views/Products/ProductMaintenanceController.js",
                "~/Views/Products/ProductInquiryController.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/workflow").Include(
                "~/Views/Workflow/WorkflowController.js",
                "~/Views/Workflow/WorkflowTypesController.js",
                "~/Views/Workflow/ActionTypeController.js",
                "~/Views/Workflow/StatusController.js"
            ));
           
            bundles.Add(new ScriptBundle("~/bundles/registries").Include(
               "~/Views/Registries/RightTypeController.js",
               "~/Views/Registries/ContentRightController.js",
               "~/Views/Registries/UserController.js",
               "~/Views/Registries/GroupController.js",
               "~/Views/Registries/UserGroupController.js"
           ));
            bundles.Add(new ScriptBundle("~/bundles/forms").Include(
               "~/Views/Forms/FormController.js",
                "~/Views/Forms/ContentFormMapController.js"
           ));
            bundles.Add(new ScriptBundle("~/bundles/documents").Include(
              "~/Views/Documents/DocumentController.js"
          ));
            bundles.Add(new ScriptBundle("~/bundles/mailTemplates").Include(
              "~/Views/MailTemplates/MailTemplateController.js"
          ));
        }
    }
}
