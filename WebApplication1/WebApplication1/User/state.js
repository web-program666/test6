function get_cookie(cardId) {    //接受后端传进来的cookie,判断是否登录
    var reg = RegExp(cardId + '=([^;]+)');
    var arr = document.cookie.match(reg);
 /*   alert(arr);*/
    if (arr) {
        //已登录
        return arr[1];  //返回登录者的cardId
    } else {
        alert("请登录！")
        return "";
    }
}