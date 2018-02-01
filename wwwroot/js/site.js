$(document).ready(function() {
    GetPageData();
    ListPosts();
    GetPostLikesAndComments();


    /* $("#MenuGetPageData").click(function() {
         GetPageData();
         ListPosts();
         GetPostLikesAndComments();
     })*/
});



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
        var likes = [];
        var comments = [];
        var postDates = [];
        var idNumbersOfPosts = [];
        jQuery.each(likesAndComments.data, function(i, val) {
            likes.push(val.likes.summary.total_count)
            comments.push(val.comments.summary.total_count)
            postDates.push(val.created_time.substring(0, 10));
            idNumbersOfPosts.push(val.id);
        });

        createLikesChart(likes, comments, postDates);
        console.log("Likes");
        console.log(likes);
        console.log("Comments");
        console.log(comments);
        console.log("Dates");
        console.log(postDates);

    })
}

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


function createLikesChart(likes, comments, postDates) {
    var likeCat = likes;
    likeCat.unshift("Likes");
    var comCat = comments;
    comCat.unshift("Comments");


    var chart = bb.generate({
        bindto: "#postLikes",
        data: {

            columns: [
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

    setTimeout(function() {
        chart.transform('line', 'Likes');
    }, 1000);

    setTimeout(function() {
        chart.transform('line', 'Comments');
    }, 2000);

    setTimeout(function() {
        chart.transform('bar');
    }, 3000);

    setTimeout(function() {
        chart.transform('line');
    }, 4000);
}