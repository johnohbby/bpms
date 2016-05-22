angular.module("app").register.controller('userGroupController',
    ['$scope', '$routeParams', '$location', 'ajaxService', 'alertService', 'loginService',
    function ($scope, $routeParams, $location, ajaxService, alertService, loginService) {

        "use strict";

        var vm = this;
        var id = $location.search().id;

        this.initializeController = function () {

            if (loginService.getLoggedUser() == -1 || loginService.getLoggedUser() === "undefined")
                return;
            loginService.broadcastFullname();

            //variables
            vm.userGroups = [];
            vm.userGroup = { id: "0" };
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
            vm.usergroup = {};
            vm.loggedUser = "";
            vm.groups = [];
            vm.users = [];

            //functions
            vm.getAllUserGroups = getAllUserGroups;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createUserGroup = createUserGroup;
            vm.updateUserGroup = updateUserGroup;
            vm.deleteUserGroup = deleteUserGroup;
            vm.toggleModal = toggleModal;
            vm.getAllUsers = getAllUsers;
            vm.getAllGroups = getAllGroups;


            function init() {
                getAllUsers();
                getAllGroups();
                getAllUserGroups();
                
            }

            init();

            vm.titles = [];

            function toggleModal() {
                vm.showModal != vm.showModal;
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
            }

            function toggleModalCreate() {
                $scope.title = "Create User Group";
                vm.userGroup = getUserGroup();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Edit User Group";
                    vm.usergroup = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select User Group!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems) {
                    $scope.title = "Delete User Group";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select User Group!");
                }
            };

            function getAllUserGroups() {
                return ajaxService.ajaxPost(vm.pagination, "api/userGroupService/GetUserGroups").then(function (data) {
                    for (var i = 0; i < data.userGroups.length; i++)
                    {
                        for(var j=0; j<vm.groups.length; j++)
                        {
                            if (data.userGroups[i].groupId === vm.groups[j].id)
                            {
                                data.userGroups[i].groupName = vm.groups[j].name;
                            }
                        }

                        for (var k = 0; k < vm.users.length; k++) {
                            if (data.userGroups[i].userId === vm.users[k].id) {
                                data.userGroups[i].userName = vm.users[k].name+ ' ' + vm.users[k].surname;
                            }
                        }
                    }
                    vm.userGroups = data.userGroups;
                    return vm.userGroups;
                });
            }

            function createUserGroup() {
                var userGroup = new Object();
                userGroup.Id = vm.userGroup.id;
                userGroup.UserId = vm.userGroup.userId;
                userGroup.GroupId = vm.userGroup.groupId;

                return ajaxService.ajaxPost(userGroup, "api/userGroupService/CreateUserGroup").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    vm.getAllUserGroups();

                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateUserGroup() {
                var userGroup = new Object();
                userGroup.Id = vm.usergroup.id;
                userGroup.UserId = vm.usergroup.userId;
                userGroup.GroupId = vm.usergroup.groupId;

                return ajaxService.ajaxPost(userGroup, "api/userGroupService/UpdateUserGroup").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    vm.getAllUserGroups();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteUserGroup() {
                if (vm.mySelectedItems[0]) {
                    var userGroup = new Object();
                    userGroup.Id = vm.mySelectedItems[0].id;
                    userGroup.UserId = vm.mySelectedItems[0].userId;
                    userGroup.GroupId = vm.mySelectedItems[0].groupId;

                    return ajaxService.ajaxPost(userGroup, "api/userGroupService/DeleteUserGroup").then(function (data) {
                        vm.showModalDelete = !vm.showModalDelete;
                        vm.getAllUserGroups();
                    })
                    .catch(function (fallback) {
                        console.log(fallback);
                    });
                }
            }

            function getUserGroup() {
                vm.userGroup = { id: "0" };
                return vm.userGroup;
            }

            function getAllGroups()
            {
                return ajaxService.ajaxPost(vm.pagination, "api/groupService/GetGroups").then(function (data) {
                    vm.groups = data.groups;
                    return vm.groups;
                });
            }

            function getAllUsers() {
                return ajaxService.ajaxPost(vm.pagination, "api/userService/GetUsers").then(function (data) {
                    vm.users = data.users;
                    return vm.users;
                });
            }

        }

    }
    ]);
