"use strict";

// added to .gitIgnore until we get this to work.

app.factory("ApiCallFactory", ($q, $http, $location, $routeParams) => {


let getCustomers = () => {
    return $q ((resolve, reject) => {
      $http.get(`http://localhost:5000/customers`)
      .success((itemObject) => {
        resolve(itemObject)
      })
      .error((error) => {
        reject(error)
        console.log("error", error)
      })
    })
    };

  let getSingleCustomer = (customerId) => {
    return $q ((resolve, reject) => {
      $http.get(`http://localhost:5000/customers${customerId}`)
      .success((itemObject) => {
        resolve(itemObject);
      })
      .error((error) => {
        reject(error);
        console.log("error", error)
      });
    });
    };

let postCustomer  = (customerInfo) => {
  console.log("postPokeRoute is running", customerInfo)
    return $q((resolve, reject) => {
       $http.post(`http://localhost:5000/customers`,
        JSON.stringify(customerInfo))
        .success((ObjFromMyServer) => {
          console.log(ObjFromMyServer);
          // currentRouteID = ObjFromFirebase.name;
          // console.log("I'M THE FIREBASE ID I CAN HELP YOU", currentRouteID)
          resolve(ObjFromMyServer);
        })
        .error ((error) => {
          reject(error);
    })
    })

  }

  let putCustomer = (customerInfo, customerId) => {
  console.log("putCustomer is running", customerId)
    return $q((resolve, reject) => {
       $http.put(`http://localhost:5000/customers/${customerId}`,
        JSON.stringify(customerInfo))
        .success((ObjFromMyServer) => {
          resolve(ObjFromMyServer);
        })
        .error ((error) => {
          reject(error);
    })
    })


}
    let deleteCustomer = (customerId) => {
    console.log("deleteCustomer is running", customerId)
    return $q((resolve, reject) => {
    $http.delete(`http://localhost:5000/customers/${customerId}`)
    .success((ObjFromMyServer) => {
      console.log("this is the ObjFromMyServer", ObjFromMyServer)
    resolve(ObjFromMyServer);
    })
    })
  }

return {putCustomer, deleteCustomer, postCustomer, getCustomers, getSingleCustomer}
})
