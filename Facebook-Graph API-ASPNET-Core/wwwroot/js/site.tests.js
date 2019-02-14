var pageUserName = "universidaddesalamanca";
//var queryParameter = "?fields=id,name,picture";
var queryParameter = "/posts?fields=created_time,message,comments.summary(true),likes.summary(true)&limit=20";

// Ultimos 5 posts
var queryParameter = "?fields=posts.limit(5){message}"

var allQuery = pageUserName + queryParameter;
$(document).ready(function() {
    $("#showDataBox").hide();
    $("#makeQueryButton").click(function() {
        GetData();
    });

});

function GetData() {
    $.ajax({
        url: '/GraphApiTest/GetDataWithParameter',
        type: 'GET',
        data: {
            'parameter': allQuery
        },
        dataType: 'json',
        success: function(data) {
            $("#showDataBox").html(data);
            $("#showDataBox").show();
        }
    });
}