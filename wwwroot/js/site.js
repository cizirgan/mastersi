$(document).ready(function() {
    $("#MenuGetPageData").click(function() {
        GetPageData();
        ListPosts();
    })
});

function ListPosts() {
    $.getJSON("/Facebook/GetPosts", function(json) {
            var posts = JSON.parse(json);
            console.log(posts.posts.data);
            jQuery.each(posts.posts.data, function(i, val) {
                document.getElementById("PostsTable").insertRow(-1).innerHTML = '<td>' + val.created_time.substring(0, 10) + '</td><td>' + val.message + '</td>';
            });
        })
        .fail(function() {
            console.log("error");
        });
}

function GetPageData() {
    $.getJSON("/Facebook", function(json) {
            var sayfaBilgileri = JSON.parse(json);
            $("#PageID").text(sayfaBilgileri.Id);
            $("#PageName").append(sayfaBilgileri.Name);
            $("#PageUserName").append(sayfaBilgileri.Username);
            $("#PageAbout").text(sayfaBilgileri.About);
            $("#PageFollowers").text(sayfaBilgileri.Fan_count);
            console.log(sayfaBilgileri);
            console.log(sayfaBilgileri.name);

        })
        .fail(function() {
            console.log("error");
        });
}





var chart = bb.generate({
    bindto: "#chart",
    data: {
        type: "bar",
        columns: [
            ["data1", 30, 200, 100, 170, 150, 250],
            ["data2", 130, 100, 140, 35, 110, 50]
        ]
    }
});