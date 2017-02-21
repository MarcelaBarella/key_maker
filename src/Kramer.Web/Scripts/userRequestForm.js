$(document).ready(function () {
    $('input[name="GlobalMaster"]').click(function () {
        if ($('input[name="GlobalMaster"]').is(':checked')) {
            var radioValue = $("input[name='GlobalMaster']:checked").val();
            if (radioValue == "true") {
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
