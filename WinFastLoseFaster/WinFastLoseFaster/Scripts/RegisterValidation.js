$(document).ready(function () {


    document.getElementById("registerButton").disabled = true;

    alert("JS LOADED!");
    var registerUsername = document.getElementById("registerUsername");
    var registerPassword = document.getElementById("registerPassword");
    var registerRetypePassword = document.getElementById("registerRetypePassword");
    var registerEmail = document.getElementById("registerEmail");
    var registerCheckbox = document.getElementById("registerCheckbox");

    var errorDiv = document.getElementById("errorMsg");


    registerUsername.addEventListener("keypress", CallRegisterValidationFunctions);
    registerPassword.addEventListener("keypress", CallRegisterValidationFunctions);
    registerRetypePassword.addEventListener("keypress", CallRegisterValidationFunctions);
    registerEmail.addEventListener("keypress", CallRegisterValidationFunctions);
    registerCheckbox.addEventListener("change", CallRegisterValidationFunctions);

});


function CallRegisterValidationFunctions() {

    errorDiv.innerHTML = "";

    validateUsername(registerUsername.value);
    validatePassword(registerPassword.value, registerRetypePassword.value);
    validateCheckbox(registerCheckbox.checked);

    if(errorDiv.innerHTML == "")
    {
        document.getElementById("registerSubmit").disabled = false;

    }
    else
    {
        document.getElementById("registerSubmit").disabled = true;

    }

};

function validateUsername(username) {
    var usernameMinLength = 3;

    if(username.length >= usernameMinLength)
    { }
    else
    {
        errorDiv.innerHTML += "Username måste vara <bold><u>minst</u></bold> 3 karaktärer<br />";

    }

};

function validatePassword(password1, password2) {
    var passwordMinLength = 6;

    if(password1.length >= passwordMinLength)
    { }
    else
    {
        errorDiv.innerHTML += "Lösenord måste vara <bold><u>minst</u></bold> 6 karaktärer<br />";

    }

    if(password1 === password2)
    { }
    else
    {
        errorDiv.innerHTML += "Lösenord matchar inte!<br>";

    }


};

function validateCheckbox(isChecked) {

    if (isChecked)
    { }
    else
    {
        errorDiv.innerHTML += "Checkboxen är inte checkad<br />";

    }

};



