var checkFinalPassword = false;
var checkFDate = false;
var checkCGU = false;
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

/**
 * Fonction qui permet de vérifier si un input est valide ou non.
 * @param {*} input l'input a vérifié.
 * @param {*} messageErreur le message d'erreur a affiché si l'input est invalid.
 * @param {*} pattern le pattern que l'ont check s'il y a un pattern à ajouter sinon laisser la valeur à null.
 * @param {*} messageFalsePattern si l'input a besoin d'un pattern, affiche un message d'erreur est invalide.
 * @returns une valeur boolean qui dit si l'input est valide ou non.
 */
function checkInput(input, messageErreur, pattern = null, messageFalsePattern = null) {
    var check = false;
    input.parent().find(".invalid-feedback").empty();
    
    if (pattern != null) {
        if (input.val() == "" || input.val() == null ) {
            input.addClass("is-invalid");
            input.parent().find(".invalid-feedback").html(messageErreur);
        } else if (!pattern.test(input.val())) {
            input.addClass("is-invalid");
            input.parent().find(".invalid-feedback").html(messageFalsePattern);
        } else {
            input.removeClass("is-invalid");
            input.parent().find(".invalid-feedback").empty();
            check = true;
        }
    } else {
        if (input.val() == "" || input.val() == null) {
            input.addClass("is-invalid");
            input.parent().find(".invalid-feedback").html(messageErreur);
        } else {
            input.removeClass("is-invalid");
            input.parent().find(".invalid-feedback").empty();
            check = true;
        }
    }
    return check;
}

/**
 * Fonction qui permet de vérifier si un input est valide ou non sans afficher de message.
 * @param {*} input l'input a vérifié.
 * @param {*} pattern le pattern que l'ont check s'il y a un pattern à ajouter sinon laisser la valeur à null.
 * @returns une valeur boolean qui dit si l'input est valide ou non.
 */
function validInput(input, pattern = null) {
    var check = false;
    if (pattern != null) {
        if (input.val() != "" && input.val() != null) {
            if (pattern.test(input.val()))
                check = true;
        }
    } else {
        if (input.val() != "" && input.val() != null) {
            check = true;
        } 
    }
    return check;
}


/**
 * Fonction événementielle qui permet de vérifier l'input a chaque fois que l'on écrit dans un input.
 */
function checkOnInput() {
    $('select, input:not(#search-custom-2, [name=__RequestVerificationToken])').each(function (i) {
        $(this).on("keyup change", function () {
            if ($(this).attr("type") == "email") {
                    var pathMail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                    checkInput($(this), "Veuillez saisir un courriel !", pathMail, "Veuillez saisir un courriel avec le format exemple@mail.com");
            } else if ($(this).attr("type") != "password") {
                    if (this.tagName == "INPUT") {
                        checkInput($(this), "Veuillez saisir votre " + $(this).data("nom") + ".");
                    } else if (this.tagName == "SELECT") {
                        checkInput($(this), "Veuillez sélectionner votre " + $(this).data("nom") + ".");
                    } 
            }
            validForm();
        });
    });
}

function validForm() {
    if (checkFinalPassword && checkAllInput() && checkFDate && checkCGU)
        $("#login-btn").removeAttr("disabled");
    else
        $("#login-btn").attr("disabled", "disabled");
}

function checkCGUVerif() {
    $("#CGU").on("click", function () {
        if ($(this).prop("checked")) {
            checkCGU = true;
        } else {
            checkCGU = false;
        }
    })
}

function checkAllInput() {
    var numberValid = 0;
    $('select, input:not(#search-custom-2, [name=__RequestVerificationToken])').each(function (i) {
        if ($(this).attr("type") == "email") {
            var pathMail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (validInput($(this), pathMail)) {
                numberValid++;
            }
        } else {
            if (validInput($(this))) {
                numberValid++;
            }
        }
    });
    if ($('select, input:not(#search-custom-2, [name=__RequestVerificationToken])').length == numberValid) {
        console.log($('select, input:not(#search-custom-2, [name=__RequestVerificationToken])').length);
        return true;
    } else {
        return false;
    }
}

function checkDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; 
    var yyyy = today.getFullYear();
    ymin = yyyy - 150;
    yyyy = yyyy - 13;
    if (mm < 10) {
        mm = "0" + mm;
    }
    if (dd < 10) {
        dd = "0" + dd;
    }

    $(".dateNaissance").attr("max", yyyy + "-" + mm + "-" + dd);
    $(".dateNaissance").attr("min", ymin + "-" + mm + "-" + dd);

    $(".dateNaissance").on("change", function () {
        var dateSaisie = new Date($(this).val());
        var dd = dateSaisie.getDate();
        var mm = dateSaisie.getMonth() + 1;
        var yyyy = dateSaisie.getFullYear();

        var today = new Date();
        var Tdd = today.getDate();
        var Tmm = today.getMonth() + 1;
        var Tyyyy = today.getFullYear();
        Tmin = Tyyyy - 150;
        Tyyyy = Tyyyy - 13;
        

        if (yyyy == Tmin) {
            if (mm == Tmm) {
                if (dd >= Tdd) {
                    checkFDate = true;
                } else {
                    checkFDate = false;
                }
            } else if (mm > Tmm) {
                checkFDate = true;
            } else {
                checkFDate = false;
            }
        } else if (yyyy > Tmin) {
            if (yyyy == Tyyyy) {
                if (mm == Tmm) {
                    if (dd <= Tdd) {
                        checkFDate = true;
                    } else {
                        checkFDate = false;
                    }
                } else if (mm < Tmm) {
                    checkFDate = true;
                } else {
                    checkFDate = false;
                }
            } else if (yyyy < Tyyyy) {
                checkFDate = true;
            } else {
                checkFDate = false;
            }
        } else {
            checkFDate = false;
        }
        validForm();
    });
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
                checkFinalPassword = false;
              //$("#login-btn").attr("disabled", "disabled");
            } else {
                checkFinalPassword = true;
                $("#passwordAlert").text("");
                $("#confirmPasswordAlert").text("");
                //$("#login-btn").removeAttr("disabled");
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
                checkFinalPassword = false;
                //$("#login-btn").attr("disabled","disabled");
            } else {
                checkFinalPassword = true;
                $("#passwordAlert").text("");
                $("#confirmPasswordAlert").text("");
               // $("#login-btn").removeAttr("disabled");
                $("#password").removeClass("is-invalid");
                $("#confirmPassword").removeClass("is-invalid");
            }
        } else {
            checkPass = false;
        }
    });

    checkCGUVerif();
    checkOnInput();
    checkDate();
});