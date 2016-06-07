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

        //$("#coin").css("display", "initial");
        alert(document.getElementById("coin").style.display)
        $('#coin').removeClass();

        setTimeout(function () {
            $('#coin').addClass(getSpin());
        }, 100);

    });

    getGameList();

    setInterval(function () {

        getGameList();

    }, 3000);

});


//var spinArray = ['animation900', 'animation1080', 'animation1260', 'animation1440', 'animation1620', 'animation1800', 'animation1980', 'animation2160'];


function getSpin() {
    //var spin = spinArray[Math.floor(Math.random() * spinArray.length)];
    var spin = "";
    var randomNumber = Math.random() * 2;
    if (randomNumber < 1) {
        //alert("SQUIRTLE WON");
        spin = "animation1980";

    }
    else {
        //alert("LENNY WON");
        spin = "animation2160";


    }
    return spin;
}



function getGameList() {

    $.ajax({

        url: '/Games/ListCoinflipGames/',
        dataType: 'json',
        data: {},
        success: function (data) {
            var list = data.activeCoinflipGame;

            $("#coinflipGameList").html("");

            //alert("data: " + data);
            //alert("json activeCoinflipGame obj: " + data.activeCoinflipGame);
            console.log("data: " + JSON.stringify(data));
            console.log("json activeCoinflipGame obj: " + JSON.stringify(data.activeCoinflipGame));

            for (i = 0; i < list.length; i++) {

                //alert("I FOR LOOPEN");

                var cGameToList = list[i];

                

                var shitToWrite = $("#coinflipGameList").html($("#coinflipGameList").html() + "<div class='coinflipGame row'>"
                    + cGameToList.Creater + " "
                    + "Wager: " + cGameToList.Wager + " "
                    + cGameToList.ShortDate + " "
                    + cGameToList.ShortTime

                    + "<form action='JoinCoinflip' method='post'>"
                    + "<input type='number' value='" + cGameToList.GameId + "' name='coinflipGameId' readonly />"
                    + "<input class='btn-success coinflipJoinGame' type='submit' value='Join Game' style='float:right' />"
                    + "</form>"
                    
                    + "</div>");

                
                //alert("Saker att skriva till div coinflipGameList: " + shitToWrite);


                //document.getElementById("coinflipGameList").innerHTML = "";

                //document.getElementById("coinflipGameList").innerHTML += "<div class='coinflipGame row'>"
                //+ cGameToList.Username
                //+ ", Wager: " + cGameToList.Wager

                //+ "<form action='JoinCoinflip' method='post'>"
                //+ "<input type='number' value='" + cGameToList.GameId + "' name='coinflipGameId' readonly />"
                //+ "<input class='btn-success coinflipJoinGame' type='submit' value='Join Game' />";
                //+ "</div>";


            }

            //alert("AFTER FOREACH");


        },
        error: function (jqXHR, statusText, errorThrown) {
            $('#coinflipGameList').html('Ett fel inträffade: <br>'
                + statusText);
        }

    });


}