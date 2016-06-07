$(document).ready(function () {

    $("#matcherFlik").on("click", function() {

        $('#stats').css("display", "none");
        $('#statsTitlar').css("display", "none");
        $("#matches").css("display", "initial");

    });//Matches onclick

    $("#statsFlik").on("click", function () {

        $('#matches').css("display", "none");
        $('#statsTitlar').css("display", "none");       
        $("#stats").css("display", "initial");

    });//Stats onclick
});//ready

