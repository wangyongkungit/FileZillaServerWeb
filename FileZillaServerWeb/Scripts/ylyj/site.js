$(document).ready(function () {
    $("#lblCurrentYear").text(new Date().getFullYear());

    $("#leftsead a").hover(function () {
        $(this).children("img.hides").show();
        $(this).children("img.shows").hide();
        $(this).children("img.hides").animate({ marginRight: '0px' }, 'slow');
    },
    function () {
        $(this).children("img.hides").animate({ marginRight: '-143px' }, 'slow', function () { $(this).hide(); $(this).next("img.shows").show(); });
    })
}
);