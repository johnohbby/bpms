angular.module("app").register.controller('workflowController',
    ['$routeParams', '$location', 'ajaxService', 'alertService', 'loginService','$scope', '$compile', '$sce',
    function ($routeParams, $location, ajaxService, alertService, loginService, $scope, $compile, $sce) {
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
            vm.showForms = false;
            vm.forms = [];
            vm.formId = -1;
            vm.folderId = -1;
            vm.formsData = { Forms: [] , FormId: -1, ActionTypeId: -1};
            vm.mySelectedItems = [];
            
            //Workflows 
            vm.showWorkflows = true;
            vm.showWorkflow = false;
            vm.workflow = { name: "", workflowTypeId: -1, createdBy: loginService.getLoggedUser().id }; //get user from session
            vm.action = { Id:-1, WorkflowId: id, ActionTypeId: -1, createdBy: loginService.getLoggedUser().id, DelegatedId: -1 }; //get user from session
            vm.selectedWorkflow = {name:""};
            
            //New Workflow
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.ToggleModalCreate = ToggleModalCreate;
            vm.ToggleModalUpdate = ToggleModalUpdate;
            vm.nextDelegated = null;
            vm.toShortDateTime = toShortDateTime;

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
            vm.CancelWorkflow = CancelWorkflow;
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

                    for(var i = 0; i<vm.actions.length; i++){
                       
                        vm.actions[i].forms = $sce.trustAsHtml(vm.actions[i].forms);
                        
                    }



                    vm.nextActionTypes = data.nextActionTypes;
                    console.log("next");
                    console.log(vm.nextActionTypes);
                    if (vm.nextActionTypes.length > 0)
                    vm.action.ActionTypeId = vm.nextActionTypes[0].id;
                    $("#tableActions").show();
                });
            }
            function GetWorkflowsData() {
                
                return ajaxService.ajaxPost(vm.pagination, "api/workflowService/GetWorkflowData").then(function (data) {
                    vm.workflowFolders = data.workflowFolders;
                    vm.workflowTypes = data.workflowTypes;

                    if (vm.workflowTypes.length > 0)
                        vm.workflow.workflowTypeId = vm.workflowTypes[0].id;
                    
                    //get workflows for first folder in list
                    if (vm.workflowFolders.length > 0) {
                        GetWorkflows(vm.workflowFolders[0]['id']);
                        vm.folderId = vm.workflowFolders[0]['id'];
                    }

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
            function toShortDateTime(item) {
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
                    vm.action.Id = data.id;                
                    vm.SaveForms();                    
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
                vm.showModalActionCreate = false;               
            }
            function CancelWorkflow() {                
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
            }

            function GetForms() {
                vm.pagination.ActionTypeId = vm.action.ActionTypeId;
                return ajaxService.ajaxPost(vm.pagination, "api/formService/GetFormData").then(function (data) {
                  
                    $("#forms").html("<h4>Metadata</h4>");
                    for (var i = 0; i < data.formFields.length; i++) {
                        vm.showForms = true;
                        
                        var formField = data.formFields[i];
                        vm.formId = formField.formId;
                        var elType = formField.fieldType;
                        var ngModel = "vm.forms.f" + formField.formId + "." + formField.name.replace(" ","");
                        console.log(elType);
                        var element = "";
                        if (elType == "text" || elType == "date")
                            element = "<div class='row col-md-12'><div class='col-sm-3'> <label >" + formField.name + "</label><input class='form-control' type='" + elType + "' ng-model='" + ngModel + "' /></div></div>";
                        else if (elType == "textarea")
                            element = "<div class='row col-md-12'><div class='col-sm-6'> <label >" + formField.name + "</label><textarea class='form-control' rows='4' cols='50' ng-model='" + ngModel + "'></textarea></div></div>";
                        $("#forms").append($compile(element)($scope));
                    }
                    
                });
            }

            function SaveForms() {
                vm.formsData.ActionTypeId = vm.action.ActionTypeId;
                vm.formsData.Forms = eval("vm.forms.f" + vm.formId);
                vm.formsData.Id = vm.formId;
                vm.formsData.ContentTypeName = "action";
                vm.formsData.ContentId = vm.action.Id;
                return ajaxService.ajaxPost(vm.formsData, "api/formService/SaveForms").then(function (data) {
                    vm.showModalActionCreate = false;
                    GetWorkflowDataById(vm.action.WorkflowId);
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



