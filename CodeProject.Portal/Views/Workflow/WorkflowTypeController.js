angular.module("app").register.controller('workflowTypeController',
    ['$scope', '$routeParams', '$location', 'ajaxService', 'alertService',
    function ($scope, $routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.messageBox = "";
            vm.alerts = [];
            vm.workflowTypes = [];
            vm.workflowType = { id: "0" };
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 1000
            };
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.showModalDelete = false;
            vm.mySelectedItems = [];
            vm.workflowtype = {};

            //functions
            vm.getAllWorkflowTypes = getAllWorkflowTypes;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createWorkflowType = createWorkflowType;
            vm.updateWorkflowType = updateWorkflowType;
            vm.deleteWorkflowType = deleteWorkflowType;
            vm.toggleModal = toggleModal;


            function init() {
                getAllWorkflowTypes();
            }

            init();

            vm.titles = [];

            function toggleModal() {
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
            }

            function toggleModalCreate() {
                $scope.title = "Create Workflow Type";
                vm.workflowType = getWorkflowType();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Edit Workflow Type";
                    vm.workflowtype = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select Workflow Type!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Delete Workflow Type";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Workflow Type!");
                }
            };

            function getAllWorkflowTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/workflowTypeService/GetWorkflowTypes").then(function (data) {
                    vm.workflowTypes = data.workflowTypes;
                    return vm.workflowTypes;
                });
            }

            function createWorkflowType() {
                var workflowType = new Object();
                workflowType.Id = vm.workflowType.id;
                workflowType.Name = vm.workflowType.name;
                workflowType.Description = vm.workflowType.description;
                return ajaxService.ajaxPost(workflowType, "api/workflowTypeService/CreateWorkflowType").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    vm.getAllWorkflowTypes();
     
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateWorkflowType() {
                var workflowType = new Object();
                workflowType.Id = vm.workflowtype.id;
                workflowType.Name = vm.workflowtype.name;
                workflowType.Description = vm.workflowtype.description;
                return ajaxService.ajaxPost(workflowType, "api/workflowTypeService/UpdateWorkflowType").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    vm.getAllWorkflowTypes();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteWorkflowType() {
                var workflowType = new Object();
                workflowType.Id = vm.mySelectedItems[0].id;
                workflowType.Name = vm.mySelectedItems[0].name;
                workflowType.Description = vm.mySelectedItems[0].description;

                return ajaxService.ajaxPost(workflowType, "api/workflowTypeService/DeleteWorkflowType").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    vm.getAllWorkflowTypes();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function closeAlert(index) {
                vm.alerts.splice(index, 1);
            };

            function clearValidationErrors() {
                vm.productNameInputError = false;
            }

            function getWorkflowType() {
                vm.workflowType = { id: "0" };
                return vm.workflowType;
            }

            function toggleModal()
            {
                vm.showModal != vm.showModal;
            }
        }

    }
    ]);
