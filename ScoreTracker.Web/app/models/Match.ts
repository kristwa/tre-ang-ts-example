/// <reference path="../References.ts"/> 
module Scoretracker {
    export class Match {
        id: number;
        homeTeam: Team;
        awayTeam: Team;
        goalsHomeTeam: number;
        goalsAwayTeam: number;
        matchDate: Date;
    }
} 