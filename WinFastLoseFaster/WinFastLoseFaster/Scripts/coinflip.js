$(document).ready(function () {

    $(".btn-success .coinflipJoinGame").on("click", function () {

        $("#coin").css("display", "initial");
        $('#coin').removeClass();

        setTimeout(function () {
            $('#coin').addClass(getSpin());
        }, 100);

    });

    $("#coin .front").css("background-image", "url(http://i3.kym-cdn.com/photos/images/original/000/013/251/lenny_fsjal.jpg)");
    $("#coin .back").css("background-image", "url(http://orig07.deviantart.net/94d2/f/2014/212/8/0/fsjal_squirtle_by_toonstar96-d7t4sie.png)");
    //$("#coin .front, #coin .back").css("background-size", "cover");

    $('#coin').on('click', function () {

        $("#coin").css("display", "initial");
        $('#coin').removeClass();

        setTimeout(function () {
            $('#coin').addClass(getSpin());
        }, 100);

    });

    setInterval(function () {

        getGameList();

    }, 1000);

});


//var spinArray = ['animation900', 'animation1080', 'animation1260', 'animation1440', 'animation1620', 'animation1800', 'animation1980', 'animation2160'];


function getSpin() {
    //var spin = spinArray[Math.floor(Math.random() * spinArray.length)];
    var spin = "";
    var randomNumber = Math.random() * 2;
    if (randomNumber < 1) {
        alert("SQUIRTLE WON");
        spin = "animation1980";
        
    }
    else {
        alert("LENNY WON");
        spin = "animation2160";
        

    }
    return spin;
}



function getGameList()
{

    $.ajax({

        url: '/Games/ListCoinflipGames/',
        dataType: 'json',
        data: {},
        success: function (data) {
            $('#ajaxResult').html('Det finns '
                + data.Count + ' djur i databasen!');

            for (i = 0; i < data.Count; i++) {

                var cGameToList = data.activeCoinflipGame[i];
                
                $("#coinflipGameList").html($("#coinflipGameList").html() +  "<div class='coinflipGame row'>" 
                    + cGameToList.users[0].Username
                    + "Wager: " + cGameToList.Userbets[0].Wager
                    + cGameToList.Timestamp

                    + "<form action='JoinCoinflip' method='post'>"
                    + "<input type='number' value='" + cGameToList.Id + "' name='coinflipGameId' readonly />"
                    + "<input class='btn-success coinflipJoinGame' type='submit' value='Join Game' />"
                    + "</form>"
                    
                    + "</div>");

            }
            

        },
        error: function (jqXHR, statusText, errorThrown) {
            $('#coinflipGameList').html('Ett fel inträffade: <br>'
                + statusText);
        }

    });

        
}