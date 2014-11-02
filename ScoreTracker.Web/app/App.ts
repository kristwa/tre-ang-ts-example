/// <reference path="References.ts"/>

module Scoretracker {
    'use strict';

    var scoretracker = angular.module('scoretracker', [])
        .controller('testController', Scoretracker.TestController)
        .service('testService', Scoretracker.TestService)
    ;
    
}