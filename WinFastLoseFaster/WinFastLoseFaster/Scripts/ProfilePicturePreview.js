$(document).ready(function () {

    

    $("#inputProfilePicture").keyup(function () {

        var newUrl = $("#inputProfilePicture").val();

        $('#imgPreview').attr('src', newUrl);

    });

    $("#inputProfilePicture").keydown(function () {

        var newUrl = $("#inputProfilePicture").val();

        $('#imgPreview').attr('src', newUrl);

    });

    $("#inputProfilePicture").change(function () {

        var newUrl = $("#inputProfilePicture").val();

        $('#imgPreview').attr('src', newUrl);

    });

});