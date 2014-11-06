///<reference path="../References.ts"/> 
module Scoretracker {
    'use strict';

    export class TestController {
        public static $inject = [
            '$scope', 'testService', 'tableService'
        ];

        private data: any;
        private table: any;
        private test: number = 42;
        private tableService: TableService;
        //private testService: TestService;

        constructor($scope, testService: TestService, tableService: TableService) {
            this.tableService = tableService;
            testService.get((data) => {
                this.data = data;
            });
            $scope.vm = this;
        }

        getTable() {
            this.tableService.getTable((data) => {
                this.table = data;
            });
        }


    }
}
