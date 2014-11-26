 ///<reference path="../References.ts"/> 
module Scoretracker {
    'use strict';

    export class LoginController {
        private $scope;
        private $location: ng.ILocationService;
        private authService: AuthService;
        private message: string;
        private loginData: any;

        public static $inject = [
            '$scope', '$location', 'authService'
        ];

        constructor($scope, $location: ng.ILocationService, authService: AuthService) {
            this.$scope = $scope;
            $scope.vm = this;
            this.$location = $location;
            this.authService = authService;
        }

        login() {
            this.authService.login(this.loginData).then(() => {
                this.$location.path("/scoretracker");
            }, err => {
                this.message = err.error_description;
            });
        }


    }
}