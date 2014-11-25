/// <reference path="../References.ts"/> 
module Scoretracker {
    export class Group {
        id: number;
        name: string;
        teams: Team[];
        matches: Match[];
        table: TableItem[];
    }
}