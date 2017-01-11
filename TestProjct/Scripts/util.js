
$(function() {
	chat = $.connection.mainHub;

	chat.client.onNewUserConnected = function(id, name) {
		AddUser(id, htmlEncode(name));
	}

	chat.client.onUserDisconnected = function(id) {

		$('#' + id).remove();
	}
	chat.client.addMessage = function(name, message) {
		if (message === ReloadMassegeConst)
			window.location.href ="/User/logout";
		else
			$("#info").append(function () {
				$(this).append("<div class='alert alert-info'><strong>Admin! </strong>" + message + "</div>");
				$(this).find(".alert-info").delay(2000).hide(300, function () { $('.alert-info').remove(); }).animate({ width: '0' }, 0);
			});
	}

	//
	$.connection.hub.start().done(function() {
		chat.server.connect(name);
		ButtonClickLogoutUser();
	});

});

function ButtonClickLogoutUser() {
	$('li button').click(function () {
		if ($(this).attr("SendType") === "Logout") {
			chat.server.send($(this).parent().attr('id'), ReloadMassegeConst);
			$(this).parent().remove();
		} else
			chat.server.send($(this).parent().attr('id'), $('#message').val());

	});
}

function AddUser(id, name) {
	//currUser = name;
	if ($('#' + id).length === 0)
		$("#chatusers").append('<li class="list-group-item" id="' + id + '">' + htmlEncode(name) + ' <button class="btn btn-danger" SendType="Logout">Logout</button> <button class="btn btn-info" SendType="Messege">Messege</button> </li>');
	ButtonClickLogoutUser();
}

function htmlEncode(value) {
	var encodedValue = $('<div />').text(value).html();
	return encodedValue;
}
