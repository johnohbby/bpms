angular.module("app").register.controller('documentController',
    ['$scope','$routeParams', '$location', 'ajaxService', 'alertService', 'loginService', 
function ($scope,$routeParams, $location, ajaxService, alertService, loginService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            //variables
            vm.folders = [];
            vm.folder = { id: "0"};
           
            vm.pagination = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 15,
                userId: loginService.getLoggedUser().id
            };
            vm.paginationD = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 15
            };
           
            vm.mySelectedItems = [];
            vm.mySelectedItemsR = [];
            vm.showModalCreateFolder = false;
            vm.showModalShareFolder = false;
            

            //functions
            vm.toggleModalCreateFolder = toggleModalCreateFolder;
            vm.toggleModalShareFolder = toggleModalShareFolder;
            vm.createFolder = createFolder;

            function init() {
                getAllFoldersForUser();
            }

            init();


            $scope.$watch('vm.mySelectedItems[0]', function (value) {
                if(vm.mySelectedItems[0])
                {
                    
                }
                else {
                    
                }
            });

            function toggleModal()
            {
                vm.showModalCreateFolder = false;
                vm.showModalShareFolder = false;
            }

            function toggleModalCreateFolder()
            {
                $scope.title = "Create Folder";
                vm.folder = getFolder();
                vm.showModalCreateFolder = !vm.showModalCreateFolder;
            }

            function toggleModalShareFolder(folderId) {
                $scope.title = "Share Folder";
                vm.contentRight = getContentRight();
                vm.contentRight.contentId = folderId;
                vm.showModalShareFolder = !vm.showModalShareFolder;
            }

            function getParentFolder(folderId)
            {
                var parentFolder = new Object();
                return null;
            }

           
            function getAllFoldersForUser() {
                return ajaxService.ajaxPost(vm.pagination, "api/folderService/GetFoldersForUser").then(function (data) {
                    vm.folders = data.folders;
                    return vm.folders;
                });
            }

            function createFolder() {
                var folder = new Object();
                folder.Id = vm.folder.id;
                folder.Name = vm.folder.name;
                folder.ParentFolderId = getParentFolder(vm.folder.id);
                folder.UserId = vm.pagination.userId;

                return ajaxService.ajaxPost(folder, "api/folderService/CreateFolder").then(function (data) {
                    vm.showModalCreateFolder = !vm.showModalCreateFolder;
                    getAllFoldersForUser();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function deleteFolder(id) {
                var folder = new Object();
                folder.id = id;
                return ajaxService.ajaxPost(folder, "api/folderService/DeleteFolder").then(function (data) {
                    vm.showModalDelete = !vm.showModalDelete;
                    getAllFoldersForUser();
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            };

           
          
           
            function getFolder()
            {
                vm.folder = { id: "0"};
                return vm.folder;
            }

            function getContentRight() {
                vm.contentRight = { id: "0"};
                return vm.contentRight;
            }

          
        }

        }
    ]);
