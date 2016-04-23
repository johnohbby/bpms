angular.module('app').service('loginService', ['$cookies', '$rootScope','$location', function ($cookies, $rootScope,$location) {

   this.getLoggedUser = function ()
   {
        return $cookies.get("Id");
   }
   this.GetFullname = function()
   {
       var name = ""
       var lastname = ""
       if ($cookies.get("Name") != undefined)
           name = $cookies.get("Name");
       if ($cookies.get("Lastname") != undefined)
           lastname = $cookies.get("Lastname");
       var Fullname = name + " " + lastname;
       return Fullname;
   }
    
   this.setLoggedUser = function (user)
   {
       $cookies.put("Id", user.id, true);
       $cookies.put("Name", user.name, true);
       $cookies.put("Lastname", user.surname, true);
       $rootScope.$broadcast('Fullname', this.GetFullname());
       loggedUser = user.id;   
       $location.url('/Workflow/Workflow');
   }
   this.Logout = function(){
        
       $cookies.put("Id", -1, true);
       
   }
   
}]);
