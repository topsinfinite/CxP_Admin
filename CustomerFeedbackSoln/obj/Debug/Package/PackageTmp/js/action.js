// JavaScript Document

$(document).ready(function(e) {
	bodyHeight = $('body').height();
	loginHeight = $("#landing-page form.login").height()
//	alert(bodyHeight +","+ loginHeight)
	heightDiff = bodyHeight - loginHeight;
	$("form.login").css('margin-top', (heightDiff/2));

});