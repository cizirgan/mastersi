$(document).ready(function() {

    getDataForOnePost();


});

function getDataForOnePost() {
    var postID = "159429424081052_1820695551287756";
    var query = "?fields=shares,likes.summary(true),comments.summary(true)";
    var allQuery = postID + query;
    $.ajax({
        url: '/GraphApiTest/GetDataWithParameter',
        type: 'GET',
        data: {
            'parameter': allQuery
        },
        dataType: 'json',
        success: function(data) {

            var dataNew = JSON.parse(data);
            console.log(dataNew.shares.count)
            console.log(dataNew.likes.summary.total_count);
            console.log(dataNew.comments.summary.total_count);

            var likes = ["Likes", dataNew.likes.summary.total_count];
            var shares = ["Shares", dataNew.shares.count];
            var comments = ["Comments", dataNew.comments.summary.total_count];

            var toplam = [];
            toplam.push(likes);
            toplam.push(shares);
            toplam.push(comments);

            console.log(toplam);
            grafikCiz(toplam);
        }
    });
};

function grafikCiz(dataMehmet) {
    google.charts.load('current', { 'packages': ['corechart'] });

    // Set a callback to run when the Google Visualization API is loaded.
    google.charts.setOnLoadCallback(drawChart);

    // Callback that creates and populates a data table,
    // instantiates the pie chart, passes in the data and
    // draws it.
    function drawChart() {

        // Create the data table.
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Topping');
        data.addColumn('number', 'Slices');
        data.addRows(dataMehmet);

        // Set chart options
        var options = {
            'title': '159429424081052_1820695551287756',
            'width': 400,
            'height': 300
        };

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
        chart.draw(data, options);
    }
}