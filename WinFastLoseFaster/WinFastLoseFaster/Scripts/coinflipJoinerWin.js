$(document).ready(function () {

    var createrPicture = $("#createrPicture").html();
    var joinerPicture = $("#joinerPicture").html();

    $("#coin .front").css("background-image", "url(" + createrPicture + ")");
    $("#coin .back").css("background-image", "url(" + joinerPicture + ")");

    $('#coin').removeClass();

    setTimeout(function () {
        $('#coin').addClass(getSpin());
    }, 1000);

    setTimeout(function () {

        $("#winnerName").css("display", "initial");
        $("#coin .back").css("border", "5px solid green");

    }, 4910);
});


function getSpin() {

    var spin = "";

    spin = "animation1980";


    return spin;

}