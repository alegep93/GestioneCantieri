$(function () {
    $("div.collapse.navbar-collapse ul li a").click(function () {
        $(this).parent("li").addClass("active");
    });
});