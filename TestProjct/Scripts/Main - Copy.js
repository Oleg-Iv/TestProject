var pablicKey;// ="MIIBITANBgkqhkiG9w0BAQEFAAOCAQ4AMIIBCQKCAQEAqt1D938gx9vFh3Ab/zMzXWohiqpds/riE6Oil/eEkH0pkeov8PcmtC8MggqaXLuq02Zb0dfTqiVDuZO3yzl9jVTyPAcKiTPLcbY/cAb+0QdafZcXUpdvqWG1PmJaLZCcfNn4pcJbSVsnjX5ivFLulu+lPkzk2F/T8ZoEwtFcxeSrlhkk+NreL/LLoiEY5PP+190wnxFb21uYxGgUGs+ux3K24wDJ8k73580/vq84GtwNJ+ffNkuzt2Fn4VEQWYO4hSSV4uw9ClmxnzT+AiwplW8WnUfdH59zwiCaNN2BAmO9hf0hYNJFDuOgKGIu1a5Ekva0U6GImCkaI2vXbGxB0wICAQE=";
var privateKey;// = "MIIEogIBAAKCAQEAqt1D938gx9vFh3Ab/zMzXWohiqpds/riE6Oil/eEkH0pkeov8PcmtC8MggqaXLuq02Zb0dfTqiVDuZO3yzl9jVTyPAcKiTPLcbY/cAb+0QdafZcXUpdvqWG1PmJaLZCcfNn4pcJbSVsnjX5ivFLulu+lPkzk2F/T8ZoEwtFcxeSrlhkk+NreL/LLoiEY5PP+190wnxFb21uYxGgUGs+ux3K24wDJ8k73580/vq84GtwNJ+ffNkuzt2Fn4VEQWYO4hSSV4uw9ClmxnzT+AiwplW8WnUfdH59zwiCaNN2BAmO9hf0hYNJFDuOgKGIu1a5Ekva0U6GImCkaI2vXbGxB0wICAQECggEAd6vn4iM/zLnNFiSvAG+QjSF+F2hhgQtbmjTZeWCojQr2RFRVYA25fTPg6oTvmZpZwuEXeBOtJnu35pVH8vF6zJMnKwnpfvlxmlnKz/T2NMRnORUnPtOm3Dx2+ORDJOTJPoq4HHA395x7VCi0t7KdJPuEqyLt+ik3zhI5H9tZ4UJfWQlVwOXFSOFss5C1CNSET0ZJ2TDtn0+rueLNaiIlc4tFlimzBBpZKJyj5W1YT+Si87SH6pE5VNUd3dukMU9+4ICcYe6BXJ/BrSwyjNkGWOc3pH9pgQLbDvFigw6kNQaDtx6xaf6BIV/S8kvwKQuPRg6pd0OnA2frzehJAu/4gQKBgQDhVF4nssDyULOposi4tjji+xjkU/7yoL4B5QMM3PoQMUNSWTTidf8cYHt4pmFaIgkXAxnivcgVXioOMhRd2EM2zSM3qvUVj4iwAIRRKfIq7+L4QBZCWyzFQTgnsOejGyYgyQXYF0MbGKv8bs8ymbMVCiBqufTlnKpn927j5ZU58QKBgQDCHwzzMUOjQpEwClf8/pFe9B0Y5gm1PvoPMm9tVTufKK47fsZpUA3LKcd2sAWoak9dIQaKDpT7bOTvCqanXPsX8cblVs615BS7UK66AOev5TDsyRVJ0fAGzbTH/uJiYqSqoi2Yx450b8+JB3Nczi2GhvG1geFgnQgvt8aiCcHUAwKBgQCk1SAFIiOOZN8RggleR4o8c/JVWFbjSsDIm5Kqx4AjwG0DdgzJhSI2oDpyKAd2ukRofck5ShTa1lCR2uwI4e9s0dQI2LNIjNuFuqaFFbgfaMHsYq2rD+T74AU37fwfqEe4W0UFq2neCBY3KjXzNanZmdX4aCew7h8FVGEOTktLQQKBgEV9qvxpSwOoQ+laXUGp0mu0Bm6EJ1XM1Qhtsljxr711KXPtoawsmFymXVFV7Bxpyrq9IThk2LGpeM0auSr6b8zOFWUGLB1AtryUamB322fOjwYSMXUSX+KPXZpFVAdUHw5EBF6JLgPUWjsDqcuOidKF+ORWVqgQXpKTd+pRMHe5AoGAECTC7KnlBrOdjl2CqtTautEOI2etZulxTWrx4yG/nIdAANkpao0as0cZd8cIqAVDOFA/sZBJ/5bYF6FJIyukB575Ub5UZ6DViqbbLAR3fEXm9UJTdEKHO3xlCxB+5C7uychzrSKf3abiixHKu/V3+EV3hwBOdsNikSTTdKa1/fE=";
var crypt = new JSEncrypt({ default_key_size: 2048, default_public_exponent: '010001', log: true });

var isKeyReady = false;
function generateKeys() {
	crypt.getKey();
	dt = new Date();
	//time += (dt.getTime());
	//$('#time-report').text('Generated in ' + time + ' ms');
	privateKey =crypt.getPrivateKeyB64();
	pablicKey = crypt.getPublicKeyB64();
	var t = crypt.exponent;

	console.log("privateKey \n" + privateKey);
	console.log("pablicKey \n" + pablicKey);
	console.log("exponent \n" + crypt.default_public_exponent);
	isKeyReady = true;
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
				var decrypted = crypt.decrypt(data);

				$("#random").val(decrypted);
				
				var encrypted = CryptoJS.AES.encrypt($("#ustring").val(), decrypted);
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