
$(document).ready(function () {

    var loadingDiv = $('#loadingDiv');
    loadingDiv.hide();

    $('#clearScrap').click(function (event) {
        $('#dataHoldElm').empty();
        $('#PostInfo').empty();
    });

    //    $('#runSpreadCrapingLnk').click(function (event) {
    //        $.fn.RunNBASpreadScrap({ GameDate: '20080316', HomeTeamCity: 'New Orleans' });
    //        return false;
    //    });

    $('#loadNBABets').click(function (event) {
        loadingDiv.show();
        $.ajax({
            url: GetRootURL() + 'sports-betting/nba/InsertNBABetting?seasonId=' + $('#SeasonSelect').val(),
            //data: JSON.stringify(games),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success) {
                    loadingDiv.hide();
                    mediator.publish('BetLoadDone', {});
                } else {
                    //show error
                    mediator.publish('pageError', { error: data.message, method: 'loadNBABets' });
                }
            },
            error: function () {
                mediator.publish('logError', { error: 'Error with service for InsertNBABetting', method: 'NBA Load Bets' });
            }
        });

        return false;
    });

    $('#upDateGameLine').click(function (event) {

        var game = {
            GameId: $('#GameId').val(),
            HomeTeamId: $('#HomeTeamId').val(),
            AwayTeamId: $('#AwayTeamId').val(),
            HomeTeamLine: $('#HomeTeamLine').val(),
            HomeTeamScore: $('#HomeTeamScore').val(),
            AwayTeamScore: $('#AwayTeamScore').val(),
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
        $.ajax({
            //url: GetRootURL() + 'sports/nba/UpdateGameLines?gameId=' + $('#GameId').val() + '&homeLine=' + $('#HomeTeamLine').val() + '&awayLine=' + $('#AwayTeamLine').val(),
            url: GetRootURL() + 'sports-betting/nba/UpdateGameLines',
            data: JSON.stringify(game),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success) {
                    //alert('updated');
                    $('#updateNote').show();
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
});

