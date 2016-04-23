angular.module("app").register.controller('mailTemplateController',
    ['$scope','$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($scope,$routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.mailTemplates = [];
            vm.mailTemplate = {id:"0"};
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 1000
            };
            vm.paginationMap = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 1000
            };
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.showModalDelete = false;
            vm.showModalCreateMap = false;
            vm.showModalDeleteMap = false;
            vm.mySelectedItems = [];
            vm.mailtemplate = {};
            vm.mailTemplateMap = { id: "0" };
            vm.mailTemplateMaps = [];
            vm.mySelectedItemsMap = [];
            vm.actionTypes = [];

            //functions
            vm.getAllMailTemplates = getAllMailTemplates;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createMailTemplate = createMailTemplate;
            vm.updateMailTemplate = updateMailTemplate;
            vm.deleteMailTemplate = deleteMailTemplate;
            vm.toggleModal = toggleModal;
            vm.toggleModalCreateMap = toggleModalCreateMap;
            vm.toggleModalDeleteMap = toggleModalDeleteMap;
            vm.createMailTemplateMap = createMailTemplateMap;
            vm.deleteMailTemplateMap = deleteMailTemplateMap;
            vm.getMapsForMailTemplate = getMapsForMailTemplate;
            vm.getAllActionTypes = getAllActionTypes;
          
         

            function init() {
                getAllMailTemplates();
                getAllActionTypes();
            }

            init();

            vm.titles = [];

            $scope.$watch('vm.mySelectedItems[0]', function (value) {
                if(vm.mySelectedItems[0])
                {
                    getMapsForMailTemplate(vm.mySelectedItems[0].id);
                }
                else {
                }
            });

            function getAllActionTypes()
            {
                return ajaxService.ajaxPost(vm.pagination, "api/actionTypeService/GetActionTypes").then(function (data) {
                    vm.actionTypes = data.actionTypes;
                    return vm.actionTypes;
                });
            }

            function toggleModal()
            {
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
                vm.showModalCreateMap = false;
                vm.showModalDeleteMap = false
            }

            function toggleModalCreate() {
                $scope.title = "Create Mail Template";
                vm.mailTemplate = getMailTemplate();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Edit Mail Template";
                    vm.mailtemplate = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select Mail Template!");
                }
            };

            function toggleModalDelete() {
                if (vm.mailTemplateMaps.length > 0) {
                    alert("First delete all mail template maps!");
                }
                else if (vm.mySelectedItems[0]) {
                    $scope.title = "Delete Mail Template";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Mail Template!");
                }
            };

            function getAllMailTemplates() {
                return ajaxService.ajaxPost(vm.pagination, "api/mailTemplateService/GetMailTemplates").then(function (data) {
                    vm.mailTemplates = data.mailTemplates;
                    return vm.mailTemplates;
                });
            }

            function createMailTemplate() {
                var mailTemplate = new Object();
                mailTemplate.Id = vm.mailTemplate.id;
                mailTemplate.Subject = vm.mailTemplate.subject;
                mailTemplate.Body = vm.mailTemplate.body;

                return ajaxService.ajaxPost(mailTemplate, "api/mailTemplateService/CreateMailTemplate").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    getAllMailTemplates();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateMailTemplate() {
                var mailTemplate = new Object();
                mailTemplate.Id = vm.mailtemplate.id;
                mailTemplate.Subject = vm.mailtemplate.subject;
                mailTemplate.Body = vm.mailtemplate.body;
                return ajaxService.ajaxPost(mailTemplate, "api/mailTemplateService/UpdateMailTemplate").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    getAllMailTemplates();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            function deleteMailTemplate() {
                var mailTemplate = new Object();
                mailTemplate.Id = vm.mySelectedItems[0].id;
                return ajaxService.ajaxPost(mailTemplate, "api/mailTemplateService/DeleteMailTemplate").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    getAllMailTemplates();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            //MAIL TEMPLATE MAP
            function toggleModalCreateMap() {
                $scope.title = "Create Mail Template Map";
                vm.mailTemplateMap = getMailTemplateMap();
                vm.showModalCreateMap = !vm.showModalCreateMap;
            };

           
            function toggleModalDeleteMap() {
                if (vm.mySelectedItemsMap[0]) {
                    $scope.title = "Delete Mail Template Map";
                    vm.showModalDeleteMap = !vm.showModalDeleteMap;
                }
                else {
                    alert("Please select Mail Template Map!");
                }
            };

            function getMapsForMailTemplate(mailTemplateId) {
                vm.paginationMap.mailTemplateId = mailTemplateId;
                vm.mailTemplateMaps = [];
                return ajaxService.ajaxPost(vm.paginationMap, "api/mailTemplateMapService/GetMapsForMailTemplate").then(function (data) {
                    if (data.mailTemplateMaps)
                    {
                        for(var i=0; i<data.mailTemplateMaps.length; i++)
                        {
                            for(var j=0; j<vm.actionTypes.length; j++)
                            {
                                if(data.mailTemplateMaps[i].actionTypeId===vm.actionTypes[j].id)
                                {
                                    data.mailTemplateMaps[i].actionTypeName = vm.actionTypes[j].name;
                                    break;
                                }
                            }
                        }
                    }
                    vm.mailTemplateMaps = data.mailTemplateMaps;
                    return vm.mailTemplateMaps;
                });
            }

            function createMailTemplateMap() {
                var mailTemplateMap = new Object();
                mailTemplateMap.Id = vm.mailTemplateMap.id;
                mailTemplateMap.MailTemplateId = vm.mySelectedItems[0].id;
                mailTemplateMap.ActionTypeId = vm.mailTemplateMap.actionTypeId;

                return ajaxService.ajaxPost(mailTemplateMap, "api/mailTemplateMapService/CreateMailTemplateMap").then(function (data) {
                    vm.showModalCreateMap = !vm.showModalCreateMap;
                    getMapsForMailTemplate(vm.mySelectedItems[0].id);
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            function deleteMailTemplateMap() {
                var mailTemplateMap = new Object();
                mailTemplateMap.Id = vm.mySelectedItemsMap[0].id;

                return ajaxService.ajaxPost(mailTemplateMap, "api/mailTemplateMapService/DeleteMailTemplateMap").then(function (data) {
                    vm.showModalDeleteMap = !vm.showModalDeleteMap;
                    getMapsForMailTemplate(vm.mySelectedItems[0].id);
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }
            

            function getMailTemplate()
            {
                vm.mailTemplate = { id: "0" };
                return vm.mailTemplate;
            }

            function getMailTemplateMap() {
                vm.mailTemplateMap = { id: "0" };
                return vm.mailTemplateMap;
            }
        }

        }
    ]);
