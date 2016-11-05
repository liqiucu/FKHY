$(function(){
    var total=$('.tab_pic ul li').length;$('.tab_pic ul li').eq(0).addClass('on');
    $('.tab_txt ul li').eq(0).addClass('on');
    $('.tab_pic ul').width(total*359);
    $('.tab_txt ul').width(total*470);
    $('.next').click(function(){
        var num=$('.tab_pic ul li').length;
        var index=$('.tab_pic ul li.on').index('.tab_pic ul li');
        var on=index+1<num?index+1:0;var pwidth=parseInt(on*359);
        var twidth=parseInt(on*470);
        $('.tab_pic ul li').eq(on).addClass('on').siblings().removeClass('on');
        $('.tab_pic ul').stop().animate({left:-pwidth},"slow");
        $('.tab_txt ul li').eq(on).addClass('on').siblings().removeClass('on');
        $('.tab_txt ul').stop().animate({left:-twidth},"slow");});
        $('.prev').click(function(){
            var num=$('.tab_pic ul li').length;
            var index=$('.tab_pic ul li.on').index('.tab_pic ul li');
            var on=index==0?num-1:index-1;var pwidth=parseInt(on*359);
            var twidth=parseInt(on*470);
            $('.tab_pic ul li').eq(on).addClass('on').siblings().removeClass('on');
            $('.tab_pic ul').stop().animate({left:-pwidth},"slow");
            $('.tab_txt ul li').eq(on).addClass('on').siblings().removeClass('on');
            $('.tab_txt ul').stop().animate({left:-twidth},"slow");
          });
})