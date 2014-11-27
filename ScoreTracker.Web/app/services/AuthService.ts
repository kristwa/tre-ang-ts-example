///<reference path="../References.ts"/>

module Scoretracker {
     export class AuthService {
         public static $inject = [
             '$http', '$q', 'localStorageService', 'scoretrackerAuthSettings'
         ];

         private $http: ng.IHttpService;
         private $q: ng.IQService;
         private localStorageService: ng.localStorage.ILocalStorageService;
         private scoretrackerAuthSettings: any;
         public authentication: AuthenticationInfo;

         constructor($http: ng.IHttpService, $q: ng.IQService, localStorageService: ng.localStorage.ILocalStorageService, scoretrackerAuthSettings) {
             this.$http = $http;
             this.$q = $q;
             this.localStorageService = localStorageService;
             this.scoretrackerAuthSettings = scoretrackerAuthSettings;

             this.authentication = new AuthenticationInfo();
             this.authentication.isAuth = false;
             this.authentication.userName = "";
             this.authentication.useRefreshTokens = false;
         }

         saveRegistration(registration) {

             this.logOut();

             return this.$http.post(this.scoretrackerAuthSettings.apiServiceBaseUri + 'api/account/register', registration).then(response => {
                 return response;
             });

         }

         login(loginData) {
             
             var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password; 

             if (loginData.useRefreshTokens) {
                 data = data + "&client_id=" + this.scoretrackerAuthSettings.clientId;
             }

             var deferred = this.$q.defer();

             this.$http.post<any>(this.scoretrackerAuthSettings.apiServiceBaseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success((response) => {

                 if (loginData.useRefreshTokens) {
                     this.localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true });
                 }
                 else {
                     this.localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false });
                 }

                 this.authentication.isAuth = true;
                 this.authentication.userName = loginData.userName;
                 this.authentication.useRefreshTokens = loginData.useRefreshTokens;

                 deferred.resolve(response);

             }).error((err, status) => {
                this.logOut();
                deferred.reject(err);
             });

             return deferred.promise;

         }

         logOut() {
             this.localStorageService.remove('authorizationData');

             this.authentication.isAuth = false;
             this.authentication.userName = "";
             this.authentication.useRefreshTokens = false;
         }

         newPassword(newPasswordText: string) {
             return this.$http.post(this.scoretrackerAuthSettings.apiServiceBaseUri + '/api/account/resetpassword', '"' + newPasswordText + '"').then((response) => {
                 return response;
             });
         }

         fillAuthData() {
             var authData = this.localStorageService.get('authorizationData');
             if (authData) {
                 this.authentication.isAuth = true;
                 this.authentication.userName = authData.userName;
                 this.authentication.useRefreshTokens = authData.useRefreshTokens;
             }
         }

         refreshToken() {
             var deferred = this.$q.defer();

             var authData = this.localStorageService.get('authorizationData');

             if (authData && authData.useRefreshTokens) {

                 var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + this.scoretrackerAuthSettings.clientId;

                 this.localStorageService.remove('authorizationData');

                 this.$http.post<any>(this.scoretrackerAuthSettings.apiServiceBaseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                     .success(response => {

                         this.localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });

                         deferred.resolve(response);

                     })
                     .error(function(err, status) {
                         this.logOut();
                         deferred.reject(err);
                     });

             } else {
                 deferred.reject();
             }

             return deferred.promise;
         }

        
     }
 }