///<reference path="../References.ts"/> 
module Scoretracker {
    'use strict';

    export class SignupController {
        
        private $scope;
        private $location: ng.ILocationService;
        private $timeout: ng.ITimeoutService;
        private authService: AuthService;
        private savedSuccessfully: boolean;
        private message: string;
        private registration: any;

        public static $inject = [
            '$scope', '$location', '$timeout', 'authService'
        ];

        constructor($scope, $location: ng.ILocationService, $timeout: ng.ITimeoutService, authService: AuthService) {
            this.$scope = $scope;
            $scope.vm = this;
            this.$location = $location;
            this.$timeout = $timeout;
            this.authService = authService;

            this.message = "";

            this.registration = {
                userName: "",
                password: "",
                confirmPassword: ""
            };
        }

        signUp() {
            this.authService.saveRegistration(this.registration).then(response => {
                this.savedSuccessfully = true;
                this.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                this.startTimer();
            }, response => {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
                this.message = "Failed to register user due to:" + errors.join(' ');
            });
        }

        startTimer() {
            var timer = this.$timeout(() => {
                this.$timeout.cancel(timer);
                this.$location.path("/login");
            }, 500);
        }

    }
} 