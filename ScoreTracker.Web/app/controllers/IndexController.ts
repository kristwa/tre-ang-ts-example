 ///<reference path="../References.ts"/> 
module Scoretracker {
    'use strict';

    export class IndexController {
        private authService: AuthService;
        private $location: ng.ILocationService;
        private authentication: AuthenticationInfo;

        public static $inject = [
            '$scope', '$location', 'authService'
        ];

        constructor($scope, $location: ng.ILocationService, authService: AuthService) {
            $scope.vm = this;
            this.$location = $location;
            this.authService = authService;
            this.authentication = this.authService.authentication;
        }

        logOut() {
            this.authService.logOut();
            this.$location.path('/home');
        }

    }

} 