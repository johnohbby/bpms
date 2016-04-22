angular.module("app").register.controller('contentRightController',
    ['$scope', '$routeParams', '$location', 'ajaxService', 'alertService',
    function ($scope, $routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.messageBox = "";
            vm.alerts = [];
            vm.contentRights = [];
            vm.contentRight = { id: "0" };
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
            vm.contentright = {};
            vm.groups = [];
            vm.contentTypes = [];
            vm.contents = [];
            vm.rightTypes = [];
            vm.actionTypes = [];
            vm.workflowTypes = [];


            //functions
            vm.getAllContentRights = getAllContentRights;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createContentRight = createContentRight;
            vm.updateContentRight = updateContentRight;
            vm.deleteContentRight = deleteContentRight;
            vm.toggleModal = toggleModal;
            vm.getAllGroups = getAllGroups;
            vm.getAllContentTypes = getAllContentTypes;
            vm.getAllContents = getAllContents;
            vm.getAllRightTypes = getAllRightTypes;
            vm.getAllWorkflowTypes = getAllWorkflowTypes;
            vm.getAllActionTypes = getAllActionTypes;


            function init() {
                getAllGroups();
                getAllContentTypes();
                getAllRightTypes();
                getAllActionTypes();
                getAllWorkflowTypes();

              //  getAllContentRights();
            }

            init();

            vm.titles = [];

            function toggleModal() {
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
            }

            function toggleModalCreate() {
                $scope.title = "Create Content Right";
                vm.contentRight = getContentRight();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Edit Content Right";
                    vm.contentright = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select Content Right!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Delete Content Right";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Content Right!");
                }
            };
            
            function getAllGroups() {
                return ajaxService.ajaxPost(vm.pagination, "api/groupService/GetGroups").then(function (data) {
                    vm.groups = data.groups;
                    return vm.groups;
                });
            };

            function getAllContentTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/contentRightService/GetContentTypes").then(function (data) {
                    vm.contentTypes = data.contentTypes;
                    return vm.contentTypes;
                });
            };

            function getAllActionTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/actionTypeService/GetActionTypes").then(function (data) {
                    vm.actionTypes = data.actionTypes;
                    return vm.actionTypes;
                });
            }

            function getAllWorkflowTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/workflowTypeService/GetWorkflowTypes").then(function (data) {
                    vm.workflowTypes = data.workflowTypes;
                    return vm.workflowTypes;
                });
            }

            function getContentTypeCode(id)
            {
                for(var i=0; i<vm.contentTypes.length; i++)
                {
                    if (vm.contentTypes[i].id === id)
                        return vm.contentTypes[i].code;
                }

                return null;
            }

            function getAllContents(contentTypeId)
            {
                var code = getContentTypeCode(contentTypeId);
                if(code)
                {
                    if(code==='ACTION')
                    {
                        //vm.getAllActionTypes();
                        vm.contents = vm.actionTypes;
                    }

                    else if(code==='WORKFLOW')
                    {
                        //vm.getAllWorkflowTypes();
                        vm.contents = vm.workflowTypes;
                    }

                    else
                    {

                    }

                }
            }

            function getAllRightTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/rightTypeService/GetRightTypes").then(function (data) {
                    getAllContentRights();
                    vm.rightTypes = data.rightTypes;
                    return vm.rightTypes;
                });
            };

            function getAllContentRights() {
                return ajaxService.ajaxPost(vm.pagination, "api/contentRightService/GetContentRights").then(function (data) {
                    if (data.contentRights.length > 0) {
                        for (var i = 0; i < data.contentRights.length; i++) {
                            for (var j = 0; j < vm.groups.length; j++) {
                                if (data.contentRights[i].groupId === vm.groups[j].id) {
                                    data.contentRights[i].groupName = vm.groups[j].name;
                                    break;
                                }
                            }

                            for (var k = 0; k < vm.contentTypes.length; k++) {
                                if (data.contentRights[i].contentTypeId === vm.contentTypes[k].id) {
                                    data.contentRights[i].contentTypeName = vm.contentTypes[k].name;
                                    data.contentRights[i].contentName = setContentName(vm.contentTypes[k].code, data.contentRights[i].contentId);
                                    break;
                                }
                            }

                            for (var m = 0; m < vm.rightTypes.length; m++) {
                                if (data.contentRights[i].rightTypeId === vm.rightTypes[m].id) {
                                    data.contentRights[i].rightTypeName = vm.rightTypes[m].name;
                                    break;
                                }
                            }
                        }
                    }
                    vm.contentRights = data.contentRights;
                    return vm.contentRights;
                });
            };

            function setContentName(contentTypeCode, contentId) {
                if (contentTypeCode === 'ACTION') {
                    for (var i = 0; i < vm.actionTypes.length; i++) {
                        if (vm.actionTypes[i].id === contentId) {
                            return vm.actionTypes[i].name;
                        }
                    }
                }

                else if (contentTypeCode === 'WORKFLOW') {
                    for (var i = 0; i < vm.workflowTypes.length; i++) {
                        if (vm.workflowTypes[i].id === contentId) {
                            return vm.workflowTypes[i].name;
                        }
                    }
                }

                else {
                    return "";
                }
            }

            function createContentRight() {
                var contentRight = new Object();
                contentRight.Id = vm.contentRight.id;
                contentRight.GroupId = vm.contentRight.groupId;
                contentRight.ContentTypeId = vm.contentRight.contentTypeId;
                contentRight.ContentId = vm.contentRight.contentId;
                contentRight.RightTypeId = vm.contentRight.rightTypeId;
                contentRight.UserId = null;
               
                return ajaxService.ajaxPost(contentRight, "api/contentRightService/CreateContentRight").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    vm.getAllContentRights();

                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateContentRight() {
                var contentRight = new Object();
                contentRight.Id = vm.contentright.id;
                contentRight.GroupId = vm.contentright.groupId;
                contentRight.ContentTypeId = vm.contentright.contentTypeId;
                contentRight.ContentId = vm.contentright.contentId;
                contentRight.RightTypeId = vm.contentright.rightTypeId;
                contentRight.UserId = null;
                return ajaxService.ajaxPost(contentRight, "api/contentRightService/UpdateContentRight").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    vm.getAllContentRights();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteContentRight() {
                var contentRight = new Object();
                contentRight.Id = vm.mySelectedItems[0].id;
                contentRight.GroupId = vm.mySelectedItems[0].groupId;
                contentRight.ContentTypeId = vm.mySelectedItems[0].contentTypeId;
                contentRight.ContentId = vm.mySelectedItems[0].contentId;
                contentRight.RightTypeId = vm.mySelectedItems[0].rightTypeId;
                contentRight.UserId = null;

                return ajaxService.ajaxPost(contentRight, "api/contentRightService/DeleteContentRight").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    vm.getAllContentRights();
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

            function getContentRight() {
                vm.contentRight = { id: "0" };
                return vm.contentRight;
            }

          /*  function toggleModal() {
                vm.showModal != vm.showModal;
            }*/
        }

    }
    ]);
