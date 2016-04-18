angular.module("app").register.controller('statusController',
    ['$scope','$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($scope,$routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.messageBox = "";
            vm.alerts = [];
            vm.statuses = [];
            vm.status = {id:"0"};
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 15
            };
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.showModalDelete = false;
            vm.mySelectedItems = [];
            vm.status_ = {};

            //functions
            vm.getAllStatuses = getAllStatuses;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createStatus = createStatus;
            vm.updateStatus = updateStatus;
            vm.deleteStatus = deleteStatus;
            vm.toggleModal = toggleModal;
         

            function init() {
                getAllStatuses();
               
            }

            init();

            vm.titles = [];

            function toggleModal()
            {
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
            }

            function toggleModalCreate() {
                $scope.title = "Create Status";
                vm.status = getStatus();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Edit Status";
                    vm.status_ = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select Status!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Delete Status";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Status!");
                }
            };

            function getAllStatuses() {
                return ajaxService.ajaxPost(vm.pagination, "api/statusService/GetStatuses").then(function (data) {
                    vm.statuses = data.statuses;
                    return vm.statuses;
                });
            }

            function createStatus() {
                var status = new Object();
                status.Id = vm.status.id;
                status.Name = vm.status.name;
                return ajaxService.ajaxPost(status, "api/statusService/CreateStatus").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    getAllStatuses();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateStatus() {
                var status = new Object();
                status.Id = vm.status_.id;
                status.Name = vm.status_.name;
                return ajaxService.ajaxPost(status, "api/statusService/UpdateStatus").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    getAllStatuses();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteStatus() {
                var status = new Object();
                status.Id = vm.mySelectedItems[0].id;
                status.Name = vm.mySelectedItems[0].name;
                return ajaxService.ajaxPost(status, "api/statusService/DeleteStatus").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    getAllStatuses();
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

            function getStatus()
            {
                vm.status = { id: "0" };
                return vm.status;
            }

        }

        }
    ]);
