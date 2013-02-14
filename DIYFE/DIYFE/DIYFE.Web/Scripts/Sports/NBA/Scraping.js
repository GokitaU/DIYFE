
$(document).ready(function () {

    //these var are used to loop over and scrap spread/betting data
    var gameDates;
    var gameDatesIndex = 0;

    var htmlHolder = $('#dataHoldElm');
    var statusDiv = $('#statusUpdate');
    var loadingDiv = $('#loadingDiv');
    loadingDiv.hide();

    $.fn.RunNBATeamScrap = function (team) {

        var games = new Array();
        
        var html = $('<div></div>');

        html.load(team.ScrapUrl + ' #my-teams-table', function (response, status, xhr) {
            if (status == "error") {
                mediator.publish('pageError', { error: xhr.status + " " + xhr.statusText, method: 'loading ' + team.ScrapUrl + ' for scrapping - -' });
            } else {

                htmlHolder.append(html);
                statusDiv.append(team.ScrapUrl + ' Done</br>');
            }
        });
    }

    $.fn.RunNBASpreadScrap = function (game) {

        if (game.HomeTeamCity == "Brooklyn") {
            game.HomeTeamCity = "New Jersey";
        }
        if (game.HomeTeamName == "Los Angeles Lakers") {
            game.HomeTeamCity = "L.A. Lakers";
        }
        if (game.HomeTeamName == "Los Angeles Clippers") {
            game.HomeTeamCity = "L.A. Clippers";
        }

        var htmlHolder = $('#dataHoldElm');
        var scrapErr = $('#scrapError');
        htmlHolder.empty();
        var html = $('<div></div>');
        var moneyLine = $('<div></div>');
        html.load('http://www.sbrforum.com/nba-basketball/odds-scores/' + game.GameDate + ' .tbl-odds:contains("' + game.HomeTeamCity + '")', function (response, status, xhr) {
            //html.load('http://www.sbrforum.com/nba-basketball/odds-scores/' + game.GameDate + ' .tbl-odds:contains("' + game.HomeTeamCity + '")', function (response, status, xhr) {
            //html.load('http://www.sbrforum.com/nba-basketball/odds-scores/' + game.GameDate + ' td:contains("' + game.HomeTeamCity + '")', function (response, status, xhr) {
            if (status == "error") {
                mediator.publish('pageError', { error: xhr.status + " " + xhr.statusText, method: 'loading ' + game.GameDate + ' for scrapping - -' });
                scrapErr.append(game.GameDate + ' ' + game.GameId + ' ' + game.HomeTeamCity + "</br>");
            } else {

                htmlHolder.append(html);



                var moneyLine = $('<div></div>');

                if (htmlHolder.html() == '<div></div>') {
                    scrapErr.append(game.GameDate + ' ' + game.GameId + ' ' + game.HomeTeamCity + "</br>");
                    //mediator.publish('pageError', { error: 'coudnt find game scraping', method: 'loading ' + game.GameDate + ' for scrapping - -' });
                    if (game.RunSave) {
                        mediator.publish('NBASpreadScrap', { GameId: game.GameId });
                    }

                } else {

                    var moneyLine = $('<div></div>');
                    var lineUrl = htmlHolder.find('a:contains("Money Line")').attr("href"); //.slice(0, -1);
                    //moneyLine.load(lineUrl + ' .tbl-lines-history-link'), function (response, status, xhr) {
                    //+ ' .tbl-lines-history-link  div:contains("Bookmaker Odds History")'
                    moneyLine.load(lineUrl + ' .AlternatingData1', function (response, status, xhr) {

                        // alert('FUSKLDFJ');
                        if (status == "error") {
                            scrapErr.append(game.GameDate + ' ' + game.GameId + ' ' + game.HomeTeamCity + "</br>");
                            mediator.publish('pageError', { error: xhr.status + " " + xhr.statusText, method: 'loading ' + game.GameDate + ' for scrapping - -' });
                        } else {
                            htmlHolder.append(moneyLine);


                            if (game.RunSave) {
                                mediator.publish('NBASpreadScrap', { GameId: game.GameId });
                            }
                            //alert(moneyLine);
                        }
                    });
                }



                //                htmlHolder.find('.tbl-odds-c5').each(function (index) {
                //                    alert(index + ': ' + $(this).text());
                //                });
                //                htmlHolder.find('.tbl-odds-c7').each(function (index) {
                //                    alert(index + ': ' + $(this).text());
                //                });
            }
        });
    }

    $('#runScrapingLnk').click(function (event) {

        var selectedTeam = $('#TeamSelect').val();
        var selectedSeason = $("#SeasonSelect option:selected").text();
        var pageError = $('#pageErrorDiv');
        var teamsArray;

        if (selectedTeam == '0') {
            mediator.publish('pageError', { error: 'Please Select Team', method: 'Run Scraping Link Click' });
            return false;
        } else {
            pageError.hide();
        }
        if (selectedSeason == 'Select Season') {
            mediator.publish('pageError', { error: 'Please Select Season', method: 'Run Scraping Link Click' });
            return false;
        } else {
            pageError.hide();
        }

        if (selectedTeam == '00') {
            //Load All Team Details
            $.ajax({
                url: GetRootURL() + 'Sports/GetAllTeams?leagueId=2',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.success) {
                        teamsArray = data.teams
                        $.each(data.teams, function (index, item) {
                            item.ScrapUrl = item.ScrapUrl.replace('[year]', selectedSeason);
                            $.fn.RunNBATeamScrap(item);
                        });
                    } else {
                        mediator.publish('pageError', { error: data.message, method: 'Run Scraping Loading All Teams' });
                    }
                },
                error: function () {
                    mediator.publish('logError', { error: 'Error with service for team', method: 'Run Scraping Loading All Teams' });
                }
            });

        } else {
            //Load Single Team
            $.ajax({
                url: GetRootURL() + 'Sports/GetTeam?teamId=' + selectedTeam,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.success) {
                        data.team.ScrapUrl = data.team.ScrapUrl.replace('[year]', selectedSeason);
                        $.fn.RunNBATeamScrap(data.team);
                    } else {
                        mediator.publish('pageError', { error: data.message, method: 'Run Scraping Loading Team Details' });
                    }
                },
                error: function () {
                    mediator.publish('logError', { error: 'Error with service for team', method: 'Run Scraping Loading Team Details' });
                }
            });
        }

        //        $.each(teamsArray, function (index, item) {
        //            alert(item.ScrapUrl);
        //        });


        return false;

    });

    $('#saveScrappingLnk').click(function (event) {

        loadingDiv.show();
        var htmlHolder = $('#dataHoldElm');
        var season = $('#SeasonSelect').val();
        var year = $("#SeasonSelect option:selected").text();
        var games = new Array();

        htmlHolder.find('.oddrow, .evenrow').each(function (index) {

            var model = new GameScrapping();
            model.SeasonId = season;
            model.TeamOneId = $('#TeamSelect').val();
            //model.TeamOneId = team.TeamId;
            model.TeamTwoScrapId = $(this).attr('class').replace('oddrow', '').replace('evenrow', '');
            model.GameDate = $(this).find('td:eq(0)').html() + ' ' + year;

            model.Location = $(this).find('td:eq(1) .game-status').html()

            if ($(this).find('.game-schedule').length == 2) {
                model.GameStatus = $(this).find('.game-schedule:eq(1) span').html();
                model.GameScore = $(this).find('.game-schedule:eq(1) a').html();
                //alert(gameStatus[1].find('span').html() + ' ' + gameStatus[1].find('a').html());

            } else {
                var gameDate = new Date(model.GameDate);
                var today = new Date();
                today.setDate(today.getDate() - 1);
                if (gameDate < today) {
                    model.GameStatus = 'Postponed';
                    //alert(gameDate + ' ' + today + ' Postpouned');
                } else {
                    model.GameStatus = 'Scheduled';
                }
                model.GameScore = '0';
            }

            games[index] = model;

        });

        $.ajax({
            url: GetRootURL() + 'sports-betting/nba/InsertNBAGameScrap',
            data: JSON.stringify(games),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success) {
                    loadingDiv.hide();
                    $('#dataHoldElm').empty();
                    $('#PostInfo').append('<p>!done!</p>');
                    mediator.publish('ScrapDone', {});
                } else {
                    //show error
                    mediator.publish('pageError', { error: data.message, method: 'Insert games for team failed' });
                }
            },
            error: function () {
                mediator.publish('logError', { error: 'Error with service for InsertGameScrap', method: 'Insert games for team when scrapping' });
            }
        });

        return false;
    });

    mediator.subscribe('NBASpreadScrap', function (arg) {

        var replacementText = '';
        //c4 - Spread Open
        htmlHolder.find('.tbl-odds-c4 nobr').each(function (index) {

            replacementText = $(this).html().replace("½", ".5");
            var spreadArray = replacementText.split("&nbsp;");

            if (index == 0) {
                $('#AwayTeamSpreadOpen').val(spreadArray[0]);
                $('#AwayTeamSpreadPayOpen').val(spreadArray[1]);
            }
            if (index == 1) {
                $('#HomeTeamSpreadOpen').val(spreadArray[0]);
                $('#HomeTeamSpreadPayOpen').val(spreadArray[1]);
            }
        });

        //c5 - Spread Current
        htmlHolder.find('.tbl-odds-c5 div').each(function (index) {

            replacementText = $(this).html().replace("½", ".5");
            var spreadArray = replacementText.split("&nbsp;");

            if (index == 0) {
                $('#AwayTeamSpread').val(spreadArray[0]);
                $('#AwayTeamSpreadPay').val(spreadArray[1]);
            }
            if (index == 1) {
                $('#HomeTeamSpread').val(spreadArray[0]);
                $('#HomeTeamSpreadPay').val(spreadArray[1]);
            }
        });

        //c-6 MoneyLine Open
        //htmlHolder.find('.tbl-odds-c6 nobr').each(function (index) {

        //    replacementText = $(this).html().replace("½", ".5");
        //    var spreadArray = replacementText.split("&nbsp;");

        //    if (index == 0) {
        //        $('#AwayTeamMoneyLineOpen').val(spreadArray[0]);
        //    }
        //    if (index == 1) {
        //        $('#HomeTeamMoneyLineOpen').val(spreadArray[0]);
        //    }
        //});

        //c-7 MoneyLine Current
        htmlHolder.find('.AlternatingData1:eq(0) .hs_Price').each(function (index) {
            //alert($(this).html());
            //                    replacementText = $(this).html().replace("½", ".5");
            //                    var spreadArray = replacementText.split("&nbsp;");
            //                    
            if (index == 0) {
                $('#AwayTeamMoneyLine').val($(this).html());
            }
            if (index == 1) {
                $('#HomeTeamMoneyLine').val($(this).html());
            }
        });

        var game = {
            GameId: arg.GameId,
            HomeTeamLine: $('#HomeTeamLine').val(),
            AwayTeamLine: $('#AwayTeamLine').val(),
            HomeTeamSpread: $('#HomeTeamSpread').val(),
            HomeTeamSpreadPay: $('#HomeTeamSpreadPay').val(),
            HomeTeamSpreadOpen: $('#HomeTeamSpreadOpen').val(),
            HomeTeamSpreadPayOpen: $('#HomeTeamSpreadPayOpen').val(),
            HomeTeamMoneyLine: $('#HomeTeamMoneyLine').val(),
            HomeTeamMoneyLineOpen: $('#HomeTeamMoneyLineOpen').val(),
            AwayTeamSpread: $('#AwayTeamSpread').val(),
            AwayTeamSpreadPay: $('#AwayTeamSpreadPay').val(),
            AwayTeamSpreadOpen: $('#AwayTeamSpreadOpen').val(),
            AwayTeamSpreadPayOpen: $('#AwayTeamSpreadPayOpen').val(),
            AwayTeamMoneyLine: $('#AwayTeamMoneyLine').val(),
            AwayTeamMoneyLineOpen: $('#AwayTeamMoneyLineOpen').val(),
            WonSpread: $('#WonSpread').is(':checked'),
            WonMoneyLine: $('#WonLine').is(':checked')
        };
        // alert(gameArray.length + ' ' + gameIndex + ' ' + gameArray[gameIndex]);

        $.ajax({
            //url: GetRootURL() + 'sports/nba/UpdateGameLines?gameId=' + $('#GameId').val() + '&homeLine=' + $('#HomeTeamLine').val() + '&awayLine=' + $('#AwayTeamLine').val(),
            url: GetRootURL() + 'sports-betting/nba/UpdateGameLines',
            data: JSON.stringify(game),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success) {
                    //alert('updated');
                    //$('#updateNote').show();
                    gameIndex++;
                    if (gameArray.length > gameIndex) {

                        $.ajax({
                            url: GetRootURL() + 'Sports/GetGameDetails?gameId=' + gameArray[gameIndex],
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            success: function (data) {
                                if (data.success) {
                                    $.fn.RunNBASpreadScrap({
                                        GameDate: data.game.GameDateFormated,
                                        HomeTeamName: data.game.HomeTeamName,
                                        HomeTeamCity: data.game.GameLocationName,
                                        GameId: gameArray[gameIndex],
                                        RunSave: true
                                    });
                                } else {
                                    //show error
                                    mediator.publish('pageError', { error: data.message, method: 'Load Game for spread scrap' });
                                }
                            },
                            error: function () {
                                mediator.publish('logError', { error: 'Error with service for Geting Game Deatils', method: 'GameDetails' });
                            }
                        });
                    }
                } else {
                    //show error
                    mediator.publish('pageError', { error: data.message, method: 'upDateGameLine' });
                }
            },
            error: function () {
                mediator.publish('logError', { error: 'Error with service for upDateGameLine', method: 'upDateGameLine' });
            }
        });
    });

    //SPREAD SCRAPING FUNCTION AND STUFF
    $.fn.LoadSpreadDate = function () {
           
    }


    $('#runSpreadCrapingLnk').click(function (event) {
        var selectedSeason = $("#SeasonSelect").val();

        $.ajax({
            url: GetRootURL() + 'sports-betting/nba/GetGameDates?seasonId=' + selectedSeason,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success) {
                    gameDates = data.dates;
                    alert(gameDates.length);
                } else {
                    //show error
                    mediator.publish('pageError', { error: data.message, method: 'Insert games for team failed' });
                }
            },
            error: function () {
                mediator.publish('logError', { error: 'Error with service for InsertGameScrap', method: 'Insert games for team when scrapping' });
            }
        });

    });

});