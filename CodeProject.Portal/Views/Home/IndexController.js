

angular.module('app').register.controller('indexController', ['$routeParams', '$location', 'ajaxService','$cookieStore','loginService', function ($routeParams, $location, ajaxService,$cookieStore, loginService) {

    "use strict";

    var vm = this;
    
    vm.IsLogedIn = false;
    if (loginService.getLoggedUser() != -1 && loginService.getLoggedUser() != undefined)
        vm.IsLogedIn = true
    vm.Message = '';
    vm.Submitted = false;
    vm.IsFormValid = false;


    vm.LoginData = {
        Username: 'mkiselica',
        Password: '2121'
    };

   vm.Login = function () {
       
       vm.Submitted = true;
       
       ajaxService.ajaxPost(vm.LoginData, "api/UserService/GetUser").then(function (data) {
           
           if (data.id > 0)
           {
               loginService.setLoggedUser(data);
               vm.IsLogedIn = true;
           }
           

           
       });
    };
    var vm = this;

    this.initializeController = function () {
        vm.title = "Index Page";
    }

}]);


