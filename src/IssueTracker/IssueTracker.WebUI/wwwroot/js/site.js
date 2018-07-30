// Write your JavaScript code.
$(document).ready(function () {
    $(".command-button").click(function () {
        var value = $(this).attr("id");
        $("<input />").attr("type", "hidden")
            .attr("name", "command")
            .attr("value", value)
            .appendTo("#change-form");
        $("#change-form").submit();
    });
});