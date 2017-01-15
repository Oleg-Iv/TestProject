var rsa = new System.Security.Cryptography.RSACryptoServiceProvider(2048);

var pablicKey;
var privateKey;

var isKeyReady = false;
function generateKeys() {
    isKeyReady = true;
     pablicKey = rsa.ToXmlString(false);
     privateKey= rsa.ToXmlString(true);

    console.log("privateKey \n" + privateKey);
    console.log("pablicKey \n" + pablicKey);
}


$(function () {
	generateKeys();
	$('#StartEnctipted').click(function () {

		if (isKeyReady == false) {
			alert("Kay is not ready, please wait.");
			return;
		}

		$.ajax({
			type: "POST",
			url: "/Home/GetKey",
			data: { pablicKey: pablicKey },
			dataType: "text",
			success: function (data) {
				console.log(data);

				var decryptedBytes = rsa.Decrypt(System.Convert.FromBase64String(data), true);
			    // ------------------------------------------------
				// Display the decrypted data.
				console.log(decryptedBytes);

				var decryptedString = System.Text.Encoding.UTF8.GetString(decryptedBytes);

				$("#random").val(decryptedString);
				var encrypted = CryptoJS.AES.encrypt($("#ustring").val(), decryptedString);
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