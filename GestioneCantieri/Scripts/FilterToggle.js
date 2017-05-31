$(function () {
    $("#filterContainer").show();
    $("#filterToggle").html("Nascondi Filtri");
    $("#filterToggle").click(function () {
        var link = $(this);

        $("#filterContainer").toggle("fast", function () {
            if ($(this).is(':visible'))
                link.text("Nascondi Filtri");
            else
                link.text("Mostra Filtri");
        });
    });
});