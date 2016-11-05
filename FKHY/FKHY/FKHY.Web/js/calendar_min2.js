var myDate = new Date();
get_now_calendar_tab(myDate.getFullYear(),myDate.getMonth()+1,myDate.getDate(),"calendar");
get_now_calendar_tab(myDate.getFullYear(),myDate.getMonth()+1,myDate.getDate(),"calendar2");
function get_now_calendar_tab(ynow,mnow,dnow,id_selected){
	mnow=mnow-1;
	//定义每月的天数m_days[x],x为0-11自然数
	var m_days=new Array(31,28+is_leap(ynow),31,30,31,30,31,31,30,31,30,31);
	//获取每个星期的第一天
	var firstday = get_first_week(ynow,mnow,dnow);
	//获取当前月显示的行数
	var tr_str=Math.ceil((m_days[mnow] + firstday)/7);
	var prv_mnow = 0;
	if((mnow-1)<0){
		prv_mnow=11;
	}else{
		prv_mnow=mnow-1;
	}
	

	get_calendar_tab(tr_str,firstday,ynow,mnow,dnow,m_days[prv_mnow],id_selected,m_days[mnow]);
}
//获取当前是否是闰年
function is_leap(year) {
   return (year%100==0?res=(year%400==0?1:0):res=(year%4==0?1:0));
}
//返回第一天是星期几0-6
function get_first_week(year,month,day){
	var n1str=new Date(year,month,day);
	var fun_firstday=n1str.getDay();
	return fun_firstday;
}
function get_calendar_tab(str_tr,first_day,now_year,now_month,now_day,prv_m,id_selected,now_month_days_num){

	var html = "<table id='"+now_year+(now_month+1)+"' width=100%; padding=0; style=background:#fff><tr><td colspan='7' height=51px><div class=rqxz style=height:30px><span class='now_year'>"+now_year+"</span><span class='now_month'>"+(now_month+1)+"월</span><div id=changem-btn><a id=prevm href='javascript:void(0);' class=prev11 onclick=prv_month("+now_year+","+now_month+","+now_day+",'"+id_selected+"');$('.nicescroll-railss').css({display:'none'});$('.sjb').css({display:'none'})></a><a id=nextm  class=next11 href='javascript:void(0);' onclick=next_month("+now_year+","+now_month+","+now_day+",'"+id_selected+"');$('.nicescroll-railss').css({display:'none'});$('.sjb').css({display:'none'}) ></a></div></div></td></tr><tr id=weektr><td style=background:#fac900>일요일</td><td>월요일<i class=fgx></i></td><td>화요일<i class=fgx></i></td><td>수요일<i class=fgx></i></td><td>목요일<i class=fgx></i></td><td>금요일<i class=fgx></i></td><td>토요일</td><tr>";

	for(i=0;i<str_tr;i++) { //外层for语句 - tr标签
	   html += "<tr class=aa>";
	   for(k=0;k<7;k++) { //内层for语句 - td标签
	      idx=i*7+k; //表格单元的自然序号
	      date_str=idx-first_day+1; //计算日期
	      var flag = true;
	      //这里是处理有效日期代码 
	      if(date_str<=0){
	      	date_str = "";
	      	flag = false;
	      }
	      if(date_str>now_month_days_num){
	      	date_str = "";
	      	flag = false; 
	      }
	      if(flag == false){
	      	if(date_str == now_day){
		      	html += "<td><a href='javascript:void(0))' style='background:#66be8c;'>"+date_str+"</a></td>";
		      }else{
		      	html += "<td><a href='javascript:void(0))'>"+date_str+"</a></td>";
		      }
	      }else{
	      	if(date_str == now_day){
	      		if(id_selected!="calendar2"){
	      			html += "<td><span id=triangle-left style=display:none></span><div class=sjb clearfix style=display:none><ul class='lul' ><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;韩同学</p></li><li><p class=kcp1>1.&nbsp;中级韩语1</p><p>13:30~14:45&nbsp;李同学</p></li><li><p class=kcp1>1.&nbsp;高级韩语1</p><p>13:30~14:45&nbsp;吴同学</p></li><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;王同学</p></li><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;王同学</p></li><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;王同学</p></li><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;王同学</p></li> </ul></div><a class=showsjb style='background:rgb(102, 190, 140);' href='javascript:void(0);' onclick=click_day($(this),"+now_year+","+now_month+","+date_str+");$('.showsjb').siblings().parent().find('div').css({display:'none'});$(this).prev().css({display:'block'})>"+date_str+"</a></td>";
	      		}else{
	      			html += "<td><a class=showsjb style='background:rgb(102, 190, 140);' href='javascript:void(0);'>"+date_str+"</a></td>";
	      		}
		      	
		      }else{
		      	if(id_selected!="calendar2"){
		      		html += "<td><span id=triangle-left style=display:none></span><div class=sjb clearfix style=display:none><ul class='lul' ><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;韩同学</p></li><li><p class=kcp1>1.&nbsp;中级韩语1</p><p>13:30~14:45&nbsp;李同学</p></li><li><p class=kcp1>1.&nbsp;高级韩语1</p><p>13:30~14:45&nbsp;吴同学</p></li><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;王同学</p></li><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;王同学</p></li><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;王同学</p></li><li><p class=kcp1>1.&nbsp;基础韩语1</p><p>13:30~14:45&nbsp;王同学</p></li> </ul></div><a class=showsjb href='javascript:void(0);' onclick=click_day($(this),"+now_year+","+now_month+","+date_str+");$('.showsjb').siblings().parent().find('div').css({display:'none'});$(this).prev().css({display:'block'});$('.showsjb').siblings().parent().find('span').css({display:'none'});$(this).parent().next().find('span').css({display:'block'})>"+date_str+"</a></td>";
		      	}else{
	      			html += "<td><a class=showsjb href='javascript:void(0);'>"+date_str+"</a></td>";
	      		}
		      }
	      }
	   } //内层for语句结束
	   html += "</tr>";
	}
	html += "</table>";
	$("#"+id_selected).html(html);
}
			//切换上一年
			function prv_year(year,month,days,prv_select_year){
				get_now_calendar_tab(year-1,month,days,prv_select_year);
			}
			//切换下一年
			function next_year(year,month,days,next_select_year){
				get_now_calendar_tab(year+1,month+1,days,next_select_year);
			}
			//切换上一月
			function prv_month(year,month,days,nprv_select_month){
				month = month+1;
				if(month<2){
					year=year-1;
					month=13;
				}
				var obj_list = $("#"+nprv_select_month+"_lisrt").find("#"+year+month);
				if(obj_list.length>0){
					$("#"+year+month).html($("#"+nprv_select_month).html());
				}else{
					$("#"+nprv_select_month+"_lisrt").html($("#"+nprv_select_month+"_lisrt").html()+"<div id='"+year+month+"'>"+$("#"+nprv_select_month).html()+"</div>");
				}
				obj_list = $("#"+nprv_select_month+"_lisrt").find("#"+year+(month-1));
				if(obj_list.length>0){
					$("#"+nprv_select_month).html($("#"+year+(month-1)).html());
				}else{
					get_now_calendar_tab(year,month-1,days,nprv_select_month);
				}
			}
			//切换下一月
			function next_month(year,month,days,next_select_month){
				month = month+1;
				if(month>11){
					year=year+1;
					month=0;
				}
				var obj_list = $("#"+next_select_month+"_lisrt").find("#"+year+month);
				if(obj_list.length>0){
					$("#"+year+month).html($("#"+next_select_month).html());
				}else{
					$("#"+next_select_month+"_lisrt").html($("#"+next_select_month+"_lisrt").html()+"<div id='"+year+month+"'>"+$("#"+next_select_month).html()+"</div>");
				}
				obj_list = $("#"+next_select_month+"_lisrt").find("#"+year+(month+1));
				if(obj_list.length>0){
					$("#"+next_select_month).html($("#"+year+(month+1)).html());
				}else{
					get_now_calendar_tab(year,month+1,days,next_select_month);
				}
			}
			//点击天的处理函数
			function click_day(obj,year,month,days){
				month = month+1;
				obj.prev().css({display:'block'})
			      function myBrowser(){//判断为safari浏览器后单独加滚动条样式
					    var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
					    var isOpera = userAgent.indexOf("Opera") > -1;
					    if (isOpera) {
					        return "Opera"
							    }; //判断是否Opera浏览器
					    if (userAgent.indexOf("Firefox") > -1) {
					        return "FF";
					    } //判断是否Firefox浏览器
					    if (userAgent.indexOf("Chrome") > -1){
					 		return "Chrome";
						}
					    if (userAgent.indexOf("Safari") > -1) {
					        return "Safari";
					    } //判断是否Safari浏览器
					    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
					        return "IE";
					    }; //判断是否IE浏览器
						}
						//以下是调用上面的函数
						var mb = myBrowser();
						if ("Safari" == mb) {
						   	 $(".sjb").addClass("scroll_for_safari")
						}else{
							obj.prev().niceScroll({//滚动条
							cursorcolor: "rgb(161, 161, 161)",//#CC0071 光标颜色
							cursoropacitymax: 1, //改变不透明度非常光标处于活动状态（scrollabar“可见”状态），范围从1到0
							touchbehavior: false, //使光标拖动滚动像在台式电脑触摸设备
							cursorwidth: "5px", //像素光标的宽度
							cursorborder: "0", // 	游标边框css定义
							cursorborderradius: "5px",//以像素为光标边界半径
							autohidemode: false //是否隐藏滚动条   
			  			});
					}


				obj.prev().mouseleave(function(){//鼠标移开隐藏时间表
			 		$(".nicescroll-railss").css({display:"none"})
			 		$('.showsjb').parent().find('span').css({display:'none'});
			 	 	obj.prev().css({display:"none"})
			 })

			}

			function get_date_into_calendar(obj){
					var index_num = $(".active").index();
					var sj=obj.text();
					obj.parent().parent().next().find("p").remove();//用户多次点击，清除掉之前的信息
					obj.parent().parent().next().append("<p class=kcp>"+$(".infoList li:eq("+index_num+")").text()+"<br>"+sj+"</p>");//插入新的预约时间
					obj.parent().parent().parent().find("i").remove();
					obj.parent().parent().next().next().css({display:"block"})//显示关闭按钮
					obj.parent().parent().parent().append("<i id=remove_yy onclick=remove_yysj($(this))>x</i>")//添加关闭按钮
					obj.parent().parent().css({display:"none"});//选择后隐藏时间列表
					obj.parent().parent().next().css({background:"#48b8bc"})
					/*$(".picList li:first").find("p").addClass("t-cur");*/
					$(".picList li").find("span").css({display:"block"});

			 }
			 $(".selected_y").click(function(){
			 	    $(".selected_y").removeClass('t-cur'); 
			 	    $(".t-name i").css({display:"block"});
			 	    $(this).addClass("t-cur");
			 	    $(".t-name").removeClass("t-cur");
			 	    $(this).prev().prev().addClass("t-cur");
			 	    $(this).prev().prev().find("i").css({display:"none"})
			 })

			 function remove_yysj(obj){//删除预约
			 		obj.prev().find("p").remove();
			 		obj.prev().css({background:"#fff"})
			 		obj.remove()
			 }
			 $(".showsjb").click(function(){//判断是否点击的是容器最后一个，如果是时间列表将显示在左边
			 	if($(this).parent().index()+1==7){
			 		$(this).prev().css({left:"-115px"})
			 	}
			 })


			$(".one2,.one3,.one4,#one11").click(function(){//如果没有触发mouseleave事件，点击其他菜单隐藏时间表
					$(".nicescroll-railss").css({display:"none"})
			 	 	$(".sjb").css({display:"none"}) 
			 		
			})
			$(".next11 .prev11").click(function(){
				console.log(1)
			})


