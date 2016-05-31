$(document).ready(function () {



    $('#coin').on('click', function () {

        $('#coin').removeClass();

        setTimeout(function () {
            $('#coin').addClass(getSpin());
        }, 100);

    });

});


//var spinArray = ['animation900', 'animation1080', 'animation1260', 'animation1440', 'animation1620', 'animation1800', 'animation1980', 'animation2160'];


function getSpin() {
    //var spin = spinArray[Math.floor(Math.random() * spinArray.length)];
    var spin = "";
    var randomNumber = Math.random() * 2;
    if (randomNumber < 1) {
        spin = "animation1980";
        
    }
    else {
        spin = "animation2160";

    }
    return spin;
}

