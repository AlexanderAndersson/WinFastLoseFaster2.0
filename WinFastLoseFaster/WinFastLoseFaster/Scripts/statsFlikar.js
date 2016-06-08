$(document).ready(function () {

    $("#matcherFlik").on("click", function() {

        $('#stats').css("display", "none");
        $('#statsTitlar').css("display", "inline-block");
        $("#matches").css("display", "inline-block");
        $("#bank").css("display", "none");

    });//Matches onclick

    $("#statsFlik").on("click", function () {

        $('#matches').css("display", "none");
        $('#statsTitlar').css("display", "none");       
        $("#stats").css("display", "initial");
        $("#bank").css("display", "none");

    });//Stats onclick

    $("#bankFlik").on("click", function () {

        $('#matches').css("display", "none");
        $('#statsTitlar').css("display", "none");
        $("#stats").css("display", "none");
        $("#bank").css("display", "initial");

    });//Bank onclick
});//ready

