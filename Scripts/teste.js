function MyFunction() {
    var model = document.getElementById("textAreaProject").title;
    var id = document.getElementById("textAreaProject").da;
    $.ajax({
        url: this.href,
        type: 'POST',
        //contentType: 'application/json; charset=utf-8',
        data: {model},
        success: function(result) {

        }
    });
    return false;
}