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
                    + "<p id='gameActive'>" + game.gameActive + "</p>"
                    + "<p id='gameId'>" + game.gameId + "</p>"



                    + "creater username: " + game.CreaterUsername + "<br />"
                    + "creater picture: " + game.CreaterPicture + "<br />"

                    + "</div>");

                }
                else
                {
                    
                    if (game.WinnerUsername == game.CreaterUsername)
                    {
                        var shitToWrite = $("#coinflipGameBoard").html("<div class='someName row'>"
                    + "<p id='gameActive'>" + game.gameActive + "</p>"
                    + "<p id='gameId'>" + game.gameId + "</p>"
                    + "<p id='createrPicture'>" + game.CreaterPicture + "</p>"
                    + "<p id='joinerPicture'>" + game.JoinerPicture + "</p>"


                    + '<div id="coin-flip-cont">'
                    +   '<div id="coin">'
                    +       '<div class="front"></div>'
                    +       '<div class="back"></div>'
                    +   '</div>'
                    + '</div>'


                    + "creater username: " + game.CreaterUsername + "<br />"
                    + "joiner username: " + game.JoinerUsername + "<br />"
                    + "winner username: " + game.WinnerUsername + "<br />"
                    + "creater picture: " + game.CreaterPicture + "<br />"
                    + "joiner picture: " + game.JoinerPicture + "<br />"
                    + "winner picture: " + game.WinnerPicture + "<br />"

                    + "</div>"
                    + "<script src='~/Scripts/coinflipJoinerWin.js'></script>");


                        var xtra = $("#coinflipGameBoard").html($("#coinflipGameBoard").html()
                         + "<script src='/Scripts/coinflipJoinerWin.js'></script>");

                    }
                    else if (game.WinnerUsername == game.JoinerUsername)
                    {
                        var shitToWrite = $("#coinflipGameBoard").html("<div class='someName row'>"
                    + "<p id='gameActive'>" + game.gameActive + "</p>"
                    + "<p id='gameId'>" + game.gameId + "</p>"
                    + "<p id='createrPicture'>" + game.CreaterPicture + "</p>"
                    + "<p id='joinerPicture'>" + game.JoinerPicture + "</p>"


                    + '<div id="coin-flip-cont">'
                    + '<div id="coin">'
                    + '<div class="front"></div>'
                    + '<div class="back"></div>'
                    + '</div>'
                    + '</div>'


                    + "creater username: " + game.CreaterUsername + "<br />"
                    + "joiner username: " + game.JoinerUsername + "<br />"
                    + "winner username: " + game.WinnerUsername + "<br />"
                    + "creater picture: " + game.CreaterPicture + "<br />"
                    + "joiner picture: " + game.JoinerPicture + "<br />"
                    + "winner picture: " + game.WinnerPicture + "<br />"

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