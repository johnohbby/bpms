﻿angular.module("app").register.controller('workflowController',
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
            vm.showWorkflow = false;
            vm.workflow = { name: "", workflowTypeId: -1, createdBy: 3 }; //get user from session
            vm.action = { WorkflowId: id, ActionTypeId: -1, createdBy: 3, DelegatedId: -1 }; //get user from session
            vm.selectedWorkflow = {name:""};
            
            //New Workflow
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.ToggleModalCreate = ToggleModalCreate;
            vm.ToggleModalUpdate = ToggleModalUpdate;
            vm.nextDelegated = null;

            vm.selected = {};

            //New Action
            vm.showModalActionCreate = false;
            vm.ToggleModalActionCreate = ToggleModalActionCreate;
            
            


        //FUNCTIONS
            vm.GetWorkflows = GetWorkflows;
            vm.GetWorkflowsByFolderId = GetWorkflowsByFolderId;
            vm.CloseAlert = CloseAlert;
            vm.ClearValidationErrors = ClearValidationErrors;      
            vm.CreateWorkflow = CreateWorkflow;
            vm.DeleteWorkflow = DeleteWorkflow;
            vm.UpdateWorkflow = UpdateWorkflow;
            vm.ViewWorkflow = ViewWorkflow;
            vm.CreateAction = CreateAction;
            vm.ToggleSelection = ToggleSelection;

            function init() {
                if (id == undefined) {
                    $("#tableWorkflows").show();
                    GetWorkflowsData(); // returns workflowFolders, workflowTypes
                }
                else {
                    vm.showWorkflows = false;
                    $("#divWorkflow").show();
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
                vm.pagination.Id = id;
                vm.pagination.UserId = 3;
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflowDataById").then(function (data) {
                   
                    vm.actions = data.actions;
                    vm.nextActionTypes = data.nextActionTypes;
                    $("#tableActions").show();
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
            function ToggleSelection(item) {

                console.log(item);

            }
            function CreateAction() {
                var selectedUsers = []; 
                for (var i = 0; i < vm.nextDelegated.length; i++)
                {
                    var id = vm.nextDelegated[i].id;
                    if (vm.selected[id] == true)
                    {
                        selectedUsers.push(vm.nextDelegated[i]);
                    }
                }
                vm.action.Delegated = selectedUsers;
                return ajaxService.ajaxPost(vm.action, "api/workflowService/CreateAction").then(function (data) {
                    vm.showModalActionCreate = false;
                    GetWorkflowDataById(vm.action.WorkflowId);
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
            function ToggleModalActionCreate() {
                GetDelegated();
            }

            function GetDelegated() {             
                return ajaxService.ajaxPost(vm.action, "api/workflowService/GetDelegated").then(function (data) {
                    vm.nextDelegated = data.delegated;
                    vm.showModalActionCreate = !vm.showModalActionCreate;
                });                
            }

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



