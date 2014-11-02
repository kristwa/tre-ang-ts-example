///<reference path="../References.ts"/> 
module Scoretracker {
    'use strict';

    export class TestController {
        public static $inject = [
            '$scope', 'testService'
        ];

        private message: string = "Fette eefett!";
        private test: number = 42;
        //private testService: TestService;

        constructor($scope, testService: TestService) {
            testService.get((data) => {
                this.message = data;
            });
            $scope.vm = this;
        }


    }
}
