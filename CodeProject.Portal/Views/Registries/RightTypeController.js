﻿angular.module("app").register.controller('rightTypeController',
    ['$scope','$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($scope,$routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.messageBox = "";
            vm.alerts = [];
            vm.rightTypes = [];
            vm.rightType = {id:"0"};
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Name",
                sortDirection: "ASC",
                pageSize: 1000
            };
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.showModalDelete = false;
            vm.mySelectedItems = [];
            vm.righttype = {};

            //functions
            vm.getAllRightTypes = getAllRightTypes;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createRightType = createRightType;
            vm.updateRightType = updateRightType;
            vm.deleteRightType = deleteRightType;
            vm.toggleModal = toggleModal;
         

            function init() {
                getAllRightTypes();
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
                $scope.title = "Create Right Type";
                vm.rightType = getRightType();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems) {
                    $scope.title = "Edit Right Type";
                    vm.righttype = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select Right Type!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems) {
                    $scope.title = "Delete Right Type";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Right Type!");
                }
            };

            function getAllRightTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/rightTypeService/GetRightTypes").then(function (data) {
                    vm.rightTypes = data.rightTypes;
                    return vm.rightTypes;
                });
            }

            function createRightType() {
                var rightType = new Object();
                rightType.Id = vm.rightType.id;
                rightType.Code = vm.rightType.code;
                rightType.Name = vm.rightType.name;
                rightType.Description = vm.rightType.description;
                return ajaxService.ajaxPost(rightType, "api/rightTypeService/CreateRightType").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                   /* if (data) {
                        vm.rightType.id = data.id;
                        vm.rightTypes.push(vm.rightType);
                        return vm.rightTypes;
                    }*/
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateRightType() {
                var rightType = new Object();
                rightType.Id = vm.righttype.id;
                rightType.Code = vm.righttype.code;
                rightType.Name = vm.righttype.name;
                rightType.Description = vm.righttype.description;
                return ajaxService.ajaxPost(rightType, "api/rightTypeService/UpdateRightType").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    /* if (data) {
                         vm.rightType.id = data.id;
                         vm.rightTypes.push(vm.rightType);
                         return vm.rightTypes;
                     }*/
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteRightType() {
                var rightType = new Object();
                rightType.Id = vm.mySelectedItems[0].id;
                rightType.Code = vm.mySelectedItems[0].code;
                rightType.Name = vm.mySelectedItems[0].name;
                rightType.Description = vm.mySelectedItems[0].description;
                return ajaxService.ajaxPost(rightType, "api/rightTypeService/DeleteRightType").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    /* if (data) {
                         vm.rightType.id = data.id;
                         vm.rightTypes.push(vm.rightType);
                         return vm.rightTypes;
                     }*/
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

            function getRightType()
            {
                vm.rightType = { id: "0" };
                return vm.rightType;
            }
        }

        }
    ]);
