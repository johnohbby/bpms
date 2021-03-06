﻿
console.log("master controller");

angular.module('app').controller('masterController',
    ['$routeParams', '$location', 'ajaxService', 'applicationConfiguration', 'loginService', '$scope',
        function ($routeParams, $location, ajaxService, applicationConfiguration, loginService, $scope) {

            var vm = this;
            

            
            this.initializeController = function () {
                vm.applicationVersion = applicationConfiguration.version;
                vm.UserFullname = loginService.GetFullname();
                vm.IsUserLogged = false;
                vm.Logout = Logout;

                function Logout() {
                    loginService.Logout();
                    vm.UserFullname = "";
                    vm.IsUserLogged = false;
                }
            }
            $scope.$on('Fullname', function (event, arg) {
                vm.UserFullname = arg;
                vm.IsUserLogged = true;
            });

        }]);