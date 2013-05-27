

function Team(TeamId, Name, City, ScrapId, ScrapUrl, Scrap2Url) {
    this.TeamId = TeamId;
    this.Name = Name;
    this.City = City;
    this.ScrapId = ScrapId;
    this.ScrapUrl = ScrapUrl;
    this.Scrap2Url = Scrap2Url;
    this.SendFormat = sendJsonFormat;
    this.EasyLoad = function (formData) {
        easyLoad(this, formData);
    };
}

function Game(GameId, GameDate, GameStatusId, TeamOneId, TeamTwoId, HomeTeamId, TeamOneScore, TeamTwoScore, WinningTeamId, HomeTeamCity, HomeTeamName, AwayTeamCity, AwayTeamName) {
    this.GameId = GameId;
    this.GameDate = GameDate;
    this.GameStatusId = GameStatusId;
    this.TeamOneId = TeamOneId;
    this.TeamTwoId = TeamTwoId;
    this.HomeTeamId = HomeTeamId;
    this.TeamOneScore = TeamOneScore;
    this.TeamTwoScore = TeamTwoScore;
    this.WinningTeamId = WinningTeamId;

    this.HomeTeamCity = HomeTeamCity;
    this.HomeTeamName = HomeTeamName;
    this.AwayTeamCity = AwayTeamCity;
    this.AwayTeamName = AwayTeamName;

    this.SendFormat = sendJsonFormat;


    this.EasyLoad = function (formData) {
        easyLoad(this, formData);
    };
}


function Team(TeamId, Name, City, ScrapId, ScrapUrl, Scrap2Url) {
    this.TeamId = TeamId;
    this.Name = Name;
    this.City = City;
    this.ScrapId = ScrapId;
    this.ScrapUrl = ScrapUrl;
    this.Scrap2Url = Scrap2Url;
    this.SendFormat = sendJsonFormat;
    this.EasyLoad = function (formData) {
        easyLoad(this, formData);
    };
}

function Game(GameId, GameDate, GameStatusId, TeamOneId, TeamTwoId, HomeTeamId, TeamOneScore, TeamTwoScore, WinningTeamId) {
    this.GameId = GameId;
    this.GameDate = GameDate;
    this.GameStatusId = GameStatusId;
    this.TeamOneId = TeamOneId;
    this.TeamTwoId = TeamTwoId;
    this.HomeTeamId = HomeTeamId;
    this.TeamOneScore = TeamOneScore;
    this.TeamTwoScore = TeamTwoScore;
    this.WinningTeamId = WinningTeamId;
    this.SendFormat = sendJsonFormat;
    this.EasyLoad = function (formData) {
        easyLoad(this, formData);
    };
}

function GameScrapping(SeasonId, TeamOneId, TeamTwoScrapId, GameDate, Location, GameStatus, GameScore) {
    this.SeasonId = SeasonId;
    this.TeamOneId = TeamOneId;
    this.TeamTwoScrapId = TeamTwoScrapId;
    this.GameDate = GameDate;
    this.Location = Location;
    this.GameStatus = GameStatus;
    this.GameScore = GameScore;
    this.SendFormat = sendJsonFormat;
    this.EasyLoad = function (formData) {
        easyLoad(this, formData);
    };
}