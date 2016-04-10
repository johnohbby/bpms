angular.module("app").register.controller('workflowController',
    ['$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($routeParams, $location, ajaxService, alertService) {
        "use strict";
        var vm = this;

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
            vm.workflow = { name: "", workflowTypeId: -1, createdBy: 3 }; //get user from session
            vm.selectedWorkflow = {name:""};
            
            //New Workflow
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            
            


        //FUNCTIONS
            vm.getWorkflows = getWorkflows;
            vm.getWorkflowsByFolderId = getWorkflowsByFolderId;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;      
            vm.createWorkflow = createWorkflow;
            vm.deleteWorkflow = deleteWorkflow;
            vm.updateWorkflow = updateWorkflow;


            function init() {            
                GetWorkflowData(); // returns workflowFolders, workflowTypes
            }

            init();
            
            function getWorkflowsByFolderId(folderId) {
                vm.folderId = folderId;
                getWorkflows(folderId);
            }
            function GetWorkflowData() {
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflowData").then(function (data) {
                    vm.workflowFolders = data.workflowFolders;
                    vm.workflowTypes = data.workflowTypes;
                });
            }

            function getWorkflows(folderId)
            {
                vm.pagination.FolderId = folderId;
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflows").then(function (data) {
                    vm.workflows = data.workflows;
                    return vm.workflows;
                });
            }
            function createWorkflow()
            {                
                return ajaxService.ajaxPost(vm.workflow, "api/workflowService/CreateWorkflow").then(function (data) {
                    vm.showModalCreate = false;                    
                    getWorkflows(vm.folderId);
                });
            }
            function updateWorkflow() {

                return ajaxService.ajaxPost(vm.selectedWorkflow, "api/workflowService/UpdateWorkflow").then(function (data) {
                    vm.showModalUpdate = false;
                    getWorkflows(vm.folderId);
                });
            }
            
            function deleteWorkflow(item) {
                vm.workflow.Id = item[0].id;
                
                return ajaxService.ajaxPost(vm.workflow, "api/workflowService/DeleteWorkflow").then(function (data) {
                    vm.showModalCreate = false;
                    getWorkflows(vm.folderId);
                });
            }
            
            function toggleModalCreate() {
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate(item) {
                console.log(item);
                vm.selectedWorkflow.name = item.name;
                vm.selectedWorkflow.id = item.id;
                vm.selectedWorkflow.caseNumber = item.caseNumber;
                vm.selectedWorkflow.workflowTypeId = item.workflowTypeId
                
                vm.showModalUpdate = !vm.showModalUpdate;
            };

            function closeAlert(index) {
                vm.alerts.splice(index, 1);
            };

            function clearValidationErrors() {
                vm.workflowNameInputError = false;
            }
        }

        }
    ]);



