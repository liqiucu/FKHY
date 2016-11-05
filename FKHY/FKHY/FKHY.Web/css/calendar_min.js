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

	var html = "<table id='"+now_year+(now_month+1)+"' width=800px; padding=0;><tr><td colspan='7' height=51px><div class=rqxz style=height:30px><span class='now_year'>"+now_year+"</span><span class='now_month'>"+(now_month+1)+"월</span><div id=changem-btn><a id=prevm href='javascript:void(0)' onclick=prv_month("+now_year+","+now_month+","+now_day+",'"+id_selected+"')></a><a id=nextm href='javascript:void(0);' onclick=next_month("+now_year+","+now_month+","+now_day+",'"+id_selected+"')></a></div></div></td></tr><tr id=weektr><td>일요일<i class=fgx></i></td><td>월요일<i class=fgx></i></td><td>화요일<i class=fgx></i></td><td>수요일<i class=fgx></i></td><td>목요일<i class=fgx></i></td><td>금요일<i class=fgx></i></td><td>토요일</td><tr>";

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
		      	html += "<td><div class=sjb clearfix style=display:none><ul class=clearfix><li onclick='get_date_into_calendar($(this));'>7:30~8:15</li><li onclick='get_date_into_calendar($(this));'>8:30~9:15</li><li onclick='get_date_into_calendar($(this));'>9:30~10:15</li><li onclick='get_date_into_calendar($(this));'>10:30~11:15</li> <li onclick='get_date_into_calendar($(this));'>11:30~12:15</li> <li onclick='get_date_into_calendar($(this));'>12:30~13:15</li> <li onclick='get_date_into_calendar($(this));'>13:30~14:15</li></ul></div><a class=showsjb style='background:rgb(102, 190, 140);' href='javascript:void(0);' onclick=click_day($(this),"+now_year+","+now_month+","+date_str+");$('.showsjb').siblings().parent().find('div').css({display:'none'});$(this).prev().css({display:'block'})>"+date_str+"</a></td>";
		      }else{
		      	html += "<td><div class=sjb clearfix style=display:none><ul class=clearfix><li onclick='get_date_into_calendar($(this));'>7:30~8:15</li><li onclick='get_date_into_calendar($(this));'>8:30~9:15</li><li onclick='get_date_into_calendar($(this));'>9:30~10:15</li><li onclick='get_date_into_calendar($(this));'>10:30~11:15</li> <li onclick='get_date_into_calendar($(this));'>11:30~12:15</li> <li onclick='get_date_into_calendar($(this));'>12:30~13:15</li> <li onclick='get_date_into_calendar($(this));'>13:30~14:15</li></ul></div><a class=showsjb href='javascript:void(0);' onclick=click_day($(this),"+now_year+","+now_month+","+date_str+");$('.showsjb').siblings().parent().find('div').css({display:'none'});$(this).prev().css({display:'block'})>"+date_str+"</a></td>";
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
}

function get_date_into_calendar(obj){
		var index_num = $(".active").index();

		var sj=obj.text();
		obj.parent().parent().next().append("<p class=kcp>"+$(".infoList li:eq("+index_num+")").text()+"<br>"+sj+"</p>");
		/*$(this).parent().parent().parent().css({background:"#48b8bc"})*/
		obj.parent().parent().css({display:"none"});
		obj.parent().parent().next().css({background:"#48b8bc"})
	   		
 }
