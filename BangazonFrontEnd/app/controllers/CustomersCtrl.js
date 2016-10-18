"use strict";


app.controller("CustomersCtrl", function ($scope, $location, ApiCallFactory) {

$scope.CustomerInfo = {
  FirstName: "",
  LastName: "",
  CustomerId: "",
  DateCreated: ""

};



    $scope.loadAllCustomers = function() {
        console.log("loadAllCustomers has started!")
        ApiCallFactory.getCustomers()
          .then(function(data){
            console.log("it worked!")
            console.log("data", data);
          })
    }
})