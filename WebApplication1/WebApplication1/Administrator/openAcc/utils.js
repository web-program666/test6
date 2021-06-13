
function checkPhone() {
    // var reg = /^(0|86|17951)?(13[0-9]|15[012356789]|18[0-9]|14[57]|17[678])[0-9]{8}$/;   //判断字符串是否为电话号码
    var reg = /^\d+$/;   //判断字符串是否均为数字
    var phone = document.getElementById("phone");
    if (reg.test(phone.value) == false) {
        phone.value = "";
        alert("电话号码格式不对，请重新尝试！");
    }
}
function checkEmail() {
    var email = document.getElementById("Email");
    var emreg = /^\w{3,}(\.\w+)*@[A-z0-9]+(\.[A-z]{2,5}){1,2}$/;    //判断字符串为邮箱地址
    if (emreg.test(email.value) == false) {
        email.value = "";
        alert("邮箱格式不对，请重新输入!")
    }
}

function checkMoney() {
    var money = document.getElementById("money");
    var mm = money.value * 1.0;    //string类型转换为double型
    if (mm < 0) {
        money.value = "";
        alert("存款金额为负，错误！  请确认！");
    }
}
//清空
function reset(container, tagName) {
    var farther = document.getElementById(container);
    var inputs = farther.getElementsByTagName(tagName);
    for (var i = 0; i < inputs.length; i++) {
        //判断这个元素是不是按钮
        if (inputs[i].type != "button") {
            inputs[i].value = "";
        }
    }
}