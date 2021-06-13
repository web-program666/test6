function get_name(name) {    //接受后端传进来的name,判断管理员是否登录
    var reg = RegExp(name + '=([^;]+)');
    var arr = document.cookie.match(reg);
    /*    alert(arr);*/
    if (arr) {
        //已登录
        return arr[1];  //返回登录者的cardId
    } else {
        alert("请登录！")
        return "";
    }
}
function get_cardId(cardId) {    //接受后端传进来的cardId,判断管理员是否输入用户卡号
    var reg = RegExp(cardId+"2"+ '=([^;]+)');
    var arr = document.cookie.match(reg);
    /*    alert(arr);*/
    if (arr) {
        //已登录
        return arr[1];  //返回登录者的cardId
    } else {
        alert("请先输入用户卡号！")
        return "";
    }
}