"use strict";

var app = angular.module("BangazonAPI", ["ngRoute"]);



app.config(function($routeProvider) {
    $routeProvider.
        when("/product", {
            templateUrl: "partials/product.html",
            controller: "ProductCtrl"
        }).
         when("/", {
            templateUrl: "partials/customers.html",
            controller: 'CustomersCtrl'
        }).
        when("/order", {
            templateUrl: "partials/order.html",
            controller: 'OrderCtrl'
        }).

        otherwise("/")
        // way to make sure they don't go anywhere else.
});