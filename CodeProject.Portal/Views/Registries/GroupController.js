angular.module("app").register.controller('groupController',
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
            vm.groups = [];
            vm.group = { id: "0" };
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
            vm.group_ = {};
            vm.loggedUser = "";
            vm.groupTypes = [];

            //functions
            vm.getAllGroups = getAllGroups;
            vm.toggleModalCreate = toggleModalCreate;
            vm.toggleModalUpdate = toggleModalUpdate;
            vm.toggleModalDelete = toggleModalDelete;
            vm.createGroup = createGroup;
            vm.updateGroup = updateGroup;
            vm.deleteGroup = deleteGroup;
            vm.toggleModal = toggleModal;
            vm.getAllGroupTypes = getAllGroupTypes;


            function init() {
                getAllGroupTypes();
                getAllGroups();
                
            }

            init();

            vm.titles = [];

            function toggleModal() {
                vm.showModalCreate = false;
                vm.showModalUpdate = false;
                vm.showModalDelete = false;
            }

            function toggleModalCreate() {
                $scope.title = "Create Group";
                vm.group = getGroup();
                vm.showModalCreate = !vm.showModalCreate;
            };

            function toggleModalUpdate() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Edit Group";
                    vm.group_ = vm.mySelectedItems[0];
                    vm.showModalUpdate = !vm.showModalUpdate;
                }
                else {
                    alert("Please select Group!");
                }
            };

            function toggleModalDelete() {
                if (vm.mySelectedItems[0]) {
                    $scope.title = "Delete Group";
                    vm.showModalDelete = !vm.showModalDelete;
                }
                else {
                    alert("Please select Group!");
                }
            };

            function getAllGroups() {
                return ajaxService.ajaxPost(vm.pagination, "api/groupService/GetGroups").then(function (data) {
                    for (var i = 0; i < data.groups.length; i++)
                    {
                        for(var j=0; j<vm.groupTypes.length; j++)
                        {
                            if(data.groups[i].groupTypeId===vm.groupTypes[j].id)
                            {
                                data.groups[i].groupTypeName = vm.groupTypes[j].name;
                            }
                        }
                    }
                    vm.groups = data.groups;
                    return vm.groups;
                });
            }

            function createGroup() {
                var group = new Object();
                group.Id = vm.group.id;
                group.Name = vm.group.name;
                group.Email = vm.group.email;
                group.GroupTypeId = vm.group.groupTypeId;

                return ajaxService.ajaxPost(group, "api/groupService/CreateGroup").then(function (data) {
                    vm.showModalCreate = !vm.showModalCreate;
                    vm.getAllGroups();

                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function updateGroup() {
                var group = new Object();
                group.Id = vm.group_.id;
                group.Name = vm.group_.name;
                group.Email = vm.group_.email;
                group.GroupTypeId = vm.group_.groupTypeId;
                return ajaxService.ajaxPost(group, "api/groupService/UpdateGroup").then(function (data) {
                    vm.showModalUpdate = !vm.showModalUpdate;
                    vm.getAllGroups();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteGroup() {
                var group = new Object();
                group.Id = vm.mySelectedItems[0].id;
                group.Name = vm.mySelectedItems[0].name;
                group.Email = vm.mySelectedItems[0].email;
                group.GroupTypeId = vm.mySelectedItems[0].groupTypeId;

                return ajaxService.ajaxPost(group, "api/groupService/DeleteGroup").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    vm.getAllGroups();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function getGroup() {
                vm.group = { id: "0" };
                return vm.group;
            }

            function getAllGroupTypes()
            {
                return ajaxService.ajaxPost(vm.pagination, "api/groupService/GetGroupTypes").then(function (data) {
                    vm.groupTypes = data.groupTypes;
                    return vm.groupTypes;
                });
            }

            function toggleModal() {
                vm.showModal != vm.showModal;
            }
        }

    }
    ]);
