///<reference path="../References.ts"/> 
module Scoretracker {
    'use strict';

    export class TestController {
        public static $inject = [
            '$scope', 'groupsService', 'tableService'
        ];

        private data: Group[];
        private tableService: TableService;
        private groupsService: GroupsService;
        private scope: any;
        

        constructor($scope, groupsService: GroupsService, tableService: TableService) {
            this.scope = $scope;
            $scope.vm = this;
            this.scope = $scope;
            this.tableService = tableService;
            this.groupsService = groupsService;

            this.buildContents();

            
            
        }

        buildContents() {
            this.groupsService.get((data) => {
                this.data = data;

                data.forEach(group => {
                    this.tableService.getTable(group.id, (data) => {
                        group.table = this.groupsService.createTable(group);
                    });
                    
                });

                this.scope.$watch(() => this.data, (oldValue, newValue) => {
                    if (oldValue !== newValue) {
                        data.forEach(group => {
                            group.table = this.groupsService.createTable(group);
                        });
                        console.log(newValue + ' ' + angular.isObject(newValue) + ' ' + typeof newValue);
                    }
                }, true);

                
                

            });
        }

        //getTable(groupId) : any {
            
        //    this.tableService.getTable(groupId, (data) => {
        //        return data;
        //        this.scope.$apply();
        //    });
        //}



    }
}
