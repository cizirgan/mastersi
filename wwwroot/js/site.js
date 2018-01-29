$(document).ready(function() {
    $("#MenuGetPageData").click(function() {
        GetPageData();
        ListPosts();
        GetPostLikesAndComments();
    })
});

function ListPosts() {
    $.getJSON("/Facebook/GetPosts", function(json) {
            var posts = JSON.parse(json);
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
            console.log(sayfaBilgileri.Name);

        })
        .fail(function() {
            console.log("error");
        });
}

function GetPostLikesAndComments() {
    $.getJSON("/Facebook/GetPostLikesAndComments", function(likesCommentsData) {
            var likesAndComments = JSON.parse(likesCommentsData);
            console.log(likesAndComments);
            var likes = [];
            var comments = [];
            var postDates = []
            jQuery.each(likesAndComments.data, function(i, val) {
                likes.push(val.likes.summary.total_count)
                comments.push(val.comments.summary.total_count)
                postDates.push(val.created_time.substring(0, 10));
            });
            console.log(likes);
            createLikesChart(likes, comments, postDates);
        })
        .fail(function() {
            console.log("error");
        });
}

function createLikesChart(likes, comments, postDates) {
    var likeCat = likes;
    likeCat.unshift("Likes");
    var comCat = comments;
    comCat.unshift("Comments");

    var chart = bb.generate({
        bindto: "#postLikes",
        data: {

            columns: [
                postDates,
                likeCat,
                comCat
            ],
            types: {
                Likes: "area",
                Comments: "area-spline"
            }
        },
        axis: {
            x: {
                type: "category",
                categories: postDates,
                tick: {
                    rotate: 75,
                    multiline: false
                },
                height: 130
            }
        }
    });
}