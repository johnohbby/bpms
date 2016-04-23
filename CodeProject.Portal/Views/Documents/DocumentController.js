angular.module("app").register.controller('documentController',
    ['$scope','$routeParams', '$location', 'ajaxService', 'alertService', 'loginService', 'documentService',
function ($scope, $routeParams, $location, ajaxService, alertService, loginService, documentService) {

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
                pageSize: 1000,
                userId: loginService.getLoggedUser().id
            };
            vm.paginationD = {
                currentPageNumber: 1,
                sortExpression: "Id",
                sortDirection: "ASC",
                pageSize: 1000,
                contentTypeId: null,
                contentId: null
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
            vm.contentRights = [];
            vm.contentRightsShared = [];
            vm.showModalDeleteFolder = false;
            vm.folderToDelete = {};
            vm.documents = [];
            vm.parentDocuments = [];
            vm.documentVersions = [];
            vm.showModalCreateDocument = false;
            vm.extensions = [".txt", ".doc", ".docx", ".xls", ".csv", ".pdf"];
            vm.document = { id: "0" };
            vm.showModalCreateVersion = false;
            vm.documentVersion = {};
            vm.showModalDeleteDocument = false;

            //functions
            vm.toggleModalCreateFolder = toggleModalCreateFolder;
            vm.toggleModalShareFolder = toggleModalShareFolder;
            vm.createFolder = createFolder;
            vm.getUsersForFolderShare = getUsersForFolderShare;
            vm.getRightTypes = getRightTypes;
            vm.shareFolder = shareFolder;
            vm.getContentTypes = getContentTypes;
            vm.showShare = showShare;
            vm.getAllContentRights = getAllContentRights;
            vm.disableShareFolder = disableShareFolder;
            vm.toggleModalDeleteFolder = toggleModalDeleteFolder;
            vm.deleteFolder = deleteFolder;
            vm.getDocumentsForFolder = getDocumentsForFolder;
            vm.toggleModalCreateDocument = toggleModalCreateDocument;
            vm.saveDocument = saveDocument;
            vm.toggleModalCreateVersion = toggleModalCreateVersion;
            vm.saveVersion = saveVersion;
            vm.showDeleteVersion = showDeleteVersion;
            vm.deleteDocument = deleteDocument;
            vm.toggleModalDeleteDocument = toggleModalDeleteDocument;
            

            function init() {
                getRightTypes();
                getContentTypes();
            }

            init();


            $scope.$watch('vm.mySelectedItems[0]', function (value) {
                vm.documentVersions = [];
                if (vm.mySelectedItems[0])
                {
                    fillDocumentVersions();
                }
            });

            function fillDocumentVersions()
            {
                vm.documentVersions.push(vm.mySelectedItems[0]);
                for (var i = 0; i < vm.documents.length; i++) {
                    if (vm.documents[i].parentDocumentId === vm.mySelectedItems[0].id) {
                        vm.documentVersions.push(vm.documents[i]);
                    }
                }
            }

            function toggleModal()
            {
                vm.showModalCreateFolder = false;
                vm.showModalShareFolder = false;
                vm.showModalDeleteFolder = false;
                vm.showModalCreateDocument = false;
                vm.showModalCreateVersion = false;
                vm.showModalDeleteDocument = false;
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

            function toggleModalDeleteFolder(folder)
            {
                $scope.title = "Delete Folder";
                vm.folderToDelete = folder;
                vm.showModalDeleteFolder = !vm.showModalDeleteFolder;
            }

            function toggleModalCreateDocument() {
                $scope.title = "Create Document";
                vm.document = getDocument();
                vm.showModalCreateDocument = !vm.showModalCreateDocument;
            }

            function toggleModalCreateVersion() {
                $scope.title = "Create Version";
                vm.documentVersion = getDocumentVersion(); 

                vm.showModalCreateVersion = !vm.showModalCreateVersion;
            }

            function toggleModalDeleteDocument()
            {
                $scope.title = "Delete Document";
                vm.showModalDeleteDocument = !vm.showModalDeleteDocument;
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

            function deleteFolder() {
                var folder = new Object();
                folder.id = vm.folderToDelete.id;
                return ajaxService.ajaxPost(folder, "api/folderService/DeleteFolder").then(function (data) {
                    vm.showModalDeleteFolder = !vm.showModalDeleteFolder;
                    if (vm.parentDocuments.length>0)
                    {
                        vm.parentDocuments = [];
                        if(vm.documentVersions.length>0)
                        {
                            vm.documentVersions = [];
                        }
                        vm.mySelectedItems = [];
                    }
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
                    getAllContentRights();
                    return vm.rightTypes;
                });
            }

            function getAllContentRights()
            {
                return ajaxService.ajaxPost(vm.pagination, "api/contentRightService/GetContentRights").then(function (data) {
                   
                    for (var i = 0; i < data.contentRights.length; i++) {
                        if (data.contentRights[i].userId === vm.pagination.userId && data.contentRights[i].contentTypeId === vm.contentTypeId) {
                            vm.contentRightsShared.push(data.contentRights[i]);
                        }
                    }
                    vm.contentRights = data.contentRights;
                    getAllFoldersForUser();
                    return vm.contentRights;
                });
            }

            function showShare(folderId)
            {
                for (var i = 0; i < vm.contentRightsShared.length; i++)
                {
                    if (vm.contentRightsShared[i].contentId === folderId && vm.contentRightsShared[i].rightTypeId === vm.rightTypeIdRead)
                    { return false;}
                }

                return true;
               
            }

            function disableShareFolder(userId)
            {
                var contentRight = new Object();
                for (var i = 0; i < vm.contentRights.length; i++)
                {
                    if (vm.contentRights[i].userId === userId && vm.contentRights[i].contentId === vm.pagination.folderId && vm.contentRights[i].contentTypeId===vm.contentTypeId)
                    {
                        contentRight = vm.contentRights[i];
                        break;
                    }
                }
               
                return ajaxService.ajaxPost(contentRight, "api/contentRightService/DeleteContentRight").then(function (data) {
                    for (var i = 0; i < vm.sharedUsers.length; i++)
                    {
                        if(vm.sharedUsers[i].id===userId)
                        {
                            vm.sharedUsers.splice(i, 1);
                            break;
                        }
                    }
                })
              .catch(function (fallback) {
                  console.log(fallback);
              });
            }

            function getDocumentsForFolder(folderId)
            {
                vm.pagination.contentId = folderId;
                vm.pagination.contentTypeId = vm.contentTypeId;
               
                return ajaxService.ajaxPost(vm.pagination, "api/documentService/GetDocumentsForContent").then(function (data) {
                    vm.parentDocuments = [];
                    vm.documentVersions = [];
                    for (var i = 0; i < data.documents.length; i++) {
                        if (!data.documents[i].parentDocumentId || data.documents[i].parentDocumentId===-1)
                        {
                            vm.parentDocuments.push(data.documents[i]);
                        }
                    }
                    if (vm.parentDocuments.length === 1)
                    {
                        vm.mySelectedItems[0] = vm.parentDocuments[0];
                    }
                    vm.documents = data.documents;

                    return vm.documents;
                });
            }

            function getDocument() {
                vm.document = { id: "0" };
                return vm.document;
            }

            function saveDocument()
            {
                var document = new Object();
                document.ContentId = vm.pagination.contentId;
                document.ParentDocumentId = null;
            }

            function getDocumentVersion() {
                vm.documentVersion = {
                    id:"0"
            };
                return vm.documentVersion;
            }

            function saveVersion()
            {
                var documentVersion = new Object();
                documentVersion.ContentId = vm.pagination.contentId;
                documentVersion.ParentDocumentId = vm.mySelectedItems[0].id;
            }

            function showDeleteVersion(index)
            {
                
                if(index===vm.documentVersions.length-1)
                    {
                        return true;
                    }
                
                return false;
            }

            function deleteDocument()
            {
                var document = new Object();
                document.Id = vm.mySelectedItemsR[0].id;
                document.Name = vm.mySelectedItemsR[0].name;
                document.NameOnServer = vm.mySelectedItemsR[0].nameOnServer;
                document.Extension = vm.mySelectedItemsR[0].extension;
                document.ContentTypeId = vm.mySelectedItemsR[0].contentTypeId;
                document.ContentId = vm.mySelectedItemsR[0].contentId;
                document.ParentDocumentId = vm.mySelectedItemsR[0].parentDocumentId;
                document.Created = vm.mySelectedItemsR[0].created;

                return ajaxService.ajaxPost(document, "api/documentService/DeleteDocument").then(function (data) {
                    getDocumentsForFolder(vm.pagination.contentId);
                    vm.showModalDeleteDocument = !vm.showModalDeleteDocument;
                })
             .catch(function (fallback) {
                 console.log(fallback);
             });
            }
          
        }

        }
    ]);
