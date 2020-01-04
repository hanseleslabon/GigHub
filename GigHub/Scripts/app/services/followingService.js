var FollowingService = function () {

    var followArtist = function (followeeId, done, fail) {
        $.post("/api/following", { FolloweeId: followeeId })
            .done(done)
            .fail(fail);
    }

    var unfollowArtist = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/following/" + followeeId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    }

    return {
        followArtist: followArtist,
        unfollowArtist: unfollowArtist
    }
}();