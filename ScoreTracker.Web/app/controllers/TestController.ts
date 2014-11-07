///<reference path="../References.ts"/> 
module Scoretracker {
    'use strict';

    export class TestController {
        public static $inject = [
            '$scope', 'groupsService', 'tableService'
        ];

        private data: any[];
        //private tables: any[];
        private test: number = 42;
        private tableService: TableService;
        private groupsService: GroupsService;
        private scope: any;

        constructor($scope, groupsService: GroupsService, tableService: TableService) {
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
                        group.table = data;
                    });
                    
                });

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
