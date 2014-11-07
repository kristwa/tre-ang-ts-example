///<reference path="../References.ts"/>

module Scoretracker {
    export class GroupsService {

        private baseUrl: string = "http://localhost:1337/api/groups";
        private http: ng.IHttpService;

        constructor($http: ng.IHttpService) {
            this.http = $http;
        }

        get(successCallback: Function) {
            this.http.get(this.baseUrl).success((data, status) => {
                successCallback(data);
            }).error(error => {
                successCallback(error);
            });
            
        }
    }
}