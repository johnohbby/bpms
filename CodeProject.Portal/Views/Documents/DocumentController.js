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
            vm.users = [];
            vm.rightTypes = [];
            vm.contentRight = { id: "0" };
            vm.contentTypeId = null;
            vm.sharedUsers = [];
            vm.rightTypeIdRead = null;

            //functions
            vm.toggleModalCreateFolder = toggleModalCreateFolder;
            vm.toggleModalShareFolder = toggleModalShareFolder;
            vm.createFolder = createFolder;
            vm.getUsersForFolderShare = getUsersForFolderShare;
            vm.getRightTypes = getRightTypes;
            vm.shareFolder = shareFolder;
            vm.getContentTypes = getContentTypes;
            vm.showShare = showShare;

            function init() {
                getRightTypes();
                getContentTypes();
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
                vm.sharedUsers = [];
                $scope.title = "Share Folder";
                vm.contentRight = getContentRight();
                vm.contentRight.contentId = folderId;
                vm.pagination.folderId = folderId;
                getUsersForFolderShare();
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

            function shareFolder()
            {
                var contentRight = new Object();
                contentRight.Id = vm.contentRight.id;
                contentRight.ContentId = vm.contentRight.contentId;
                contentRight.RightTypeId = vm.contentRight.rightTypeId;
                contentRight.GroupId = null;
                contentRight.UserId = vm.contentRight.userId;
                contentRight.ContentTypeId = vm.contentTypeId;

                return ajaxService.ajaxPost(contentRight, "api/contentRightService/CreateContentRight").then(function (data) {
                    vm.showModalShareFolder = !vm.showModalShareFolder;
                })
                .catch(function (fallback) {
                    console.log(fallback);
                });
            }

            function getUsersForFolderShare()
            {
                return ajaxService.ajaxPost(vm.pagination, "api/userService/GetUsersForFolderShare").then(function (data) {
                    if (data.users) {
                        vm.users = data.users;
                        getUsersSharedFolder();
                    }
                    return vm.users;
                });
            }

            //useri sa kojima je vec folder share-an
            function getUsersSharedFolder() {
                return ajaxService.ajaxPost(vm.pagination, "api/userService/GetUsersSharedFolder").then(function (data) {
                    for (var i = 0; i < data.users.length; i++)
                    {
                        if (data.users[i].id!==vm.pagination.userId)
                         vm.sharedUsers.push(data.users[i]);
                    }
                    
                    vm.showModalShareFolder = !vm.showModalShareFolder;
                    return vm.sharedUsers;
                });
            }
           
            function getFolder()
            {
                vm.folder = { id: "0"};
                return vm.folder;
            }

            function getContentRight() {
                vm.contentRight = { id: "0"};
                return vm.contentRight;
            }

            function getContentTypes()
            {
                return ajaxService.ajaxPost(vm.pagination, "api/contentRightService/GetContentTypes").then(function (data) {
                    vm.contentTypes = data.contentTypes;
                    for (var i = 0; i < vm.contentTypes.length; i++)
                    {
                        if(vm.contentTypes[i].code==='FOLDER')
                        {
                            vm.contentTypeId = vm.contentTypes[i].id;
                        }
                    }
                    return vm.contentTypes;
                });
            }


            function getRightTypes()
            {
                return ajaxService.ajaxPost(vm.pagination, "api/rightTypeService/GetRightTypes").then(function (data) {
                    vm.rightTypes = data.rightTypes;
                    for (var i = 0; i < vm.rightTypes.length; i++)
                    {
                        if(vm.rightTypes[i].code==='READ')
                        {
                            vm.rightTypeIdRead = vm.rightTypes[i].id;
                            break;
                        }
                    }
                    return vm.rightTypes;
                });
            }

            function showShare(folderId)
            {

               //to do
            }

          
        }

        }
    ]);
