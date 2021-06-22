function read(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#changeAvatar').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#fileinput").change(function () {
    read(this);
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#postImage').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#formFile").change(function () {
    readURL(this);
});

var connection = new signalR.HubConnectionBuilder().withUrl("/Comment").build();

document.getElementById("sendComment").disabled = true;

connection.on("ReceiveComment", function (username, message, avatar, date, postid) {
    var strMessage = '' + message + '';
    if (strMessage.length > 170)
    {
        alert("Your comment message can be only less than 170 symbols");
    }
    else
    {
        var avatarSrc = '';
        if (avatar != null)
        {
            avatarSrc = 'data:image/jpeg; base64,' + avatar;
        }
        else
        {
            avatarSrc = '/images/avatar7.png';
        }
        var id = '#' + postid;
        
        $(id).prepend('<li class="comment even thread-even depth-1">' + '<article id="div-comment-2" class="comment-body">' +
            '<footer class="comment-meta">' +
            '<div class="comment-author vcard">' +
            '<a href="/Users?username=' + username + '">' +
            '<img src="' + avatarSrc + '" class="avatar avatar-32 photo" height="32" width="32" loading="lazy" />' +
            '</a>' +
            '<a href="/Users?username=' + username + '">' +
            '<b class="fn">' + username + '</b> <span class="says">says:</span>' +
            '</a>' +
            '</div>' +
            '<div class="comment-metadata">' +
            '<a><time>' + date + '</time></a>' +
            '</div>' +
            '</footer>' +
            '<div class="comment-content">' +
            message +
            '</div>' +
            '</article>' + '</li>');
    }
    

});

connection.start().then(function () {
    document.getElementById("sendComment").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendComment").addEventListener("click", function (event) {
    var id = document.getElementById("hiddenpostid").value;
    var postid = document.getElementById("hiddenid").value;
    var username = document.getElementById("hiddenuser").value;
    var message = document.getElementById("comment").value;
    connection.invoke("SendComment", username, message, postid, id).then(function () {
        document.getElementById("comment").value = "";
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


