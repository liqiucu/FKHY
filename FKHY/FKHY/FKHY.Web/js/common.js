var host = "http://localhost/os";
var URLS = {
	getCoursePage:host+"/pc/api/getCoursePage.html?jwt=" + (localStorage.getItem("jwt") || ""),
	getOrderPage:host+"/pc/api/getOrderPage.html?jwt=" + (localStorage.getItem("jwt") || ""),
	submitOrder:host+"/pc/api/submitOrder.html?jwt=" + (localStorage.getItem("jwt") || ""),
	getUserInfo:host+"/pc/api/getUserInfo.html?jwt=" + (localStorage.getItem("jwt") || ""),
	modifyUser:host+"/pc/api/modifyUser.html?jwt=" + (localStorage.getItem("jwt") || ""),
	subscribe:host+"/pc/api/subscribe.html?jwt=" + (localStorage.getItem("jwt") || ""),
	getMessagePage:host+"/pc/api/getMessagePage.html?jwt=" + (localStorage.getItem("jwt") || ""),
	reg:host+"/pc/api/reg.html",
	login:host+"/pc/api/login.html",
	getValidCode:host+"/pc/api/getValidCode.html",

}