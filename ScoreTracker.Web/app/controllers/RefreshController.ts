 ///<reference path="../References.ts"/> 
module Scoretracker {
    'use strict';

    export class RefreshController {
        private authentication: AuthenticationInfo;
        private $location: ng.ILocationService;
        private authService: AuthService;
        private tokenRefreshed: boolean;
        private tokenResponse: any;

        public static $inject = [
            '$scope', '$location', 'authService'
        ];

        constructor($scope, $location: ng.ILocationService, authService: AuthService) {
            $scope.vm = this;
            this.$location = $location;
            this.authService = authService;
            this.authentication = authService.authentication;
        }

        refreshToken() {
            this.authService.refreshToken().then(response => {
                this.tokenRefreshed = true;
                this.tokenResponse = response;
            }, err => {
                this.$location.path("/login");
            });
        }
    }
} 