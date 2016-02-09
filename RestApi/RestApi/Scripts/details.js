var commentsCurentUser;



$("#btnBack").click(function () {
    window.location.href = "/User/List/";
});

$(document).ready(function () {
    var userId = $('#userId').val();
    preloaderOn();

    $.ajax({
        url: '/user/getPostsByUserId?userId=' + userId,
        type: "get",
        dataType: "json",
        success: function (data) {
            drawTable(data);
            commentsCurentUser = data;
            $('#allPostsCount').html(data.length);
            preloaderOff();
            $("#details").show();
        }
    });
});


var preloaderOn = function () {
    $.blockUI();
};
var preloaderOff = function () {
    $.unblockUI();
};



var drawTable = function (data) {
    for (var i = 0; i < data.length; i++) {
        drawPostRow(data[i]);
    }
}

var drawPostRow = function (rowData) {
    var postRow = $("<tr />")
    postRow.append($("<td id='post-" + rowData.id + "'>" + rowData.body + "<a class='padding-left pointer linkShow' onclick='showComments(" + rowData.id + ")'>show comments</a>" + "</td>"));
    postRow.append($("<td> <a id='post-" + rowData.id + "' class='padding-left pointer' onclick='showStatistic(" + rowData.id + ")'>show statistic</a>" + "</td>"));
    $("#userDetails").append(postRow);
}

var showComments = function (selectedPostId) {
    var commentsForCurrentPost = $("#commentsForPostId-" + selectedPostId);

    if (commentsForCurrentPost.length && !commentsForCurrentPost.hasClass("hideComment")) {
        commentsForCurrentPost.addClass("hideComment");
        var postId = '#post-' + selectedPostId + " .linkShow";
        $(postId).html('show comments');
        return;
    }

    preloaderOn();
    getPostDetails(selectedPostId, renderComments);
};

var getPostDetails = function (postId, callback) {
    var post = commentsCurentUser.filter(function (post) { return post.id == postId })[0];

    if (post != null && !post.comments) {
        $.ajax({
            url: '/user/GetPostDetailsById?postId=' + postId,
            type: "get",
            dataType: "json",
            success: function (postDetails) {
                if (postDetails != null) {
                    post.comments = postDetails.comments;
                    post.statistics = postDetails.statistics;
                }
                return callback(postId);
            }
        });
    }
    else {
        return callback(postId);
    }
}

var renderComments = function (selectedPostId) {
    var post = commentsCurentUser.filter(function (post) { return post.id == selectedPostId })[0];
    var isCommentsRendered = $('#commentsForPostId-' + selectedPostId).length;

    if (post != null && !isCommentsRendered) {
        drawComments(post);
    }

    var commentsForCurrentPost = $("#commentsForPostId-" + selectedPostId);
    commentsForCurrentPost.removeClass('hideComment');
    var postId = '#post-' + selectedPostId + " .linkShow";
    $(postId).html('hide comments');
    preloaderOff();
}

var drawComments = function (post) {
    var comments = $("<div id='commentsForPostId-" + post.id + "'></div>");
    $("#post-" + post.id).append(comments);

    for (var i = 0; i < post.comments.length; i++) {
        drawCommentsRow(post.comments[i]);
    }
}

var drawCommentsRow = function (comment) {
    var postId = "#commentsForPostId-" + comment.postId;
    var user = $("<div><a href='mailto:" + comment.email + "'>[" + comment.email + "]</a> posted comment:</div>")
    var comments = $("<div> <p class='comment'>" + comment.body + "</p></div>");
    $(postId).append(user);
    $(postId).append(comments);
}



var showStatistic = function (selectedPostId) {
    preloaderOn();
    getPostDetails(selectedPostId, renderStatistic);
};

var renderStatistic = function (selectedPostId) {
    var selectedPost = commentsCurentUser.filter(function (post) {
        return post.id == selectedPostId
    })[0];

    if (selectedPost != null) {
        $("#statisticPost").empty();
        var statisticSelectedPost = selectedPost.statistics;
        drawStatistic(statisticSelectedPost);
        $('#modalStatistic').modal('show');
    }

    preloaderOff();
};

var drawStatistic = function (data) {
    for (var i = 0; i < data.length; i++) {
        drawStatisticRow(data[i]);
    }
}

var drawStatisticRow = function (rowData) {
    var postRow = $("<tr />")
    $("#statisticPost").append(postRow);
    postRow.append($("<td>" + rowData.word + "</td>"));
    postRow.append($("<td>" + rowData.count + "</td>"));
}
