angular.module("app").register.controller('rightTypeController',
    ['$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.title = "Right Type";
            vm.messageBox = "";
            vm.alerts = [];
            vm.rightTypes = [];
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Name",
                sortDirection: "ASC",
                pageSize: 15
            };

            //functions
            vm.getAllRightTypes = getAllRightTypes;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;

            function init() {
                getAllRightTypes();
            }

            init();

            function getAllRightTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/rightTypeService/GetRightTypes").then(function (data) {
                    vm.rightTypes = data.rightTypes;
                    return vm.rightTypes;
                });
            }

            function closeAlert(index) {
                vm.alerts.splice(index, 1);
            };

            function clearValidationErrors() {
                vm.productNameInputError = false;
            }
        }

        }
    ]);
