angular.module("app").register.controller('userController',
    ['$scope', '$routeParams', '$location', 'ajaxService', 'alertService', 'loginService',
    function ($scope, $routeParams, $location, ajaxService, alertService, loginService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.messageBox = "";
            vm.alerts = [];
            vm.users = [];
            vm.user = { id: "0" };
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
            vm.user_ = {};
            vm.loggedUser = "";

            //functions
            vm.getAllUsers = getAllUsers;
            vm.closeAlert = closeAlert;
            vm.clearValidationErrors = clearValidationErrors;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createUser = createUser;
            vm.updateUser = updateUser;
            vm.deleteUser = deleteUser;
            vm.toggleModal = toggleModal;


            function init() {
                getAllUsers();
            }

            init();

            vm.titles = [];

            function toggleModal() {
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
            }

            function toggleModalCreate() {
                $scope.title = "Create User";
                vm.user = getUser();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Edit User";
                    vm.user_ = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select User!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Delete User";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select User!");
                }
            };

            function getAllUsers() {
                return ajaxService.ajaxPost(vm.pagination, "api/userService/GetUsers").then(function (data) {
                    vm.users = data.users;
                    return vm.users;
                });
            }

            function createUser() {
                var user = new Object();
                user.Id = vm.user.id;
                user.Name = vm.user.name;
                user.Surname = vm.user.surname;
                user.Username = vm.user.username;
                user.Password = vm.user.password;
                user.Email = vm.user.email;
                user.IsActive = vm.user.isActive;
                user.Created = new Date();
                user.CreatedBy = loginService.getLoggedUser().username;
                return ajaxService.ajaxPost(user, "api/userService/CreateUser").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    vm.getAllUsers();

                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateUser() {
                var user = new Object();
                user.Id = vm.user_.id;
                user.Name = vm.user_.name;
                user.Surname = vm.user_.surname;
                user.Username = vm.user_.username;
                user.Password = vm.user_.password;
                user.Email = vm.user_.email;
                user.IsActive = vm.user_.isActive;
                user.Created = vm.user_.created;
                user.CreatedBy = vm.user_.createdBy;
                return ajaxService.ajaxPost(user, "api/userService/UpdateUser").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    vm.getAllUsers();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteUser() {
                var user = new Object();
                user.Id = vm.mySelectedItems[0].id;
                user.Name = vm.mySelectedItems[0].name;
                user.Surname = vm.mySelectedItems[0].surname;
                user.Username = vm.mySelectedItems[0].username;
                user.Password = vm.mySelectedItems[0].password;
                user.Email = vm.mySelectedItems[0].email;
                user.IsActive = vm.mySelectedItems[0].isActive;
                user.Created = vm.mySelectedItems[0].created;
                user.CreatedBy = vm.mySelectedItems[0].createdBy;

                return ajaxService.ajaxPost(user, "api/userService/DeleteUser").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    vm.getAllUsers();
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

            function getUser() {
                vm.user = { id: "0" };
                return vm.user;
            }

            function toggleModal() {
                vm.showModal != vm.showModal;
            }
        }

    }
    ]);
