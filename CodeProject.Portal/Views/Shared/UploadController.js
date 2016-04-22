angular.module("app").controller('uploadController',['$scope', '$http', '$timeout', '$upload', 'documentService', function ($scope, $http, $timeout, $upload, documentService) {
        
        var up = this;
        
        //alert(a);
        this.initializeController = function (contentTypeName) {
        up.upload = [];
        up.fileUploadObj = { testString1: contentTypeName, testString2: "Test string 2" };
        up.attachedFiles = [];
        up.contentTypeName = contentTypeName;
        up.onFileSelect = onFileSelect;

        function onFileSelect ($files) {
            
            //$files: an array of files selected, each file has name, size, and type.
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    up.upload[index] = $upload.upload({
                        url: "api/uploadService/Upload", // webapi url
                        method: "POST",
                        data: { fileUploadObj: up.fileUploadObj, contentTypeName: up.contentTypeName },
                        file: $file
                    }).progress(function (evt) {
                        // get upload percentage
                        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data, status, headers, config) {
                        
                        // file is uploaded successfully

                        console.log(data.document);
                        //documentService.setAttachedFiles(data);
                        documentService.pushFile(data.document);
                        up.attachedFiles.push(data.document);
                        console.log(up.attachedFiles);
                    }).error(function (data, status, headers, config) {
                        // file failed to upload
                        console.log(data);
                    });
                })(i);
            }
        }

        up.abortUpload = function (index) {
            up.upload[index].abort();
        }
        }
        
        
    }]);
