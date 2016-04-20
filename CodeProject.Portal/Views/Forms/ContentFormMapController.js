angular.module("app").register.controller('contentFormMapController',
    ['$scope', '$routeParams', '$location', 'ajaxService', 'alertService',
    function ($scope, $routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.messageBox = "";
            vm.alerts = [];
            vm.contentFormMaps = [];
            vm.contentFormMap = { id: "0" };
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
            vm.contentformMap = {};
            vm.forms = [];
            vm.contentTypes = [];
            vm.contents = [];
            vm.actionTypes = [];
            vm.workflowTypes = [];

            //functions
            vm.getAllContentFormMaps = getAllContentFormMaps;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createContentFormMap = createContentFormMap;
            vm.updateContentFormMap = updateContentFormMap;
            vm.deleteContentFormMap = deleteContentFormMap;
            vm.toggleModal = toggleModal;
            vm.getAllForms = getAllForms;
            vm.getAllContentTypes = getAllContentTypes;
            vm.getAllContents = getAllContents;
            vm.getAllWorkflowTypes = getAllWorkflowTypes;
            vm.getAllActionTypes = getAllActionTypes;


            function init() {
                getAllForms();
                getAllContentTypes();
                getAllContentFormMaps();
                getAllActionTypes();
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
                $scope.title = "Create Content Form Map";
                vm.contentFormMap = getContentFormMap();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Edit Content Form Map";
                    vm.contentFormMap = vm.mySelectedItems[0];
                    vm.showModalCreate = !vm.showModalCreate;
                }
                else {
                    alert("Please select Content Form Map!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Delete Content Form Map";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Content Form Map!");
                }
            };
            
            function getAllForms() {
                return ajaxService.ajaxPost(vm.pagination, "api/formService/GetForms").then(function (data) {
                    vm.forms = data.forms;
                    return vm.forms;
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
                    return vm.contents;
                });
            }

            function getAllWorkflowTypes() {
                return ajaxService.ajaxPost(vm.pagination, "api/workflowTypeService/GetWorkflowTypes").then(function (data) {
                   
                    vm.workflowTypes = data.workflowTypes;
                    return vm.contents;
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
                        vm.contents = vm.actionTypes;
                    }

                    else if(code==='WORKFLOW')
                    {
                        vm.contents = vm.workflowTypes;
                    }

                    else
                    {

                    }

                }
            }

           

            function getAllContentFormMaps() {
                return ajaxService.ajaxPost(vm.pagination, "api/contentFormMapService/GetContentFormMaps").then(function (data) {
                    if (data.contentFormMaps.length > 0) {
                        for (var i = 0; i < data.contentFormMaps.length; i++) {
                            for (var j = 0; j < vm.forms.length; j++) {
                                if (data.contentFormMaps[i].formId === vm.forms[j].id) {
                                    data.contentFormMaps[i].formName = vm.forms[j].name;
                                    break;
                                }
                            }

                            for (var k = 0; k < vm.contentTypes.length; k++) {
                                if (data.contentFormMaps[i].contentTypeId === vm.contentTypes[k].id) {
                                    data.contentFormMaps[i].contentTypeName = vm.contentTypes[k].name;
                                    data.contentFormMaps[i].contentName = setContentName(vm.contentTypes[k].code, data.contentFormMaps[i].contentId);
                                    break;
                                }
                            }
                        }
                    }
                    vm.contentFormMaps = data.contentFormMaps;
                    return vm.contentFormMaps;
                });
            };

            function setContentName(contentTypeCode, contentId)
            {
                if(contentTypeCode==='ACTION')
                {
                    for(var i=0; i<vm.actionTypes.length; i++)
                    {
                        if(vm.actionTypes[i].id===contentId)
                        {
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

            function createContentFormMap() {
                var contentFormMap = new Object();
                contentFormMap.Id = vm.contentFormMap.id;
                contentFormMap.FormId = vm.contentFormMap.formId;
                contentFormMap.ContentTypeId = vm.contentFormMap.contentTypeId;
                contentFormMap.ContentId = vm.contentFormMap.contentId;
               
                return ajaxService.ajaxPost(contentFormMap, "api/contentFormMapService/CreateContentFormMap").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    vm.getAllContentFormMaps();

                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateContentFormMap() {
                var contentFormMap = new Object();
                contentFormMap.Id = vm.contentFormMap.id;
                contentFormMap.FormId = vm.contentFormMap.formId;
                contentFormMap.ContentTypeId = vm.contentFormMap.contentTypeId;
                contentFormMap.ContentId = vm.contentFormMap.contentId;

                return ajaxService.ajaxPost(contentFormMap, "api/contentFormMapService/UpdateContentFormMap").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    vm.getAllContentFormMaps();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteContentFormMap() {
                var contentFormMap = new Object();
                contentFormMap.Id = vm.mySelectedItems[0].id;
                contentFormMap.FormId = vm.mySelectedItems[0].formId;
                contentFormMap.ContentTypeId = vm.mySelectedItems[0].contentTypeId;
                contentFormMap.ContentId = vm.mySelectedItems[0].contentId;

                return ajaxService.ajaxPost(contentFormMap, "api/contentFormMapService/DeleteContentFormMap").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    vm.getAllContentFormMaps();
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

            function getContentFormMap() {
                vm.contentFormMap = { id: "0" };
                return vm.contentFormMap;
            }
        }

    }
    ]);
