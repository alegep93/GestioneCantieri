$(function () {
    var text = $("select.form-control option:selected").text();

    if (text != "")
        $(".ddlContainer").addClass("col-md-6");
    else
        $(".ddlContainer").addClass("col-md-offset-3 col-md-6");
});