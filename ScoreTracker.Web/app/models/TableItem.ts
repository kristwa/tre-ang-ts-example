 /// <reference path="../References.ts"/> 
module Scoretracker {
    export class TableItem {
        place: number;
        team: Team;

        games: number;
        goalsFor: number;
        goalsAgainst: number;
        won: number;
        drawn: number;
        lost: number;
        points: number;
    }
}