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

        createTable(group: Group): TableItem[] {
            var table: TableItem[] = new Array<TableItem>();

            group.teams.forEach(team => {
                var tableItem = new TableItem();
                tableItem.team = team;
                tableItem.games = 0;
                tableItem.won = 0;
                tableItem.drawn = 0;
                tableItem.lost = 0;
                tableItem.goalsFor = 0;
                tableItem.goalsAgainst = 0;
                tableItem.points = 0;
                table.push(tableItem);
            });

            group.matches.forEach(match => {
                var tableItemHomeTeam = this.findTableItem(match.homeTeam.id, table);
                var tableItemAwayTeam = this.findTableItem(match.awayTeam.id, table);

                if (match.goalsAwayTeam != null && match.goalsHomeTeam != null) {
                    this.updateTable(tableItemHomeTeam, match.goalsHomeTeam, match.goalsAwayTeam);
                    this.updateTable(tableItemAwayTeam, match.goalsAwayTeam, match.goalsHomeTeam);
                }
            });

            //Sort table
            table = table.sort((t1, t2) => {
                if (t1.points > t2.points) {
                    return -1;
                } else if (t1.points == t2.points) {
                    var gd1 = t1.goalsFor - t1.goalsAgainst;
                    var gd2 = t2.goalsFor - t2.goalsAgainst;

                    if (gd1 > gd2) {
                        return -1;
                    }
                    else if (gd1 == gd2) {
                        return t2.goalsFor - t1.goalsFor;
                    } else {
                        return 1;
                    }

                } else {
                    return 1;
                }
            });

            var i: number = 1;

            table.forEach(item => {
                item.place = i;
                i++;
            });

            return table;
        } 

        updateTable(tableItem: TableItem, goalsScored: number, goalsConceeded: number) {
            tableItem.goalsFor += goalsScored;
            tableItem.goalsAgainst += goalsConceeded;
            tableItem.games++;

            var diff = goalsScored - goalsConceeded;

            if (diff > 0) {
                tableItem.won++;
                tableItem.points += 3;
            }
            else if (diff == 0) {
                tableItem.drawn++;
                tableItem.points++;
            } else {
                tableItem.lost++;
            }
        }

        findTableItem(teamId: number, table: TableItem[]): TableItem {
            var selection: TableItem;

            table.forEach(tableItem => {
                if (tableItem.team.id === teamId)
                    selection = tableItem;
            });

            return selection;
        }
    }
}