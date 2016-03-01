
console.log("workflow type maintenance");

angular.module("app").register.controller('workflowTypeMaintenanceController',
    ['$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            vm.title = "Workflow type Maintenance";

            vm.messageBox = "";
            vm.alerts = [];

            var workflowTypeID = ($routeParams.id || "");

            if (workflowTypeID == "") {
                vm.Id = "0";
                vm.Name = "";
                vm.Description = "";                
            }
            else {
                vm.Id = workflowTypeID;
                var workflowType = new Object();
                workflowTypeID.Id = workflowTypeID
                ajaxService.ajaxPost(product, "api/workflowTypeService/GetworkflowType", this.getProductOnSuccess, this.getProductOnError);
            }

        }

        this.closeAlert = function (index) {
            vm.alerts.splice(index, 1);
        };

        this.getProductOnSuccess = function (response) {

            vm.productName = response.productName;
            vm.quantityPerUnit = response.quantityPerUnit;
            vm.unitPrice = response.unitPrice;
          
        }

        this.getProductOnError = function (response) {

        }


        this.saveWorkflowType = function () {

            var workflowType = new Object();

            workflowType.Id = vm.Id
            workflowType.ProductName = vm.Name;
            workflowType.Description = vm.Description;
      

            if (workflowType.Id == "1") {
                ajaxService.ajaxPost(workflowType, "api/workflowTypeService/CreateWorkflowType", this.createProductOnSuccess, this.createProductOnError);
            }
            else {
                ajaxService.ajaxPost(workflowType, "api/workflowTypeService/GetWorkflowTypes", this.createProductOnSuccess, this.createProductOnError);
                //ajaxService.ajaxPost(workflowType, "api/productService/UpdateProduct", this.updateProductOnSuccess, this.updateProductOnError);
            }

        }

        this.createProductOnSuccess = function (response) {
            vm.clearValidationErrors();
            alertService.renderSuccessMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            vm.productID = response.productID;
        }

        this.createProductOnError = function (response) {
            vm.clearValidationErrors();
            alertService.renderErrorMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            alertService.setValidationErrors(vm, response.validationErrors);
        }

        this.updateProductOnSuccess = function (response) {
            vm.clearValidationErrors();
            alertService.renderSuccessMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
        }

        this.updateProductOnError = function (response) {
            vm.clearValidationErrors();
            alertService.renderErrorMessage(response.returnMessage);
            vm.messageBox = alertService.returnFormattedMessage();
            vm.alerts = alertService.returnAlerts();
            alertService.setValidationErrors(vm, response.validationErrors);
        }

        this.clearValidationErrors = function () {
            vm.productNameInputError = false;
        }

    }]);
