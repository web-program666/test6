function submit1() {
    var cardId = document.getElementById("cardId").value;
    var form = new FormData();
    form.append("cardId", cardId);
    var xht = new XMLHttpRequest();
    xht.open("post", "../aspx/cardIdInput.aspx", false);
    xht.send(form);
    if (xht.readyState == 4 && xht.status == 200) {
        if (xht.responseText == "-1") {
            alert("卡号不存在，请重新输入！");
            document.getElementById("cardId").value = "";
        }
        else {
            window.location.href = "/人脸识别/faceContrast.html";
        }
    }
}