

angular.module('app').register.controller('indexController', ['$routeParams', '$location', 'ajaxService', function ($routeParams, $location, ajaxService) {

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

           alert(1);
           $cookieStore.put('logged-in', "eca");
           alert($cookieStore.get('logged-in'));
           
       });
    };
    var vm = this;

    this.initializeController = function () {
        vm.title = "Index Page";
    }

}]);


