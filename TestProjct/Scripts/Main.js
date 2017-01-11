$(function () {
	$('#StartEnctipted').click(function () {
		$.ajax({
			type: "POST",
			url: "/Home/GetKey",
			dataType: "text",
			success: function (data) {
				$("#random").val(data);
				var encrypted = CryptoJS.AES.encrypt($("#ustring").val(), data);
				$.ajax({
					type: "POST",
					url: "/Home/Send",
					data: '{encrypted: "' + encrypted.toString() + '" }',
					contentType: "application/json; charset=utf-8",
					dataType: "text",
					success: function (str) {
						$("#decrypt").val(str);
					}
				});
			}
		});
	});
});