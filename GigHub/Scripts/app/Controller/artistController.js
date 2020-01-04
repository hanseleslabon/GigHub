var ArtistController = function (followingService) {

    var button;

    var init = function () {
        $(".js-toggle-following").click(toggleFollowing);
    }

    var toggleFollowing = function (e) {
        button = $(e.target);
        var followeeId = button.attr("data-artist-id");
        if (button.text().trim() === "Follow?") {
            followingService.followArtist(followeeId, done, fail);
        }
        else {
            followingService.unfollowArtist(followeeId, done, fail);
        }
    }

    var done = function () {
        var text = (button.text() == "Following") ? "Follow?" : "Following";
        button.text(text);
    }

    var fail = function () {
        alert("Something Failed");
    }

    return {
        init: init
    }

}(FollowingService);