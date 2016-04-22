angular.module("app").register.controller('formController',
    ['$scope','$routeParams', '$location', 'ajaxService', 'alertService', 
    function ($scope,$routeParams, $location, ajaxService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.messageBox = "";
            vm.alerts = [];
            vm.forms = [];
            vm.form = {id:"0"};
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 1000
            };
            vm.paginationField = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 1000
            };
            vm.showModalCreate = false;
            vm.showModalUpdate = false;
            vm.showModalDelete = false;
            vm.showModalCreateField = false;
            vm.showModalUpdateField = false;
            vm.showModalDeleteField = false;
            vm.showModalInsertTable = false;
            vm.mySelectedItems = [];
            vm.form_ = {};
            vm.fieldTypes = [{ code: 'text', name: 'Text' }, { code: 'date', name: 'Date' }, { code: 'select', name: 'Select' }, { code: 'textarea', name: 'Text Area' }];
            vm.formField = { id: "0" };
            vm.formfield = {};
            vm.formFields = [];
            vm.mySelectedItemsField = [];
            vm.formName = {};

            //functions
            vm.getAllForms = getAllForms;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createForm = createForm;
            vm.updateForm = updateForm;
            vm.deleteForm = deleteForm;
            vm.toggleModal = toggleModal;
            vm.toggleModalCreateField = toggleModalCreateField;
            vm.toggleModalUpdateField = toggleModalUpdateField;
            vm.toggleModalDeleteField = toggleModalDeleteField;
            vm.createFormField = createFormField;
            vm.updateFormField = updateFormField;
            vm.deleteFormField = deleteFormField;
            vm.getFormFieldsForForm = getFormFieldsForForm;
            vm.toggleModalInsertTable = toggleModalInsertTable;
            vm.insertTable = insertTable;
         

            function init() {
                getAllForms();
            }

            init();

            vm.titles = [];

            $scope.$watch('vm.mySelectedItems[0]', function (value) {
                if(vm.mySelectedItems[0])
                {
                    getFormFieldsForForm(vm.mySelectedItems[0].id);
                    vm.formName = vm.mySelectedItems[0].name;
                }
                else {
                    vm.formName = "";
                }
            });

            function toggleModal()
            {
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
                vm.showModalCreateField = false;
                vm.showModalUpdateField = false;
                vm.showModalDeleteField = false;
                vm.showModalInsertTable = false;
            }

            function toggleModalCreate() {
                $scope.title = "Create Form";
                vm.form = getForm();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems) {
                    $scope.title = "Edit Form";
                    vm.form_ = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select Form!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems) {
                    $scope.title = "Delete Form";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Form!");
                }
            };

            function getAllForms() {
                return ajaxService.ajaxPost(vm.pagination, "api/formService/GetForms").then(function (data) {
                    vm.forms = data.forms;
                    return vm.forms;
                });
            }

            function createForm() {
                var form = new Object();
                form.Id = vm.form.id;
                form.Name = vm.form.name;
                form.TableName = vm.form.tableName;

                return ajaxService.ajaxPost(form, "api/formService/CreateForm").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    getAllForms();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateForm() {
                var form = new Object();
                form.Id = vm.form_.id;
                form.Name = vm.form_.name;
                form.TableName = vm.form_.tableName;
                return ajaxService.ajaxPost(form, "api/formService/UpdateForm").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    getAllForms();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            function deleteForm() {
                var form = new Object();
                form.Id = vm.mySelectedItems[0].id;
                form.Name = vm.mySelectedItems[0].name;
                form.TableName = vm.mySelectedItems[0].tableName;
                return ajaxService.ajaxPost(form, "api/formService/DeleteForm").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    getAllForms();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            //FORM FIELD
            function toggleModalCreateField() {
                $scope.title = "Create Form Field";
                vm.formField = getFormField();
                vm.showModalCreateField = !vm.showModalCreateField;
            };

            function toggleModalUpdateField() {
                if (vm.mySelectedItemsField[0]) {
                    $scope.title = "Edit Form Field";
                    vm.formfield = vm.mySelectedItemsField[0];
                    vm.showModalUpdateField = !vm.showModalUpdateField;
                }
                else {
                    alert("Please select Form Field!");
                }
            };

            function toggleModalDeleteField() {
                if (vm.mySelectedItemsField[0]) {
                    $scope.title = "Delete Form Field";
                    vm.showModalDeleteField = !vm.showModalDeleteField;
                }
                else {
                    alert("Please select Form Field!");
                }
            };

            function getFormFieldsForForm(formId) {
                vm.paginationField.formId = formId;
                return ajaxService.ajaxPost(vm.paginationField, "api/formFieldService/GetFormFieldsForForm").then(function (data) {
                    vm.formFields = data.formFields;
                    return vm.formFields;
                });
            }

            function createFormField() {
                var formField = new Object();
                formField.Id = vm.formField.id;
                formField.Name = vm.formField.name;
                formField.FormId = vm.mySelectedItems[0].id;
                formField.FieldType = vm.formField.fieldType;
                return ajaxService.ajaxPost(formField, "api/formFieldService/CreateFormField").then(function (data) {
                    vm.showModalCreateField = !vm.showModalCreateField;
                    getFormFieldsForForm(vm.mySelectedItems[0].id);
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            function updateFormField() {
                var formField = new Object();
                formField.Id = vm.formfield.id;
                formField.Name = vm.formfield.name;
                formField.FormId = vm.formfield.formId;
                formField.FieldType = vm.formfield.fieldType;
                return ajaxService.ajaxPost(formField, "api/formFieldService/UpdateFormField").then(function (data) {
                    vm.showModalUpdateField = !vm.showModalUpdateField;
                    getFormFieldsForForm(vm.mySelectedItems[0].id);
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

            function deleteFormField() {
                var formField = new Object();
                formField.Id = vm.mySelectedItemsField[0].id;
                formField.Name = vm.mySelectedItemsField[0].name;
                formField.FormId = vm.mySelectedItemsField[0].id;
                formField.FieldType = vm.mySelectedItemsField[0].fieldType;

                return ajaxService.ajaxPost(formField, "api/formFieldService/DeleteFormField").then(function (data) {
                    vm.showModalDeleteField = !vm.showModalDeleteField;
                    getFormFieldsForForm(vm.mySelectedItems[0].id);
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

            function getForm()
            {
                vm.form = { id: "0" };
                return vm.form;
            }

            function getFormField() {
                vm.formField = { id: "0" };
                return vm.formField;
            }

            function toggleModalInsertTable()
            {
                vm.showModalInsertTable = !vm.showModalInsertTable;
            }

            function insertTable()
            {
                var formField = new Object();
                formField.Id = vm.mySelectedItems[0].id;
                return ajaxService.ajaxPost(formField, "api/formService/CreateTable").then(function (data) {
                    vm.showModalInsertTable = !vm.showModalInsertTable;
                });
            }

        }

        }
    ]);
