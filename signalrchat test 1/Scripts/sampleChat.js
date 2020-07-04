var lstMessages = [
];
var hub = {};
$(function () {
     hub = $.connection.chatHub;
    hub.client.broadcastConnections = function (allConnections) {
        $("#Connections").html('');
        delete allConnections[$.connection.hub.id];
        for (var key in allConnections) {
            var append = "<div class='connection' onclick='OpenChatBox(this)'>\
                <span id='connectionName'>"+ allConnections[key] + "</span>\
                <input type='hidden' id='connectionId' value='"+ key + "'/>\
                </div>";
            $("#Connections").append(append);
        }
    };

    //hub.client.broadcastSelfMessage = function (from, message) {

    //    $("#messages").append(initmsg(from.Name, message));
    //};
    hub.client.broadcastMessage = function (from, message) {
        $("#chatBox").css('display', '');
        if ($("#partnerConnectionId").val() != null || $("#partnerConnectionId").val() != "") {
            if (from.Id == $("#myConnectionId").val()) {
                var a = 0;
            }
            else if (from.Id != $("#partnerConnectionId").val()) {
                $("#messages").html('');
                $("#partnerConnectionId").val(from.Id);
                GetMessages();
            }
        }
        if (from.Id != $.connection.hub.id) {
            $("#partnerConnectionId").val(from.Id);
            $("#partnerConnectionName").val(from.Name);
        }    
        
        $("#messages").append(initmsg(from.Name, message));
        SetMessages();
        console.log(lstMessages);
    };
    hub.connection.start().done(function () {
        var connectionname = prompt();
        var Url = window.location;
        var UserId = Url.pathname.split('/');
        hub.server.setConnectionName(connectionname, UserId[UserId.length - 1]);
        $("#myConnectionId").val($.connection.hub.id);
        $("#myConnectionName").val(connectionname);
        $("#inmessage").keypress(function (e) {
            if (e.which == 13) {
                var partnerId = $(this).closest('#chatBox').find('#partnerConnectionId').val();  
                var message = $(this).val();
               
                hub.server.send(partnerId, UserId[UserId.length-1], message);
                $(this).val('');
            }
        });
    });
    
});

function SetMessages() {
    var uniquekey = $("#myConnectionId").val() + $("#partnerConnectionId").val();
    var obj = [];
    $("#messages .message").each(function (i, dv) {
        var user = $(dv).find('.usr').text();
        var message = $(dv).find('.msg').text();
        obj.push({ user: user, message: message });
    });
    var f = 0;
    for (var i = 0; i < lstMessages.length; i++) {
        if (lstMessages[i].title == uniquekey) {
            lstMessages[i].obj = obj;
            f = 1;
        }
        //else {
        //    lstMessages.push({ title: uniquekey, obj: obj });
        //}
        if (i == lstMessages.length-1 && f==0) {
            lstMessages.push({ title: uniquekey, obj: obj });
        }
    }
    if (lstMessages.length == 0) {
        lstMessages.push({ title: uniquekey, obj: obj });
    }
}
function GetMessages(item) {
    var uniquekey = $("#myConnectionId").val() + $(item).find("#connectionId").val();
    if (lstMessages == undefined || lstMessages.length == 0) {
        var connectionId = $("#myConnectionId").val();
        var partnerConnectionId = $(item).find("#connectionId").val();
       
        var Messages = hub.server.getMessageFromServer(connectionId, partnerConnectionId);
        p(Messages, item);
        //Messages.promise().done(function (arg1) {            
        //    lstMessages = arg1;    
        //});
    }
    for (var i = 0; i < lstMessages.length; i++) {
        if (lstMessages[i].title == uniquekey) {
            for (var x = 0; x < lstMessages[i].obj.length; x++) {
                $("#messages").append(initmsg(lstMessages[i].obj[x].user, lstMessages[i].obj[x].message));
            }
            break;
        }
    }
}
function p(Messages, item) {
    Messages.promise().done(function (arg1) {
        lstMessages = arg1;
        GetMessages(item);
    });
    return;
}
function OpenChatBox(item) {
    var connectionId = $(item).find("#connectionId").val();
    var connectionName = $(item).find("#connectionName").text();
    $("#chatBox").css('display', '');
    $("#messages").html('');
    $("#partnerConnectionId").val(connectionId);
    $("#partnerConnectionName").val(connectionName);
    GetMessages(item);
}
function initmsg(name, message) {
    return "<div class='message'>\
                <b class='usr'>"+ name +"</b>\
                <span class='msg'>"+ message + "</span>\
                   </div>";
}