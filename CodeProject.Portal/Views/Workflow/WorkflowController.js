angular.module("app").register.controller('workflowController',
    ['$routeParams', '$location', 'ajaxService', 'alertService', 'loginService','$scope', '$compile',
    function ($routeParams, $location, ajaxService, alertService, loginService, $scope, $compile) {
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
                pageSize: 10,
                UserId: loginService.getLoggedUser().id,
                Forms:[]
            };
            vm.forms = [];
            vm.formId = -1;
            vm.formsData = { Forms: [] , FormId: -1, ActionTypeId: -1};
            vm.mySelectedItems = [];
            
            //Workflows 
            vm.showWorkflows = true;
            vm.showWorkflow = false;
            vm.workflow = { name: "", workflowTypeId: -1, createdBy: loginService.getLoggedUser().id }; //get user from session
            vm.action = { WorkflowId: id, ActionTypeId: -1, createdBy: loginService.getLoggedUser().id, DelegatedId: -1 }; //get user from session
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
            vm.GetDelegatedUsers = GetDelegatedUsers;
            vm.CancelAction = CancelAction;
            vm.GetForms = GetForms;
            vm.SaveForms = SaveForms;
            

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
              
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflowDataById").then(function (data) {
                   
                    vm.actions = data.actions;
                    vm.nextActionTypes = data.nextActionTypes;
                    if (vm.nextActionTypes.length > 0)
                    vm.action.ActionTypeId = vm.nextActionTypes[0].id;
                    $("#tableActions").show();
                });
            }
            function GetWorkflowsData() {
                console.log(vm.pagination);
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
                vm.SaveForms();
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
            function CancelAction() {
                console.log(vm.forms);
                vm.showModalActionCreate = false;
                
            }

            function GetForms() {
                vm.pagination.ActionTypeId = vm.action.ActionTypeId;
                return ajaxService.ajaxPost(vm.pagination, "api/formService/GetFormData").then(function (data) {
                  

                    for (var i = 0; i < data.formFields.length; i++) {
                        
                        var formField = data.formFields[i];
                        vm.formId = formField.formId;
                        var elType = formField.fieldType;
                        var ngModel = "vm.forms.f" + formField.formId + "."+formField.name;
                        var element = "<div class='form-group'><label>" + formField.name + "</label><input type='" + elType + "' ng-model='" + ngModel + "' /></div>";
                        
                        $("#forms").append($compile(element)($scope));
                    }
                    
                });
            }

            function SaveForms() {
                console.log('llll');
                


                vm.formsData.ActionTypeId = vm.action.ActionTypeId;
                vm.formsData.Forms = eval("vm.forms.f" + vm.formId);
                vm.formsData.Id = vm.formId;
                console.log(vm.formsData);
                
                return ajaxService.ajaxPost(vm.formsData, "api/formService/SaveForms").then(function (data) {


                });
            }
            function GetDelegatedUsers() {GetDelegated(); }
            function GetDelegated() {
                vm.GetForms();
                return ajaxService.ajaxPost(vm.action, "api/workflowService/GetDelegated").then(function (data) {
                    
                    vm.showModalActionCreate = true;
                    vm.nextDelegated = data.delegated;
                    vm.nextActionTypes = vm.nextActionTypes;
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



