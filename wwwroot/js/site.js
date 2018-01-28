$(document).ready(function() {
    $("#MenuGetPageData").click(function() {
        GetPageData();
    })

});

function GetPageData() {
    $.getJSON("/Facebook", function(json) {
            var sayfaBilgileri = JSON.parse(json);
            $("#PageName").append(sayfaBilgileri.name);
            $("#PageAbout").append(sayfaBilgileri.about);
            $("#PageFollowers").append(sayfaBilgileri.fan_count);
            console.log(sayfaBilgileri);
            console.log(sayfaBilgileri.name);

        })
        .fail(function() {
            console.log("error");
        });
}