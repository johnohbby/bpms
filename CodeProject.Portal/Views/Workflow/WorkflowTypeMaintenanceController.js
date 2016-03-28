
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
            vm.myItems = [];
            vm.currentPageNumber = 1;
            vm.sortExpression = "Name";
            vm.sortDirection = "ASC";
            vm.pageSize = 15;

            this.prepareSearch = function () {

                var inquiry = new Object();

                inquiry.currentPageNumber = vm.currentPageNumber;
                inquiry.sortExpression = vm.sortExpression;
                inquiry.sortDirection = vm.sortDirection;
                inquiry.pageSize = vm.pageSize;

                return inquiry;

            }
        

            var workflowTypeID = ($routeParams.id || "");
            var inquiry = vm.prepareSearch();
            ajaxService.ajaxPost(inquiry, "api/workflowTypeService/GetWorkflowTypes", this.getProductsOnSuccess, this.getProductsOnError);

            if (workflowTypeID == "") {
                vm.Id = "0";
                vm.Name = "";
                vm.Description = "";                
            }
            else {
                vm.Id = workflowTypeID;
                var workflowType = new Object();
                workflowTypeID.Id = workflowTypeID
                ajaxService.ajaxPost(product, "api/workflowTypeService/GetworkflowType", this.getProductsOnSuccess, this.getProductsOnError);
            }

        }

        this.closeAlert = function (index) {
            vm.alerts.splice(index, 1);
        };

        this.getProductOnSuccess = function (response) {

            vm.productName = response.productName;
            vm.quantityPerUnit = response.quantityPerUnit;
            vm.unitPrice = response.unitPrice;

            vm.myItems = response.WorkflowTypes;
          
        }

        this.getProductOnError = function (response) {

        }


        this.saveWorkflowType = function () {

            var workflowType = new Object();

            workflowType.Id = vm.Id
            workflowType.Name = vm.Name;
            workflowType.Description = vm.Description;
      

            if (workflowType.Id == "0") {
                ajaxService.ajaxPost(workflowType, "api/workflowTypeService/CreateWorkflowType");
            }
            else {
                ajaxService.ajaxPost(workflowType, "api/productService/UpdateProduct");
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

        this.getProductsOnSuccess = function (response) {
            vm.myItems = response.workflowTypes;
        }

        this.getProductsOnError = function (response) {
            
        }

        this.clearValidationErrors = function () {
            vm.productNameInputError = false;
        }

    }]);
