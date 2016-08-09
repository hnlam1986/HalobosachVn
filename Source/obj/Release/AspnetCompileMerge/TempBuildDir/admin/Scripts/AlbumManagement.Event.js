var AlbumManagementEvent = {
    DeleteAlbum: function (button) {
        var id = $(button).attr("data-albumId");
        var folder = $(button).attr("data-folder");
        $("#album_indicator_" + id).css("display", "");
        $.ajax({
            type: "POST",
            url: "/ajax.aspx?action=deletealbum",
            dataType: 'text',
            data: { "id": id, folder: folder },
            success: function (data) {
                if (data == 'True') {
                    $("#Album_" + id).remove();
                }
            },
            error: function (msg) {
                this.result = false;
            }
        });
    },
    DeleteVideo: function (button) {
        var id = $(button).attr("data-videoid");
        $("#video_indicator_" + id).css("display", "");
        $.ajax({
            type: "POST",
            url: "/ajax.aspx?action=deletevideo",
            dataType: 'text',
            data: { "id": id },
            success: function (data) {
                if (data == 'True') {
                    $("#Video_" + id).remove();
                }
            },
            error: function (msg) {
                this.result = false;
            }
        });
    }

};