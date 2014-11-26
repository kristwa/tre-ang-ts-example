module Scoretracker {
    export class AuthInterceptor {
        public static $inject = [
            '$q', '$injector', '$location', 'localStorageService'
        ];

        private $q: ng.IQService;
        private $injector;
        private $location: ng.ILocationService;
        private localStorageService: ng.localStorage.ILocalStorageService;

        constructor($q: ng.IQService, $injector, $location: ng.ILocationService, localStorageService: ng.localStorage.ILocalStorageService) {
            this.$q = $q;
            this.$injector = $injector;
            this.$location = $location;
            this.localStorageService = localStorageService;
        }

        request = (config) => {
            config.headers = config.headers || {};

            var authData = this.localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        responseError = (rejection) => {
            var deferred = this.$q.defer();

            if (rejection.status === 401) {
                var authService: AuthService = this.$injector.get('authService');

                authService.refreshToken().then(response => {
                    this.retryHttpRequest(rejection.config, deferred);
                }, () => {
                    authService.logOut();
                    this.$location.path("/login");
                    deferred.reject(rejection);
                });

            } else {
                deferred.reject(rejection);
            }

            return deferred.promise;

        }

        retryHttpRequest = (config, deferred) => {
            var http: ng.IHttpService = this.$injector.get('$http');

            http(config).then(response => {
                deferred.resolve(response);
            }, response => {
                deferred.reject(response);
            });
        }
    } 
} 