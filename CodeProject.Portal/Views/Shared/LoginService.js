angular.module('app').service('loginService', [ function () {

   var loggedUser= {};

   this.getLoggedUser = function ()
   {
        return loggedUser;
   }

   this.setLoggedUser = function (user)
   {

       loggedUser = user;
    }
   
}]);
