$(document).ready(function () {
    $(".editCard").click(function () {
        $(this).parent().parent().find(".AfficheGeneral").attr("hidden", "hidden");
        $(this).parent().parent().find(".EditGeneral").removeAttr("hidden");

        $(".editBTN").attr("hidden", "hidden");
        $(this).parent().parent().find(".discardBTN").removeAttr("hidden");
    });

    $(".discardBTN").click(function () {
        $(this).parent().parent().find(".EditGeneral").attr("hidden", "hidden");
        $(this).parent().parent().find(".AfficheGeneral").removeAttr("hidden");

        $(".editBTN").removeAttr("hidden");
        $(this).parent().parent().find(".discardBTN").attr("hidden", "hidden");
    });
});