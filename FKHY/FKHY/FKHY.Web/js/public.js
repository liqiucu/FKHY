//显示菜单
function show_menu() {
    var bd_top = $(document).scrollTop();
    if($('#menu').css('display')=='none') {
        $('#menu').removeClass('hid');
        $('#menu').addClass('show');
       
      
    } else {
        $('#menu').removeClass('show');
        $('#menu').addClass('hid');
      
    }
 }
 
(function(){
   var $nav = $('.goods_nav');
   $(window).on("scroll", function() {  
   $('#menu').removeClass('show');  
    $('#menu').addClass('hid');
    });
 })();