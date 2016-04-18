
console.log("master controller");

angular.module('app').controller('masterController',
    ['$routeParams', '$location', 'ajaxService', 'loginService', 'applicationConfiguration',
        function ($routeParams, $location, ajaxService, applicationConfiguration, loginService) {

            var vm = this;
           

            this.initializeController = function () {
                vm.applicationVersion = applicationConfiguration.version;
                
            }

        }]);