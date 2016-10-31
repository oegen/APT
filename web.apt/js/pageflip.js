$("#pageflip").hover(function() { //On hover...
	$("#pageflip img").stop()
		.animate({ //Animate and expand the image and the msg_block (Width + height)
			width: '102px',
			height: '102px'
		}, 290);	
		
		$(".msg_block").stop()
		.animate({ //Animate and expand the image and the msg_block (Width + height)
			width: '93px',
			height: '90px'
		}, 300);
		
	} , function() {
	$("#pageflip img").stop() //On hover out, go back to original size 50x52
		.animate({
			width: '60px',
			height: '62px'
		}, 220);
	$(".msg_block").stop() //On hover out, go back to original size 50x50
		.animate({
			width: '55px',
			height: '55px'
		}, 200); //Note this one retracts a bit faster (to prevent glitching in IE)
});