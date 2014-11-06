/// <reference path="../References.ts"/> 

module Scoretracker {
    
    export class TableService {
        private baseUrl: string = "http://localhost:1337/api/tables";
        private http: ng.IHttpService;

        constructor($http: ng.IHttpService) {
            this.http = $http;
        }

        getTable(successCallback: Function) {
            this.http.get(this.baseUrl).success((data, status) => {
                successCallback(data);
            }).error(error => {
                    successCallback(error);
                });

        }
    }    
}