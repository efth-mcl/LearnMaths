function LessonPermit(x) {
    x = x.split(',');
    if (x[0] != null) {
        $("#L1").html("<div class='hvr-sweep-to-right'>" + x[0] + "</div>");
    }
    var MaxL = parseInt(x[x.length - 1])
    for (var i = 1; i < x.length - 1 ; i++) {
        $(".Chapters").append("<li id='L" + (i + 1) + "'><div>" + x[i] + "</div></li>");
        if ((i + 1) <= MaxL) {
            $("#L" + (i + 1)).addClass("OK");
            $("#L" + (i + 1)).addClass("Stats");
            $("#L" + (i + 1) + " div").addClass("hvr-sweep-to-right");
        }
    }
}

$(document).ready(function () {
    $(".Stats").click(function () {
       
        var ThisID = $(this).attr('id');
        console.log(ThisID);
        $.ajax({
            type: "POST",
            url: "StudStats.aspx/GraphMethod",
            data: '{MethodID: "' + ThisID + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#Canvas").show();
                if (ThisID == "MaxHist") {
                    var HistMax = new HIST("Canvas", 500);
                    console.log(response.d[0]);
                    HistMax.graph.Data.DataX = response.d[0];
                    HistMax.graph.Data.DataY = response.d[1];
                    HistMax.PLOT_HISTOGRAM();
                    HistMax.SetHistMessage();
                    $(window).resize(function () { UpdateHist(HistMax) });
                }
                else if (ThisID.slice(0, 1) == "L") {
                    console.log(response.d[0].length);
                            if (response.d[0].length>=2) {
                                var TS = new TimeSires("Canvas", 500);
                                TS.graph.Data.DataY = response.d[1];
                                TS.graph.Data.DataX = response.d[0];
                                TS.PLOT_TimeSiries();
                                TS.SetTimeMessage();
                                $(window).resize(function () { UpdateTimeSiries(TS) });
                            }
                            else
                            {
                                document.getElementById("Canvas").height = 500;
                                document.getElementById("Canvas").width = $("#Canvas").parent().width();
                                var ctx = document.getElementById("Canvas").getContext("2d");
                                ctx.font = "30px Arial";
                                $(".MessageDivCanvas").remove();
                                if (response.d[0].length == 1) {
                                    ctx.fillText("Η πρόοδος προβάλλεται για αριθμό προσπαθειών πάνω από 2", 200, 200);
                                }
                                else {
                                    ctx.fillText("Δεν υπάρχουν ακόμα δεδομένα για να προβληθούν", 200, 200);
                                }
                            }
                }
            },
            failure: function (response) {
                $("#a1").html("Error calling Ajax");
            }
        })
    });

    $("#LessonsTime").click(function ()
    {
        $(".Chapters").toggle("fast");
        $("#Canvas").hide();
        
    })
});