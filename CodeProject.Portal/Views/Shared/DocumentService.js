angular.module('app').service('documentService', ['$rootScope','ajaxService', '$http', function ($rootScope, ajaxService, $http) {

    var attachedFiles = [];
    var contentId = -1;
    var parentDocumentId = -1;

   this.getAttachedFiles = function (){
        return attachedFiles;
   }
   this.setAttachedFiles = function (files){
       attachedFiles = files;
   }
   this.setContentId = function (id) {
       contentId = id;
   }
   this.setParentDocumentId = function (id) {
       parentDocumentId = id;
   }
   this.pushFile = function (file) {
       
       attachedFiles.push(file);        
   }
   
   this.saveDocuments = function () {
       var data = { Documents: attachedFiles, ContentId: contentId, ParentDocumentId: parentDocumentId }
       attachedFiles = [];
        return ajaxService.ajaxPost(data, "api/documentService/UpdateDocumentsContentId").then(function (data) {
            $rootScope.$broadcast('documentsUpladed', '');
        });
   }

   this.downloadDocument = function (id, name) {
       $http({
           method: 'GET',
           cache: false,
           url: 'api/documentService/DownoadDocument',
           responseType: 'arraybuffer',
           headers: {
               'Content-Type': 'application/json; charset=utf-8',
               'id': id
           }
       }).success(function (data, status, headers, config) {

           var file = new Blob([data], {
               type: 'application/csv'
           });
           
           var fileURL = URL.createObjectURL(file);
           var a = document.createElement('a');
           a.href = fileURL;
           a.target = '_blank';
           a.download = name;
           document.body.appendChild(a);
           a.click();
       }).error(function (data, status) {
           
       });
   }
   
}]);
