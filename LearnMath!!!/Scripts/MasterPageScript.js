var cont_arH
function helpDivPos()
{
    
    if ($(window).scrollTop() >= $("#content_area").position().top) {
        $(".helpDiv").css("position", "fixed");
        $(".helpDiv").css("top", "10px");
    }
    else {
        $(".helpDiv").css("position", "absolute");
        $(".helpDiv").css("top", $("#content_area").position().top + "px");
    }
}

$(document).ready(function () {


    $("#content_area").css("margin-bottom", "200px");
    $(".helpDiv").css("position", "absolute");
    $(".helpDiv").css("top", $("#content_area").position().top + "px");
    $(".helpDiv").css("height", $(".helpDiv").width() + "px");
   
    $("#content_area").resize(function () {
        console.log("ok");
    })
    $(".ScrollTop").css("height", $(".ScrollTop").width() + "px");
    $(window).resize(function () {
        //HelpB
        $(".helpDiv").css("height", $(".helpDiv").width() + "px");
        helpDivPos();

        $(".ScrollTop").css("height", $(".ScrollTop").width() + "px");
    });

    $(window).scroll(function () {
        //HelpB
        helpDivPos();

        ///Gotop
        if($(window).scrollTop()-$(window).height()>=250)
        {
            $(".ScrollTop").fadeIn("slow")
        }
        else
        {
            $(".ScrollTop").fadeOut("fast")
        }
    });
    $(".ScrollTop").click(function () {
        $('html, body').animate({ scrollTop: 0 }, 'fast');

    });
});