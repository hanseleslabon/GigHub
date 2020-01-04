var GigsController = function (attendanceService) {

    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    }


    var toggleAttendance = function (e) {
        button = $(e.target);
        if (button.hasClass("btn-default")) {
            attendanceService.createAttendance(button.attr("data-gig-id"), done, fail);
        }
        else {
            attendanceService.deleteAttendance(button.attr("data-gig-id"), done, fail);
        }
    }

    var fail = function () {
        alert("Something Failed");
    }

    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }


    return {
        init: init
    }

}(AttendanceService);