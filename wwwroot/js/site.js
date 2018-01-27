$.getJSON("/api/Facebook", function(data) {
        $("#metin").html(data);
    })
    .fail(function() {
        console.log("error");
    });