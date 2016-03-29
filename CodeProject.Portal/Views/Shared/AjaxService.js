angular.module('app').service('ajaxService', ['$http', 'blockUI', 'alertService', function ($http, blockUI, alertService) {

    "use strict";

    this.ajaxPost2 = function (data, route, successFunction, errorFunction) {

        blockUI.start();

        $http.post(route, data).success(function (response, status, headers, config) {
            blockUI.stop();
            successFunction(response, status);
        }).error(function (response) {
            blockUI.stop();
            errorFunction(response);
        });

    }

    this.ajaxPost = function (data, route) {

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