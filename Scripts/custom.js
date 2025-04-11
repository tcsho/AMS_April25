// JavaScript Document
$(document).ready(function(e) {
	/*$('.panel_body input[type="text"]').attr("placeholder","type Here");*/
   
    $('.acordian_header').click(function(){
		$('.acordian_body').slideToggle("slow",function(){
			/*$('.acordian_header p').toggleClass('plus_icon');*/
			$('.acordian_header').find('.plus_icon').toggleClass('minus_icon');
			});
		});
	
	
		
	$('.show_hide_btn').click(function(){
		$('.approved').slideToggle()
		});
		
		
	/*$('.show_hide_menu').click(function(){
		$('.menu_wrap').slideToggle()
		});*/
		
	$('.right_menu_wrap').click(function(){
		$('.left_menu_wrap').toggle(900);
		$('.menu_click').toggleClass('menu_click_reverse');
		});
		
	$('.close').click(function(){
		$('.mesg').slideUp("slow");
		$('.error_mesg').slideUp("slow");
		});
	$('.Report_criteria_header input[type="radio"]').click(function(){
		if($(this).attr('value')=='DateRanges'){
			$('.monthly').hide();
			$('.date_range').show("slow");
			}
			else
			{
				if($(this).attr('value')=='Monthly')
				{
				$('.date_range').hide();
				$('.monthly').show("slow");		
				}
			}
		});
	$('.Report_criteria_body input[type="checkbox"]').click(function(){
		$('.employee_list').toggle("slow");
		});
	$('.Reoprts_list_footer').click(function(){
		$('.Reoprts_list_body').slideToggle("slow");
		$('.Show_reoprt_icon').toggleClass('Show_reoprt_icon_hide');
		});
	$('.leave_balance .footer').click(function(){
		$('.leave_balance .header').slideToggle("slow");
		$('.leave_icon').toggleClass('leave_icon_hide');
		});
	$('.viewoptions').change(function(){
		var selected_view_option	=	$(this).val();
		if(selected_view_option == 'both'){
			$('.approved,.panel').show("slow");
		}else
		{
			if(selected_view_option == "pending"){
				$('.panel').show("slow");
				$('.approved').fadeOut("slow");
				}else
				{
					if(selected_view_option == "approved"){
						$('.panel').fadeOut("slow");
						$('.approved').show("slow");
						}
				}
		}
		
		});
	/*Date Picker*/	
	$(function() {
		$( ".datepicker" ).datepicker();
		$( "#anim" ).change(function() {
		  $( ".datepicker" ).datepicker( "option", "showAnim", $( this ).val());
		});
  	});
	
  /*Date Picker*/
  
  /*********Navigation***********/
 /* $('nav li').hover(function(){
		$(this).find('ul').slideToggle(800);
	});*/
	
	/*$( "li" ).has( "ul" ).css( "background-color", "red" );*/
	
	
	/*$("nav li").has( "ul" ).prepend('<span class="plus_icon">Plus</span>').css("background-color", "#888").attr("title","Click To Expand");*/
	
	/*$("nav li").css({"list-style-position":"inside","list-style-image":"url(images/icons/list_item.png)"});*/
	$("nav li").has("ul").css({"list-style-image":"url(images/icons/list_icon_down.png)","Font-weight":"bold"}).attr("title","Click To Expand");
	
	$("nav li").css("cursor","pointer");
	
	
 	 $('nav li').click(function(){ 
		/*$(this).find('.plus_icon').toggleClass('minus_icon');*/
		$(this).find('ul').slideToggle(800);
	});
	
  
    /*********Navigation***********/
	
	
	
	/*********Pnael_expand***********/
	$('.panel_head').append('<span class="panel_expand">Plus icon</span>');
	$('.panel_expand').click(function(){
		var parentpanel	=	$(this).parents(".panel");
		$(parentpanel).css("background","#C60");
		$(parentpanel).find('.panel_body').css("max-height","100%");
	/*	alert("click");
		$('.panel_body').css("max-height","100%");*/
		});
	
	
	
	
	
	
	/*********panel_expand***********/
  
  $('.add').click(function(){
	  $('.new_event_wraper').toggle("slow");
	  });
	  
	$('.send_mail_checkbox').click(function(){
	  $('.send_mail_input').toggle("slow");
	  });	
  
  /******Row Count********/
  /*var i=1;
  var rowCount = $('.panel:eq(i) .panel_body table tr').length;
  alert(rowCount);
	if(rowCount < 2){
		$('.panel:eq(1)').hide();
	}*/
	

	var divcount	=	$('.panel_body').length;
	/*alert(divcount);*/
	/*$('.panel_body').eq(1).hide();*/
	for(var i=0;i< divcount;i++){
		var selected_div = $('.panel_body').eq(i);
		
		
		/*var rowcount = ($('.panel_body table tr')[0]).length;
		alert(rowcount);*/
	}
	 /******Row Count********/
	 
	 /***************************Show or hide Popup**************************/
	 $('.show_popup').click(function(){
			$('.popup').slideDown();
			});
        $('.close_popup').click(function(){
			$('.popup').slideUp();
			});
	 /***************************Show or hide Popup**************************/
	 
	 
});