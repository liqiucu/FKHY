$(function(){
	$(".class-time").click(function(){//课时进度条
		$(".progress").css({display:"block"})
		$(".ksjd").css({display:"block"})
	})
	$(".zx-menu ul li").click(function(){//自习教室点击效果
		 $(".zx-menu ul li ").eq($(this).index()).addClass("z-cur").siblings().removeClass('z-cur');
		 $(".zx-menu ul li ").eq($(this).index()).removeClass('br-cur').siblings().addClass('br-cur');
	})
	$(".js-menu ul li").click(function(){//自习教室点击效果
		 $(".js-menu ul li ").eq($(this).index()).addClass("z-cur").siblings().removeClass('z-cur');
		 $(".js-menu ul li ").eq($(this).index()).removeClass('br-cur').siblings().addClass('br-cur');
	})
	$(".wx").click(function(){//自习课程选择效果
		$(".wx").find("img").attr('src','./images/wx.png');
		$(".wx").css({color:"#B5B5B6"})
		$(".wx").find("span").css({color:"#B5B5B6"})
		$(".yx").find("span").css({color:"#000"})
		$(this).find("img").attr('src','./images/xx.png');
		$(this).css({color:"#000"})
		$(this).find("span").css({color:"#F8C900"})
		$(".yx").find("img").attr('src','./images/yx.png');
	})
	$(".yx").click(function(){//自习课程选择效果
		$(".wx").css({color:"#B5B5B6"})
		$(".wx").find("span").css({color:"#B5B5B6"})
		$(".yx").find("span").css({color:"#000"})
		$(".yx").find("img").attr('src','./images/yx.png');
		$(this).find("img").attr('src','./images/xx.png');
		$(this).find("span").css({color:"#F8C900"})
		$(".wx").find("img").attr('src','./images/wx.png');
	})
	$(".bkyx").click(function(){//备课教室选择效果
		$(".bkyx").find("span").css({color:"#000"})
		$(".bkyx").find("img").attr('src','./images/yx.png');
		$(this).find("img").attr('src','./images/bk.png');
		$(this).find("span").css({color:"#46B7BC"})
		
	})
	$(".kcap-m ul li").click(function(){//课程安排点击效果
		 $(".kcap-m ul li").eq($(this).index()).addClass("curm").siblings().removeClass('curm');
	})
	$(".pc-tab-menu ul li").click(function(){//TAB-MENU点击效果
		 $(".pc-tab-menu ul li").eq($(this).index()).addClass("cur").siblings().removeClass('cur');
	})
	$(".sucbtn").click(function(){
		$(".one3").addClass("cur").siblings().removeClass('cur');
		$("#con4_a_2").css({display:"block"}).siblings().css({display:"none"});
		$(".a2").addClass("cur4").siblings().removeClass('cur4');
	})

	$(".buy-menu ul li").click(function(){//TAB-MENU点击效果
		 $(".buy-menu ul li").eq($(this).index()).addClass("curr").siblings().removeClass('curr');
	})
	$(".level-menu ul li").click(function(){//TAB-MENU点击效果
		 $(".level-menu ul li").eq($(this).index()).addClass("cur3").siblings().removeClass('cur3');
		var li1 = $(".level-menu ul li:first-child")
		$(".cur3").insertBefore(li1)
		$(".level-menu ul li:first-child").click(function(){
			
		})
	})
	$(".ms-menu ul li").click(function(){//TAB-MENU点击效果
		 $(".ms-menu ul li").eq($(this).index()).addClass("cur4").siblings().removeClass('cur4');
	})
	$(".ms-menu ul li").click(function(){//TAB-MENU点击效果
		 $(".ms-menu ul li").eq($(this).index()).addClass("cur4").siblings().removeClass('cur4');
	})
	/*$(".sjb ul li").hover(function(){//TAB-MENU点击效果
		 $(".sjb ul li").eq($(this).index()).addClass("cur5").siblings().removeClass('cur5');
	})*/
	$("#c-num").click(function(){//手机号绑定
		$("#num-bind").removeAttr('readOnly');
		$("#num-bind").css({background:"#fff"});
		$("#nb").text('绑定');
		$("#nb").css({cursor:'pointer',color:'#49b8bc'});
	})
	$("#nb").click(function(){
		$("#num-bind").attr('readOnly','true');
		$("#num-bind").css({background:"#f4f5f5"});
		$("#nb").text('已绑定');
		$("#nb").css({cursor:'default',color:'#363434'});
	})
	$("#c-email").click(function(){//邮箱绑定
		$("#nemail-bind").removeAttr('readOnly');
		$("#email-bind").css({background:"#fff"});
		$("#eb").text('绑定');
		$("#eb").css({cursor:'pointer',color:'#49b8bc'});
	})
	$("#eb").click(function(){
		$("#email-bind").attr('readOnly','true');
		$("#email-bind").css({background:"#f4f5f5",border:""});
		$("#eb").text('已绑定');
		$("#eb").css({cursor:'default',color:'#363434'});
	})
	//日历
})