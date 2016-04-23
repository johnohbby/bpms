angular.module('app').service('emailService', ['$http', 'blockUI', function ($http, blockUI) {

    var from = "bpmsemail1@gmail.com";

   this.sendEmail = function (data, route)
   {
       data.From = from;
       blockUI.start();
       return $http.post(route, data)
           .then(successFunction)
           .catch(errorFunction);

       function successFunction(response) {
           blockUI.stop();
           return response.data;
       }

       function errorFunction(error) {
           blockUI.stop();
           return error;
       }

   }
   
}]);
