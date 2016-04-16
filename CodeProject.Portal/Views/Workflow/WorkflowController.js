angular.module("app").register.controller('workflowController',
    ['$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($routeParams, $location, ajaxService, alertService) {
        "use strict";
        var vm = this;
        var id = $location.search().id;
        //alert(a);
        this.initializeController = function () {

        //VARIABLES
            //glogal
            vm.title = "Workflows";
            vm.messageBox = "";
            vm.alerts = [];
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Name",
                sortDirection: "ASC",
                pageSize: 10
            };
            vm.mySelectedItems = [];
            
            //Workflows 
            vm.showWorkflows = true;
            vm.workflow = { name: "", workflowTypeId: -1, createdBy: 3 }; //get user from session
            vm.selectedWorkflow = {name:""};
            
            //New Workflow
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.ToggleModalCreate = ToggleModalCreate;
            vm.ToggleModalUpdate = ToggleModalUpdate;
            
            


        //FUNCTIONS
            vm.GetWorkflows = GetWorkflows;
            vm.GetWorkflowsByFolderId = GetWorkflowsByFolderId;
            vm.CloseAlert = CloseAlert;
            vm.ClearValidationErrors = ClearValidationErrors;      
            vm.CreateWorkflow = CreateWorkflow;
            vm.DeleteWorkflow = DeleteWorkflow;
            vm.UpdateWorkflow = UpdateWorkflow;
            vm.ViewWorkflow = ViewWorkflow;

            function init() {
                if (id == undefined) {
                    $("#tableWorkflows").show();
                    GetWorkflowsData(); // returns workflowFolders, workflowTypes
                }
                else {
                    vm.showWorkflows = false;
                    GetWorkflowDataById(id);
                }
            }

            init();
            function ViewWorkflow(path) {
                $location.url(path);
            }
            function GetWorkflowsByFolderId(folderId) {
                vm.folderId = folderId;
                GetWorkflows(folderId);
            }
            function GetWorkflowDataById(id) {
                vm.Id = id;
                console.log(id);
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflowDataById").then(function (data) {
                    vm.workflowFolders = data.workflowFolders;
                    vm.workflowTypes = data.workflowTypes;
                    
                    //get workflows for first folder in list
                    if(vm.workflowFolders.length > 0)
                    GetWorkflows(vm.workflowFolders[0]['id']);
                });
            }
            function GetWorkflowsData() {
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflowData").then(function (data) {
                    vm.workflowFolders = data.workflowFolders;
                    vm.workflowTypes = data.workflowTypes;

                    //get workflows for first folder in list
                    if (vm.workflowFolders.length > 0)
                        GetWorkflows(vm.workflowFolders[0]['id']);
                });
            }
            function GetWorkflows(folderId)
            {
                vm.pagination.FolderId = folderId;
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflows").then(function (data) {
                    vm.workflows = data.workflows;
                    return vm.workflows;
                });
            }
            function CreateWorkflow()
            {                
                return ajaxService.ajaxPost(vm.workflow, "api/workflowService/CreateWorkflow").then(function (data) {
                    vm.showModalCreate = false;                    
                    GetWorkflows(vm.folderId);
                });
            }
            function UpdateWorkflow() {

                return ajaxService.ajaxPost(vm.selectedWorkflow, "api/workflowService/UpdateWorkflow").then(function (data) {
                    vm.showModalUpdate = false;
                    GetWorkflows(vm.folderId);
                });
            }
            
            function DeleteWorkflow(item) {
                vm.workflow.Id = item[0].id;
                
                return ajaxService.ajaxPost(vm.workflow, "api/workflowService/DeleteWorkflow").then(function (data) {
                    vm.showModalCreate = false;
                    GetWorkflows(vm.folderId);
                });
            }
            
            function ToggleModalCreate() {
                vm.showModalCreate = !vm.showModalCreate;
            };

            function ToggleModalUpdate(item) {
                console.log(item);
                vm.selectedWorkflow.name = item.name;
                vm.selectedWorkflow.id = item.id;
                vm.selectedWorkflow.caseNumber = item.caseNumber;
                vm.selectedWorkflow.workflowTypeId = item.workflowTypeId
                
                vm.showModalUpdate = !vm.showModalUpdate;
            };

            function CloseAlert(index) {
                vm.alerts.splice(index, 1);
            };

            function ClearValidationErrors() {
                vm.workflowNameInputError = false;
            }
        }

        }
    ]);



