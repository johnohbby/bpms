

angular.module('app').register.controller('indexController', ['$routeParams', '$location', 'ajaxService','$cookieStore','loginService', function ($routeParams, $location, ajaxService,$cookieStore, loginService) {

    "use strict";
    var vm = this;
    vm.IsLogedIn = false;
    vm.Message = '';
    vm.Submitted = false;
    vm.IsFormValid = false;


    vm.LoginData = {
        Username: '1',
        Password: '1'
    };

   vm.Login = function () {
       
       vm.Submitted = true;
       
       ajaxService.ajaxPost(vm.LoginData, "api/workflowTypeService/LoginTest").then(function (data) {
           vm.IsLogedIn = true;

           loginService.setLoggedUser('blabla');
           alert(loginService.getLoggedUser());
           
       });
    };
    var vm = this;

    this.initializeController = function () {
        vm.title = "Index Page";
    }

}]);


