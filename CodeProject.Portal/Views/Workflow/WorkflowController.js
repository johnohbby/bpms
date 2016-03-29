angular.module("app").register.controller('workflowController',
    ['$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.title = "Workflows";
            vm.messageBox = "";
            vm.alerts = [];
            vm.rightTypes = [];
            vm.rightType = { id: "0" };
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Name",
                sortDirection: "ASC",
                pageSize: 15
            };
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.mySelectedItems = [];

            //functions
            vm.getWorkflows = getWorkflows;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;

            function init() {
                getWorkflows();
            }

            init();

            function getWorkflows() {
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflows").then(function (data) {
                    vm.workflows = data.workflows;
                    console.log(data.workflows);
                    return vm.workflows;
                });
            }

            function closeAlert(index) {
                vm.alerts.splice(index, 1);
            };

            function clearValidationErrors() {
                vm.workflowNameInputError = false;
            }
        }

        }
    ]);
