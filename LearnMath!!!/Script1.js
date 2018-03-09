
$(document).ready(function () {

    $("a").click(function () {
        $(".register-form").toggle("slow")
        $(".login-form").toggle("slow")
        $(".input").val("")
        $("a").toggleClass("Celect")
    });
});
function LogError(x) {
    $(document).ready(function () {
        if (x == "1") {
            var div = $(".form");
            div.animate({ left: '+=15px' }, 15);
            div.animate({ left: '-=15px' }, 15);
            div.animate({ left: '+=15px' }, 15);
            div.animate({ left: '-=15px' }, 15);
            div.animate({ left: '+=15px' }, 15);
            div.animate({ left: '-=15px' }, 15);
            div.animate({ left: '+=15px' }, 15);
            div.animate({ left: '-=15px' }, 15);
        }
        else if(x=="2")
        {
            
            x0popup({
                title: '',
                text: 'Ο λογαριασμός σας βρίσκεται στην διαδικασία ενεργοποίησης από τους διαχειριστές.',
                icon:'info'

            })
        }
        else if(x=="3")
        {
             x0popup({
                title: '',
                text: 'Δεν μπορείτε να αποκτήσετε πρόσβαση.Επικοινωνήστε με τους Καθηγητές.',
                icon: 'warning'

            })
        }

    });
}
function RegError(x) {
    $(document).ready(function () {
        var div = $(".form");
        div.animate({ left: '+=15px' }, 15);
        div.animate({ left: '-=15px' }, 15);
        div.animate({ left: '+=15px' }, 15);
        div.animate({ left: '-=15px' }, 15);
        div.animate({ left: '+=15px' }, 15);
        div.animate({ left: '-=15px' }, 15);
        div.animate({ left: '+=15px' }, 15);
        div.animate({ left: '-=15px' }, 15);
        $(".register-form").toggle();
        $(".login-form").toggle();
        var i = 1;
        if (GetNDigit(x, i) == 1) {
            $(".MessageEmptyBox").show();
            $(".MessageEmptyBox > p").html("Συμπληρώστε όλα τα κενά πεδία και επιλέξτε έναν ρόλο.");
            $("[ID*='Reg']").filter(function () { return this.value == ""; }).css("border", "2px solid red");
        }
        i++;
        if(GetNDigit(x, i)==1)
        {
            $("input[name*='RegPass']").css("border", "2px solid red");
            $(".MessagePassBox > p").html("Ο κωδικός πρέπει να έχει από 8 και πανό χαρακτήρες");
            $(".MessagePassBox").show();
        }
        else if(GetNDigit(x, i)==2)
        {
            $("input[name*='RegPass']").css("border", "2px solid red");
            $(".arrowPass,.MessagePassBox").show();
            $(".MessagePassBox > p").html("Ο κωδικός πρέπει να είναι ο ίδιος σε αυτά τα πεδία");
        }
        i++;
        if (GetNDigit(x, i) == 1) {
            $("input[name='RegEmail']").css("border", "2px solid red");
            $(".arrowEmail,.MessageEmailBox").show();
            $(".MessageEmailBox").css("top", "26%");
            $(".MessageEmailBox > p").html("Μη έγκυρο e-mail.");
        }
        else if (GetNDigit(x, i) == 2)
        {
            $("input[name='RegEmail']").css("border", "2px solid red");
            $(".arrowEmail,.MessageEmailBox").show();
            $(".MessageEmailBox").css("top", "23%");
            $(".MessageEmailBox > p").html("Αυτό το Email ήδη χρησιμοποιείτε από κάποιον χρήστη.");
        }
    });
}
//niosto psifio
function GetNDigit(x,n)
{
    return Math.floor(x / (Math.pow(10, n - 1))) % 10;
}