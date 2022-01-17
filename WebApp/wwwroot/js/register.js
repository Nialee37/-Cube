/**
 * Fonction qui permet de vérifier si le mot de passe est valide ou non.
 * @param {*} password le mot de passe a vérifié.
 * @returns une valeur boolean qui dit si le mot de passe est valide ou non.
 */
function checkPassword(password) {
    var passwordCheck = false;
    var nbChar = /^.{8,15}$/;
    var majChar = /[A-Z]/;
    var minChar = /[a-z]/;
    var minNumber = /\d/;
    var minCharSpe = /[-+!*$@%_]/;
    var exclusion = /[^-+!*$@%_\w]/;
    var pathPassword = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[-+!*$@%_])([-+!*$@%_\w]{8,15})$/;
    password.parent().removeClass("is-invalid");
    if (password.val() == "") {
        password.parent().addClass("is-invalid");
        password.addClass("is-invalid");
        password.parent().find(".invalid-feedback").html("Veuillez saisir un mot de passe !");
    } else if (!pathPassword.test(password.val())) {
        password.parent().addClass("is-invalid");
        password.addClass("is-invalid");
        password.parent().find(".invalid-feedback").html("Veuillez saisir un mot de passe valide : <ul>" +
            "<li id=\"nbChar\" class=\"text-success fas fa-check\"> De 8 à 15 caractères</li>" +
            "<li id=\"minChar\" class=\"text-success fas fa-check\"> Au moins une lettre minuscule</li>" +
            "<li id=\"majChar\" class=\"text-success fas fa-check\"> Au moins une lettre majuscule</li>" +
            "<li id=\"minNumber\" class=\"text-success fas fa-check\"> Au moins un chiffre</li>" +
            "<li id=\"minCharSpe\" class=\"text-success fas fa-check\"> Au moins un caractères spécial : $ @ % * + - _ !</li>" +
            "<li id=\"exclusion\" class=\"text-success fas fa-check\"> Pas de caractère interdit tel que &, {, )</li></ul>");
        if (!nbChar.test(password.val())) {
            $("#nbChar").removeClass("text-success fa-check");
            $("#nbChar").addClass("text-danger fa-times");
        } else {
            $("#nbChar").addClass("text-success fa-check");
            $("#nbChar").removeClass("text-danger fa-times");
        }
        if (!majChar.test(password.val())) {
            $("#majChar").removeClass("text-success fa-check");
            $("#majChar").addClass("text-danger fa-times");
        } else {
            $("#majChar").addClass("text-success fa-check");
            $("#majChar").removeClass("text-danger fa-times");
        }
        if (!minChar.test(password.val())) {
            $("#minChar").removeClass("text-success fa-check");
            $("#minChar").addClass("text-danger fa-times");
        } else {
            $("#minChar").addClass("text-success fa-check");
            $("#minChar").removeClass("text-danger fa-times");
        }
        if (!minNumber.test(password.val())) {
            $("#minNumber").removeClass("text-success fa-check");
            $("#minNumber").addClass("text-danger fa-times");
        } else {
            $("#minNumber").addClass("text-success fa-check");
            $("#minNumber").removeClass("text-danger fa-times");
        }
        if (!minCharSpe.test(password.val())) {
            $("#minCharSpe").removeClass("text-success fa-check");
            $("#minCharSpe").addClass("text-danger fa-times");
        } else {
            $("#minCharSpe").addClass("text-success fa-check");
            $("#minCharSpe").removeClass("text-danger fa-times");
        }
        if (exclusion.test(password.val())) {
            $("#exclusion").removeClass("text-success fa-check");
            $("#exclusion").addClass("text-danger fa-times");
        } else {
            $("#exclusion").addClass("text-success fa-check");
            $("#exclusion").removeClass("text-danger fa-times");
        }
    } else {
        password.parent().removeClass("is-invalid");
        password.parent().find(".invalid-feedback").empty();
        passwordCheck = true;
    }
    return passwordCheck;
}

$(document).ready(function () {
    var checkPass = false;
    $("#confirmPassword").keyup(function () {
        if (checkPass) {
            if ($("#confirmPassword").val() != $("#password").val()) {
                $("#confirmPasswordAlert").text("Les mots de passe ne sont pas identique !");
                $("#passwordAlert").text("");
                $("#password").addClass("is-invalid");
                $("#confirmPassword").addClass("is-invalid");
                $("#login-btn").attr("disabled", "disabled");
            } else {
                $("#passwordAlert").text("");
                $("#confirmPasswordAlert").text("");
                $("#login-btn").removeAttr("disabled");
                $("#password").removeClass("is-invalid");
                $("#confirmPassword").removeClass("is-invalid");
            }
        }
    });
    $("#password").keyup(function () {
        if (checkPassword($("#password"))) {
            checkPass = true;
            if ($("#confirmPassword").val() != $("#password").val()) {
                $("#passwordAlert").text("Les mots de passe ne sont pas identique !");
                $("#confirmPasswordAlert").text("");
                $("#password").addClass("is-invalid");
                $("#confirmPassword").addClass("is-invalid");
                $("#login-btn").attr("disabled","disabled");
            } else {
                $("#passwordAlert").text("");
                $("#confirmPasswordAlert").text("");
                $("#login-btn").removeAttr("disabled");
                $("#password").removeClass("is-invalid");
                $("#confirmPassword").removeClass("is-invalid");
            }
        } else {
            checkPass = false;
        }
    });
});