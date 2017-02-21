$(document).ready(function () {
    $('input[name="optradio"]').click(function () {
        if ($('input[name="optradio"]').is(':checked')) {
            var radioValue = $("input[name='optradio']:checked").val();
            if (radioValue == "UsertypeGM") {
                $("#saletypedrop").prop("disabled", true);
                $("#saletypedrop").val("null");
                $("#saletypedrop").prop("required", false);
            } else {
                $("#saletypedrop").prop("disabled", false);
                $("saletypedrop").prop("required", true);
            }
        }
    });
});
