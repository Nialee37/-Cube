$(document).ready(function () {
    /*===== LINK ACTIVE =====*/
    $(".nav_link").click(function(){
        colorLink();
    })

    $("#header-toggle").click(function(){
        // show navbar
        $("#nav-bar").toggleClass('show-nav');
        // change icon
        $("#header-toggle").toggleClass('fa-times');
        // add padding to body
        $("#body-pd").toggleClass('body-pd');
        // add padding to header
        $("#header").toggleClass('body-pd');
    });

    
    setTimeout(function () {
        document.body.className = "";
    }, 500);
});


function colorLink() {
    if (linkColor) {
        linkColor.removeClass("active");
        this.addClass('active');
    }
}