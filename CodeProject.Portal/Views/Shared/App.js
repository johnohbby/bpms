﻿(function () {
angular
    .module('app', 
        ['ngRoute', 
         'ui.bootstrap', 
         'ngSanitize', 
         'blockUI',
         'trNgGrid',
         'ngCookies'
        ])
    .config(['$controllerProvider', '$provide', function ($controllerProvider, $provide) {
        angular
        .module('app').register =
      {
          controller: $controllerProvider.register,
          service: $provide.service
      };
}]);
})();





