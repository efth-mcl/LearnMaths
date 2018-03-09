$(document).ready(function () {
    ///NEWS/////
    $(".rmM").click(function () {
        var parent = $(this).parent();
        console.log(parent.attr("id"))
        parent.remove();
        $.ajax({
            type: "POST",
            url: "ProfesorMain.aspx/RmMessage",
            data: '{MessID: "' + parent.attr("id") + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if ($(".News").html() == "<table><tbody></tbody></table>") {
                    $(".News").html("Δεν υπάρχει κάποια καινούργια είδηση.");

                }
            }
        });
    });
    //NEWS////
});