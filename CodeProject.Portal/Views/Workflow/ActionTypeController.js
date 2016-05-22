angular.module("app").register.controller('actionTypeController',
    ['$scope','$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($scope,$routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.messageBox = "";
            vm.alerts = [];
            vm.actionTypes = [];
            vm.actionType = {id:"0"};
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 1000
            };
            vm.paginationSt = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 1000
            };
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.showModalDelete = false;
            vm.showModalCreateSt = false;
            vm.showModalUpdateSt = false;
            vm.showModalDeleteSt = false;
            vm.mySelectedItems = [];
            vm.actiontype = {};
            vm.workflowTypes = [];
            vm.statusTranslations = [];
            vm.statusTranslation = { id: "0" };
            vm.statustranslation = {};
            vm.mySelectedItemsSt = [];
            vm.statuses = [];
            vm.actionTypeName = {};


            //functions
            vm.getAllActionTypes = getAllActionTypes;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createActionType = createActionType;
            vm.updateActionType = updateActionType;
            vm.deleteActionType = deleteActionType;
            vm.toggleModal = toggleModal;
            vm.getWorkflowTypes = getWorkflowTypes;
            vm.toggleModalCreateSt = toggleModalCreateSt;
            vm.toggleModalUpdateSt = toggleModalUpdateSt;
            vm.toggleModalDeleteSt = toggleModalDeleteSt;
            vm.getAllStatuses = getAllStatuses;
            vm.createStatusTranslation = createStatusTranslation;
            vm.updateStatusTranslation = updateStatusTranslation;
            vm.deleteStatusTranslation = deleteStatusTranslation;
            vm.getStatusTranslationsForActionType = getStatusTranslationsForActionType;
         

            function init() {
                getWorkflowTypes();
                getAllActionTypes();
                getAllStatuses();
               
            }

            init();

            vm.titles = [];

            $scope.$watch('vm.mySelectedItems[0]', function (value) {
                if(vm.mySelectedItems[0])
                {
                    getStatusTranslationsForActionType(vm.mySelectedItems[0].id);
                    vm.actionTypeName = vm.mySelectedItems[0].name;
                }
                else {
                    vm.actionTypeName = "";
                }
            });

            function toggleModal()
            {
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
                vm.showModalCreateSt = false;
                vm.showModalUpdateSt = false;
                vm.showModalDeleteSt = false;
            }

            function toggleModalCreate() {
                $scope.title = "Create Action Type";
                vm.actionType = getActionType();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems) {
                    $scope.title = "Edit Action Type";
                    vm.actiontype = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select Action Type!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems) {
                    $scope.title = "Delete Action Type";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Action Type!");
                }
            };

            function getAllActionTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/actionTypeService/GetActionTypes").then(function (data) {
                    for (var i = 0; i < data.actionTypes.length; i++)
                    {
                        for(var j=0; j< vm.workflowTypes.length; j++)
                        {
                            if(data.actionTypes[i].workflowTypeId===vm.workflowTypes[j].id)
                            {
                                data.actionTypes[i].workflowTypeName = vm.workflowTypes[j].name;
                            }
                        }
                    }
                    vm.actionTypes = data.actionTypes;
                    return vm.actionTypes;
                });
            }

            function createActionType() {
                var actionType = new Object();
                actionType.Id = vm.actionType.id;
                actionType.Name = vm.actionType.name;
                actionType.WorkflowTypeId = vm.actionType.workflowTypeId;
                return ajaxService.ajaxPost(actionType, "api/actionTypeService/CreateActionType").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    getAllActionTypes();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateActionType() {
                var actionType = new Object();
                actionType.Id = vm.actiontype.id;
                actionType.Name = vm.actiontype.name;
                actionType.WorkflowTypeId = vm.actiontype.workflowTypeId;
                return ajaxService.ajaxPost(actionType, "api/actionTypeService/UpdateActionType").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    getAllActionTypes();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            function deleteActionType() {
                var actionType = new Object();
                actionType.Id = vm.mySelectedItems[0].id;
                actionType.Name = vm.mySelectedItems[0].name;
                actionType.Description = vm.mySelectedItems[0].description;
                return ajaxService.ajaxPost(actionType, "api/actionTypeService/DeleteActionType").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    getAllActionTypes();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            //STATUS TRANSLATIONS
            function toggleModalCreateSt() {
                $scope.title = "Create Status Translation";
                vm.statusTranslation = getStatusTranslation();
                vm.showModalCreateSt = !vm.showModalCreateSt;
            };

            function toggleModalUpdateSt() {
                if (vm.mySelectedItemsSt[0]) {
                    $scope.title = "Edit Status Translation";
                    vm.statustranslation = vm.mySelectedItemsSt[0];
                    vm.showModalUpdateSt = !vm.showModalUpdateSt;
                }
                else {
                    alert("Please select Status Translation!");
                }
            };

            function toggleModalDeleteSt() {
                if (vm.mySelectedItemsSt[0]) {
                    $scope.title = "Delete Status Translation";
                    vm.showModalDeleteSt = !vm.showModalDeleteSt;
                }
                else {
                    alert("Please select Status Translation!");
                }
            };

            function getAllStatuses() {
                return ajaxService.ajaxPost(vm.pagination, "api/statusService/GetStatuses").then(function (data) {
                    vm.statuses = data.statuses;
                    return vm.statuses;
                });
            }

            function getStatusTranslationsForActionType(actionTypeId) {
                vm.paginationSt.actionTypeId = actionTypeId;
                return ajaxService.ajaxPost(vm.paginationSt, "api/statusTranslationService/GetStatusTranslationsForActionType").then(function (data) {
                    if (data.statusTranslations && data.statusTranslations.length > 0) {
                        for (var i = 0; i < data.statusTranslations.length; i++) {
                            for (var j = 0; j < vm.statuses.length; j++) {
                                if (data.statusTranslations[i].statusIdFrom === vm.statuses[j].id) {
                                    data.statusTranslations[i].statusFromName = vm.statuses[j].name;
                                }
                                if (data.statusTranslations[i].statusIdTo === vm.statuses[j].id) {
                                    data.statusTranslations[i].statusToName = vm.statuses[j].name;
                                }
                            }
                        }
                    }
                    vm.statusTranslations = data.statusTranslations;
                    return vm.statusTranslations;
                });
            }

            function createStatusTranslation() {
                var statusTranslation = new Object();
                statusTranslation.Id = vm.statusTranslation.id;
                statusTranslation.StatusIdFrom = vm.statusTranslation.statusIdFrom;
                statusTranslation.StatusIdTo = vm.statusTranslation.statusIdTo;
                statusTranslation.ActionTypeId = vm.mySelectedItems[0].id;
                return ajaxService.ajaxPost(statusTranslation, "api/statusTranslationService/CreateStatusTranslation").then(function (data) {
                    vm.showModalCreateSt = !vm.showModalCreateSt;
                    getStatusTranslationsForActionType(vm.mySelectedItems[0].id);
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            function updateStatusTranslation() {
                var statusTranslation = new Object();
                statusTranslation.Id = vm.statustranslation.id;
                statusTranslation.StatusIdFrom = vm.statustranslation.statusIdFrom;
                statusTranslation.StatusIdTo = vm.statustranslation.statusIdTo;
                statusTranslation.ActionTypeId = vm.statustranslation.actionTypeId;
                return ajaxService.ajaxPost(statusTranslation, "api/statusTranslationService/UpdateStatusTranslation").then(function (data) {
                    vm.showModalUpdateSt = !vm.showModalUpdateSt;
                    getStatusTranslationsForActionType(vm.mySelectedItems[0].id);
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            function deleteStatusTranslation() {
                var statusTranslation = new Object();
                statusTranslation.Id = vm.mySelectedItemsSt[0].id;
                statusTranslation.StatusIdFrom = vm.mySelectedItemsSt[0].statusIdFrom;
                statusTranslation.StatusIdTo = vm.mySelectedItemsSt[0].statusIdTo;
                statusTranslation.ActionTypeId = vm.mySelectedItemsSt[0].actionTypeId;

                return ajaxService.ajaxPost(statusTranslation, "api/statusTranslationService/DeleteStatusTranslation").then(function (data) {
                    vm.showModalDeleteSt = !vm.showModalDeleteSt;
                    getStatusTranslationsForActionType(vm.mySelectedItems[0].id);
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

            function getActionType()
            {
                vm.actionType = { id: "0" };
                return vm.actionType;
            }

            function getStatusTranslation() {
                vm.statusTranslation = { id: "0" };
                return vm.statusTranslation;
            }

            function getWorkflowTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/workflowTypeService/GetWorkflowTypes").then(function (data) {
                    vm.workflowTypes = data.workflowTypes;
                    return vm.workflowTypes;
                });
            }
        }

        }
    ]);
