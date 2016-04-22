angular.module('app').service('documentService', [ function () {

   var attachedFiles= [];

   this.getAttachedFiles = function ()
   {
        return attachedFiles;
   }

   this.setAttachedFiles = function (files){
       attachedFiles = files;
    }
    this.pushFile = function (file){
        attachedFiles.push(file);
        //$("#attachedFiles").html("lalala");
    }
   this.saveDocuments = function(){alert(1);}
   
}]);
