$(document).ready(function () {

    var createrPicture = $("#createrPicture").html();
    var joinerPicture = $("#joinerPicture").html();

    $("#coin .front").css("background-image", "url(" + createrPicture + ")");
    $("#coin .back").css("background-image", "url(" + joinerPicture + ")");

    //$("#coin").css("display", "initial");
    $('#coin').removeClass();

    setTimeout(function () {
        $('#coin').addClass(getSpin());
    }, 1000);


});


function getSpin() {

    //var winner = $("#coinflipWinner").html();

    var spin = "";
    //alert(winner + " won!");
    spin = "animation1980";


    //spin = "animation2160";


    return spin;

}