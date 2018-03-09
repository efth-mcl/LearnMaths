
//Load Test
var Q=null;
var Qcount = { cnt: 0 };
var myVar;
var ANN=[];
var Qnumb;
var St_An
//clearInterval(id);
var click = false;
function Start() {
    click = false;
    var d0 = new Date();
    d0 = d0.getTime();
    var t = 0;
    Total = 1000;
    Total = Total + 2;
    myVar = setInterval(function () { myTimer() }, 1000);
    function myTimer() {
        var d1 = new Date();
        d1 = d1.getTime();
        t = Math.round(Total - (d1 - d0) / 1000);
        if (click == true || $('#Clock').is(":hover")) {
            document.getElementById("Clock").innerHTML = t;
        }
        //if (t == 0) {
        //    EndTest()
        //}
        //load in Tresults
        if (t == Total - 3) {
            var I = St_An.length - 1;
            $("#D0").html(Q[0][0]);
            $("#D0").next().next().html(findfromABCD(Q[0][5], 0));

            for (var i = 1; i <= I; i++) {
                $(".Tresult").append("<div id=D" + i + " class='Re'></div><div class='Re Stud'></div><div class='Re Ans'></div>");
                $("#D" + i).html(Q[i][0]);
                $("#D" + i).next().next().html(findfromABCD(Q[i][5], i));
                ANN[i] = Q[i][5];
                MathJax.Hub.Queue(["Typeset", MathJax.Hub]);
            }
        }
    }
}
$(document).ready(function () {
    $(".block").click(function () {
        $(".block").removeClass("select");
        $(this).addClass("select");
        St_An[Qcount.cnt] = this.id;

    });
    $("#Clock").click(function () {
        click = !click;
        $(this).toggleClass("ClickClock");
    });
    $("#Clock").hover(function () {

        $(this).css("height", "10%");
        $(this).css("width", "10%");

    }, function () {
        if (click == false) {
            $(this).css("border", "");
            $(this).css("height", "5%");
            $(this).css("width", "5%");
            document.getElementById("Clock").innerHTML = "Time";
        }
    });
});
$("#NQ").click(function () {
    if (CheckNull(St_An)) {
        NextQ();
    }
    else {
        EndTest()
    }

});
function NextQ() {

    Qcount.cnt++;
    Qcount.cnt = Qcount.cnt % Qnumb;

    UpdateMath(Q, Qcount.cnt);

    function NotNull(St_An) {
        return St_An != null;
    }


    //Έχετε απαντήσει 0 από τις 5 ερωτήσεις

    $(".CheckedNumb p").html("Έχετε απαντήσει " + St_An.filter(NotNull).length + " απο τις " + St_An.length + " ερωτήσεις");
    $(".block").removeClass("select");
    if (St_An[Qcount.cnt] != undefined) {
        $("#" + St_An[Qcount.cnt]).addClass("select");
    }

}
////MathJax
UpdateMath = function (Q, cnt) {

    $("#Quetion").html(Q[cnt][0]);
    $("#A").html(Q[cnt][1]);
    $("#B").html(Q[cnt][2]);
    $("#C").html(Q[cnt][3]);
    $("#D").html(Q[cnt][4]);
    MathJax.Hub.Queue(["Typeset", MathJax.Hub]);
}
//checkNull
function CheckNull(Array) {
    for (var i = 0; i < Array.length; i++) {
        if (Array[i] == null) {
            return true;
        }
    }
    return false;
}

function findfromABCD(X, i) {
    if (X == "A")
        return Q[i][1];
    else if (X == "B")
        return Q[i][2];
    else if (X == "C")
        return Q[i][3];
    else if (X == "D")
        return Q[i][4];
}
function EndTest() {
    try{
        document.getElementById("LessonID").value = ID.id;
    } catch (err)
    {
        document.getElementById("LessonID").value = -1;
    }
    var Score=0
    for (var i = 0; i < St_An.length; i++)
    {
        if(St_An[i]==ANN[i])
        {
            Score++;
        }

        $("#D" + i).next().html(findfromABCD(St_An[i], i));
      
    }
    MathJax.Hub.Queue(["Typeset", MathJax.Hub]);
    console.log(St_An);
    console.log(ANN);
    console.log(Score);
    Score = Math.round(Score / St_An.length * 10000) / 100;
   
    document.getElementById("Score").value = Score;

    
    $(".Score").html("Σκορ : " + Score + "\%");
    $(".TestForm,.CheckedNumb").toggle("fast");
    $(".Tresult").toggle();
    sleep(2000);
    for (var i = 0; i < St_An.length; i++) {
        var MaxH = Math.max($("#D" + i).height(), $("#D" + i).next().height(), $("#D" + i).next().next().height());
        console.log("MaxH= " + MaxH);
        $("#D" + i).height(MaxH);
        $("#D" + i).next().height(MaxH);
        $("#D" + i).next().next().height(MaxH);
    }
    var lastDiv = $("#D" + (St_An.length - 1));
    if ((lastDiv.position().top + lastDiv.height() + 70) > $(".Tresult").height()) {
        $(".TestView").css("overflow-y", "visible");
        $(".Tresult").css("margin-bottom", "2%");
        $(".Tresult").height((lastDiv.position().top + lastDiv.height()*2+120));
    }
}
function sleep(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            break;
        }
    }
}