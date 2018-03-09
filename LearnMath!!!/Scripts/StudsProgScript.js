var StudID = "";
var ID_Stats = "";
$(document).ready(function () {
    $(".goToStuds").click(function () {
        $(this).hide();
        $("#Canvas").hide();
        $(".stats").hide();
        $('.StudsDiv label').show();
        $('.StudsDiv label').css("cursor", "pointer");
        $(".StudsDiv input[type=radio]:checked").prop('checked', false);
        //$(".StudsDiv").css("width", "15%");
        $("h1").show();
    });
    $("input[name=radio]").click(function () {
        $("h1").hide();
        $(".goToStuds").show();
        StudID = $(this).val();
        var thislabel = $('label[for="' + $(this).attr('id') + '"]');
        //$(".StudsDiv").css("width", "80%");
        $('.StudsDiv label').css("cursor", "context-menu");
        $('.StudsDiv label').hide();
        thislabel.show();
        $(".stats").hide();
        ID_Stats = "#stats_" + $(this).attr('id');
        $(ID_Stats).show();
        console.log(StudID);
    });
    ///HIST///
    $(".MaxHist").click(function () {
        $("#Canvas").show();
        $.ajax({
            type: "POST",
            url: "StudentProgress.aspx/MaxHist",
            data: '{StudID: "' + StudID + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var HistMax = new HIST("Canvas", 500);
                console.log(response.d[0]);
                HistMax.graph.Data.DataX = response.d[0];
                HistMax.graph.Data.DataY = response.d[1];
                HistMax.PLOT_HISTOGRAM();
                HistMax.SetHistMessage();
                $(window).resize(function () { UpdateHist(HistMax) });
            },
            failure: function (response) {
                console.log("Error calling Ajax");
            }
        });
    });
    $(".L-1").click(function () {
        
        TimeSiries(-1);
    });
    $(ID_Stats + " .TimePerLesson .LessonsStatTime li").click(function () {
        
        var id = $(this).attr('class').split(' ')[0].slice(1);
        TimeSiries(id);
    });
    ///CALL TIME SIRIES///
function TimeSiries(LID) {
    $("#Canvas").show();
    $.ajax({
        type: "POST",
        url: "StudentProgress.aspx/TimeSiries",
        data: '{StudID: "' + StudID + '",LID: "' + LID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response.d[0].length);
            if (response.d[0].length >= 2) {
                var TS = new TimeSires("Canvas", 500);
                TS.graph.Data.DataY = response.d[1];
                TS.graph.Data.DataX = response.d[0];
                TS.PLOT_TimeSiries();
                TS.SetTimeMessage();
                $(window).resize(function () { UpdateTimeSiries(TS) });
            }
            else {
                document.getElementById("Canvas").height = 500;
                document.getElementById("Canvas").width = $("#Canvas").parent().width();
                var ctx = document.getElementById("Canvas").getContext("2d");
                ctx.font = "30px Arial";
                $(".MessageDivCanvas").remove();
                if (response.d[0].length==1)
                {
                    ctx.fillText("Η πρόοδος προβάλλεται για αριθμό προσπαθειών πάνω από 2", 200, 200);
                }
                 else
                {
                    ctx.fillText("Δεν υπάρχουν ακόμα δεδομένα για να προβληθούν", 200, 200);
                }
            }
        },
        failure: function (response) {
            console.log("Error calling Ajax");
        }
    });
}
});