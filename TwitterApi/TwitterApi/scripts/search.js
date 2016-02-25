var feedArray;
var tweets;
var listMaxId = [];
var maxId;
var userName = "POSSIBLE";
var tweetNotFoundMessage = "Sorry, but your search did not return any results";
var maxLengthSearchPhrase = 25;

var DIRECTION = {
    Next: 0,
    Back: 1
}
var errorMessage = {
    EmptySearchQuery: 0,
    TooLongSearchQuery: 1
}

$(window).load(function () {
    $(".loader").fadeOut("slow");
})

$(document).ready(function () {
    if (document.cookie.indexOf("feed") >= 0) {
        feedArray = JSON.parse($.cookie("feed"));
        showFeed(feedArray);
    }
    else {
        getFeed();
    }

    setInterval(function () { getFeed() }, 36000000);

    $("#search-text").keyup(function () {
        hideErorMessage();
        var charactersLength = $("#search-text").val().length;
        if (charactersLength > maxLengthSearchPhrase) {
            showErrorMessage(errorMessage.TooLongSearchQuery);
            var enteredText = $("#search-text").val().substr(0, maxLengthSearchPhrase);
            $("#search-text").val(enteredText);
        }
        else if (charactersLength == 0) {
            showErrorMessage(errorMessage.EmptySearchQuery);
        }
    });

    $("#search-button").click(function () {
        if ($("#search-text").val().length) {
            hideErorMessage();
            cleanResultContainer();
            showResultContainer();
            getTwits();
            hidePrevious();
        }
        else {
            showErrorMessage(errorMessage.EmptySearchQuery);
            return;
        }
    });

    $(".next").click(function () {
        cleanResultContainer();
        if (tweets && Array.isArray(tweets)) {
            listMaxId.push(tweets[0].statusId);
            var maxId = tweets[tweets.length - 1].statusId;
            getTwits(maxId, DIRECTION.Next);
            showPrevious();
        }
    });

    $(".previous").click(function () {
        cleanResultContainer();
        var maxId = listMaxId.pop();
        getTwits(maxId, DIRECTION.Back);
        if (!listMaxId.length) {
            hidePrevious();
        }
    });
})

var getTwits = function (maxId, direction) {
    preloaderOn();
    var query = $("#search-text").val();
    $.ajax({
        url: "/twitter/getTweetsByQuery",
        type: "get",
        dataType: "json",
        data: { query: query, maxId: maxId, direction: direction },
        success: function (data) {
            tweets = data;
            showResult(data);
            preloaderOff();
        }
    });
}

var showResult = function (data) {
    showResultContainer();
    if (data.length) {
        for (var i = 0; i < data.length; i++) {
            showTweetRow(data[i]);
        }
        if (data.length < 10 && !listMaxId.length) {
            hidePagination();
            return;
        }
        showPagination();
        if (data.length < 10 && listMaxId.length) {
            hideNext();
        }
        else {
            showNext();
        }
    }
    else {
        if (!listMaxId.length) {
            hidePagination();
        }
        else {
            hideNext();
        }
        $(".results-container>.twitter-feed").append(tweetNotFoundMessage);
    }
}

var showTweetRow = function (rowData) {
    var tweetRow = $("<div class='tweet'>");
    var date = dateFormat(rowData.tweetDate);
    var tweet = anchorme.js(rowData.tweetText);
    tweetRow.append($("<p>" + tweet + "</p>" + "<p class='tweet-datetime'> Posted by @" + rowData.userName + " on " + date + "</p>"));
    $(".results-container>.twitter-feed").append(tweetRow);
}

var getFeed = function () {
    $.ajax({
        url: "/twitter/getTweetsByUser",
        type: "get",
        dataType: "json",
        data: { screenName: userName },
        success: function (data) {
            showFeed(data);
            setCoockies(data);
        }
    });
}

var showFeed = function (feedArray) {
    $(".tweet-heading").append(feedArray[0].userName);
    cleanFeedContainer();
    for (var i = 0; i < feedArray.length; i++) {
        showFeedRow(feedArray[i]);
    }
}

var showFeedRow = function (feed) {
    var feedRow = $("<div class='tweet'>");
    var date = dateFormat(feed.tweetDate);
    var tweet = anchorme.js(feed.tweetText);
    feedRow.append($("<p>" + tweet + "</p>" + "<p class='tweet-datetime'>Posted on " + date + "</p>"));
    $("#sidebar>.twitter-feed").append(feedRow);
}

var setCoockies = function (data) {
    var expireTime = new Date();
    expireTime.setTime(expireTime.getTime() + (3600 * 1000));
    $.cookie.json = true;
    $.cookie("feed", data, { expires: expireTime });
}

var dateFormat = function (data) {
    var dateByMask = Date.parse(data);
    return dateByMask = $.format.date(dateByMask, "D MMMM yyyy at hh.mmp");
}

//helpers
var showErrorMessage = function (message) {
    $("#search-text").addClass("validationError");
    if (message == errorMessage.EmptySearchQuery) {
        $("#errorRequiredMessage").show();
    }
    else {
        $("#errorMaxLehgth").show();
    }
}
var hideErorMessage = function () {
    $("#search-text").removeClass("validationError");
    $(".errorMessage").hide();
}
var showPrevious = function () {
    $(".previous").show();
}
var hidePrevious = function () {
    $(".previous").hide();
}
var showNext = function () {
    $(".next").show();
}
var hideNext = function () {
    $(".next").hide();
}
var showPagination = function () {
    $(".pagination").show();
}
var hidePagination = function () {
    $(".pagination").hide();
}
var cleanResultContainer = function () {
    $(".results-container>.twitter-feed").empty();
}
var showResultContainer = function () {
    $(".tweet-search-results").show();
}
var cleanFeedContainer = function () {
    $("#sidebar>.twitter-feed").empty();
}
var preloaderOn = function () {
    $(".results-container>.twitter-feed").block();
}
var preloaderOff = function () {
    $(".results-container>.twitter-feed").unblock();
}
