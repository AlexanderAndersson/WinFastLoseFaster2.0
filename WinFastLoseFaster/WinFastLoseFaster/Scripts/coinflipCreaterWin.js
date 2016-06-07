$(document).ready(function () {

    var createrPicture = $("#createrPicture").html();
    var joinerPicture = $("#joinerPicture").html();

    $("#coin .front").css("background-image", "url(" + createrPicture + ")");
    $("#coin .back").css("background-image", "url(" + joinerPicture + ")");
    

    setTimeout(function () {
        $('#coin').removeClass();
        $('#coin').addClass(getSpin());
    }, 1000);

    setTimeout(function () {

        $("#winnerName").css("display", "initial");
        $("#coin .front").css("border", "5px solid green");

    }, 4910);
});


function getSpin() {

    //var winner = $("#coinflipWinner").html();

    var spin = "";
    //alert(winner + " won!");
    //spin = "animation1980";

    //alert("LENNY WON");
    spin = "animation2160";


    return spin;

}