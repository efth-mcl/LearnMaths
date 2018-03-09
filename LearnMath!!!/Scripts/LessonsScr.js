var fromtestb=false;
function LessonPermit(x) {
    x = x.split(',');
    if (x[0] != null)
    {
        $("#L1").html("<div class='hvr-sweep-to-right'>" + x[0] + "</div>");
    }
    var MaxL = parseInt(x[x.length - 1])
     for (var i = 1; i < x.length - 1 ; i++) {
         $(".Chapters").append("<li id='L" + (i + 1) + "'><div>" + x[i] + "</div></li>");
         if ((i + 1) <=MaxL) {
             $("#L" + (i + 1)).addClass("OK");
             $("#L" + (i + 1)+" div").addClass("hvr-sweep-to-right");
             
         }
    }
}
//Load Lesson
function LoadLesson(LID)
{   //
    $(".Lesson").load("../Lessons/Lesson" + LID + ".html");
    $(".TestB").show("slow");
}
var ID = { id: "" }

$(document).ready(function () {
    $(".OK").click(function () {
        ID.id = $(this).attr('id').substring(1);
        if (fromtestb == true) {
            LoadTest();
        }
        else {
            LoadLesson(ID.id);
        }
    });
    //Go to Test
    $(".TestB").click(LoadTest);
});
//LoadTest
function LoadTest() {
    $(".video").html(" ");
    $("#TestForm").load("Test.aspx");
    document.getElementById("TestForm").style.width = "100%";
    $.ajax({
        type: "POST",
        url: "Test.aspx/LoadTest",
        data: '{LessonID: "' + ID.id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Q = response.d;
            Qnumb = Q.length;
            $(".CheckedNumb p").html("Έχετε απαντήσει " + 0 + " απο τις " + Qnumb + " ερωτήσεις");
            UpdateMath(Q, 0);
            ANN[0] = Q[0][5];
            St_An = new Array(Qnumb);
        },
        failure: function (response) {
            $("#a1").html("Error calling Ajax");
        }
    })
}
function FromTestB()
{
    fromtestb = true;
}