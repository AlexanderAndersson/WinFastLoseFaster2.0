$(document).ready(function () {

    //Hejsan, för ändring.

    UpdateShit();

    var refresher = setInterval(function() {

        UpdateShit(refresher);

    }, 300);

});

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}


function UpdateShit(refresher)
{

        $.ajax({

            url: '/Games/PlayCoinflipJson/',
            dataType: 'json',
            data: {
                gameId: getParameterByName("gameId", window.location.href)
            },
            success: function (data) {
                var game = data.activeCoinflipGame;

                $("#coinflipGameBoard").html("");

                //alert("data: " + data);
                //alert("json activeCoinflipGame obj: " + data.activeCoinflipGame);
                console.log("data: " + JSON.stringify(data));
                console.log("json activeCoinflipGame obj: " + JSON.stringify(data.activeCoinflipGame));

                if (game.gameActive == false) {
                    clearInterval(refresher);

                }

                if (game.gameActive == true)
                {
                    var shitToWrite = $("#coinflipGameBoard").html("<div class='someName row'>"
                    + "<p id='gameActive' readonly hidden>" + game.gameActive + "</p>"
                    + "<p id='gameId' readonly hidden>" + game.gameId + "</p>"

                    + "<center><h2>Waiting for a player to join</h2></center>"
                    + "<center><img src='" + game.CreaterPicture + "' class='profilPic'/></center>"
                    + "<div><h3>" + game.CreaterUsername + "</h3></div>"
                    + "<div><h3>"+ "Wager: " + game.Wager + "</h3></div>"
                    

                    + "</div>");

                }
                else
                {
                    
                    if (game.WinnerUsername == game.CreaterUsername)
                    {
                        var shitToWrite = $("#coinflipGameBoard").html("<div class='someName row'>"
                    + "<p id='gameActive' readonly hidden>" + game.gameActive + "</p>"
                    + "<p id='gameId' readonly hidden>" + game.gameId + "</p>"
                    + "<p id='createrPicture' readonly hidden>" + game.CreaterPicture + "</p>"
                    + "<p id='joinerPicture' readonly hidden>" + game.JoinerPicture + "</p>"


                    + '<div id="coin-flip-cont">'
                    +   '<div id="coin">'
                    +       '<div class="front"></div>'
                    +       '<div class="back"></div>'
                    +   '</div>'
                    + '</div>'


                    + "<div class='coinflipGameBoardLeft'>"
                        + "<div><img class='profilPic' src='" + game.CreaterPicture + "'/></div>"
                        + "<div><h3>" + game.CreaterUsername + "</h3></div>"
                    + "</div>"

                    + "<div class='coinflipGameBoardRight'>"                     
                        + "<div><img class='profilPic' src='" + game.JoinerPicture + "'/></div>"
                        + "<div><h3> " + game.JoinerUsername + "</h3></div><br />"
                    + "</div>"
                    + "<div id='vs'>"
                        + "<h3>VS</h3>"
                    + "<div id='Winner'>"
                    + "<h3>" + "WINNER IS!</h3><br/>"
                    + "</div>"
                    + "<div id='winnerName'><h3>" + game.WinnerUsername + "</h3></div>"
                    + "</div>");

                        var xtra = $("#coinflipGameBoard").html($("#coinflipGameBoard").html()
                        + "<script src='/Scripts/coinflipCreaterWin.js'></script>");


                        var xtra = $("#coinflipGameBoard").html($("#coinflipGameBoard").html()
                         + "<script src='/Scripts/coinflipJoinerWin.js'></script>");

                    }
                    else if (game.WinnerUsername == game.JoinerUsername)
                    {
                        var shitToWrite = $("#coinflipGameBoard").html("<div class='someName row'>"
                    + "<p id='gameActive' readonly hidden>" + game.gameActive + "</p>"
                    + "<p id='gameId' readonly hidden>" + game.gameId + "</p>"
                    + "<p id='createrPicture' readonly hidden>" + game.CreaterPicture + "</p>"
                    + "<p id='joinerPicture' readonly hidden>" + game.JoinerPicture + "</p>"

                    + '<div id="coin-flip-cont">'
                    + '<div id="coin">'
                        + '<div class="front"></div>'
                        + '<div class="back"></div>'
                    + '</div>'
                    + '</div>'
                    
                    + "<div class='coinflipGameBoardLeft'>"
                        + "<div><img class='profilPic' src='" + game.CreaterPicture + "'/></div>"
                        + "<div><h3>" + game.CreaterUsername + "</h3></div>"
                    + "</div>"

                    + "<div class='coinflipGameBoardRight'>"                     
                        + "<div><img class='profilPic' src='" + game.JoinerPicture + "'/></div>"
                        + "<div><h3> " + game.JoinerUsername + "</h3></div><br />"
                    + "</div>"
                    + "<div id='vs'>"
                        + "<h3>VS</h3>"
                    + "<div id='Winner'>"
                    + "<h3>" + "WINNER IS!</h3><br/>"
                    + "</div>"
                    + "<div id='winnerName'><h3>" + game.WinnerUsername + "</h3></div>"
                    + "</div>");
                                        
                        var xtra = $("#coinflipGameBoard").html($("#coinflipGameBoard").html()
                         + "<script src='/Scripts/coinflipJoinerWin.js'></script>");                   
                    }                   
                }               
            },
            error: function (jqXHR, statusText, errorThrown) {
                $('#coinflipGameList').html('Ett fel inträffade: <br>'
                    + statusText);
            }
        });
}