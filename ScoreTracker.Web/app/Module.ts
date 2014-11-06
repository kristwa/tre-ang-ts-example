/// <reference path="References.ts"/>

module Scoretracker {
    'use strict';

    var scoretracker = angular.module('scoretracker', ['ngRoute'])
        .config(['$routeProvider', ($routeProvider: ng.route.IRouteProvider) => {
                $routeProvider.when('/test', {
                        templateUrl: 'app/templates/test.html',
                        controller: 'testController'
                    })
                    .otherwise({
                        redirectTo: '/test'
                    });
            }])
        .controller('testController', Scoretracker.TestController)
        .service('testService', Scoretracker.TestService)
        .service('tableService', Scoretracker.TableService)
        
    ;
    
 
}
