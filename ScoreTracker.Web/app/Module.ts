/// <reference path="References.ts"/>

module Scoretracker {
    'use strict';

    var scoretracker = angular.module('scoretracker', ['ngRoute', 'LocalStorageModule'])
        .config([
            '$routeProvider', ($routeProvider: ng.route.IRouteProvider) => {
                $routeProvider
                    .when('/home', {
                        controller: 'homeController',
                        templateUrl: 'app/templates/home.html'
                    })
                    .when('/scoretracker', {
                        templateUrl: 'app/templates/scoretracker.html',
                        controller: 'testController'
                    })
                    .when('/login', {
                        controller: 'loginController',
                        templateUrl: 'app/templates/login.html'
                    })
                    .when('/signup', {
                        controller: 'signupController',
                        templateUrl: 'app/templates/signup.html'
                    })
                    .when('/refresh', {

                    })
                    .when('/resetpassword', {

                    })
                    .otherwise({
                        redirectTo: '/home'
                    });
            }
        ])
        .service('authService', Scoretracker.AuthService)
        .service('authInterceptorService', Scoretracker.AuthInterceptor)
        .controller('testController', Scoretracker.TestController)
        .controller('signupController', Scoretracker.SignupController)
        .controller('loginController', Scoretracker.LoginController)
        .controller('homeController', Scoretracker.HomeController)
        .service('groupsService', Scoretracker.GroupsService)
        .service('tableService', Scoretracker.TableService)
        .constant('scoretrackerAuthSettings', {
            apiServiceBaseUri: 'http://localhost:1337/',
            clientId: 'scoretrackerApp'
        })
        .config($httpProvider => {
            $httpProvider.interceptors.push('authInterceptorService');
        })
        .run(authService => {
            authService.fillAuthData();
        })
    ;
    
 
}
