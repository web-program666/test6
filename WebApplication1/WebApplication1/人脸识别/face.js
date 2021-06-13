function submit(picture, state) {
    url = window.location.href;
    var temp = url.split("?")[1];
    var cardId = temp.split("=")[1].toString();
        var xht = new XMLHttpRequest();
        /*form = "cardId=" + cardId + "&picture=" + encodeURIComponent(picture);*/
        var form = new FormData();
        form.append("cardId", cardId);
        form.append("picture", picture);
        form.append("state", state);
        xht.open("Post", "/人脸识别/savePic.aspx", false);
        xht.send(form);
    if (xht.status == 200 && xht.readyState == 4) {
        if (xht.responseText == "1") {
            alert("人脸上传成功！");
            window.location.href = "/Administrator/cardIdInput/html/cardIdInput.html";
        }
        }
}
function submit1(picture, state) {
    if (get_cardId("cardId")) {
        var cardId = get_cardId("cardId");
        var xht = new XMLHttpRequest();
        /*form = "cardId=" + cardId + "&picture=" + encodeURIComponent(picture);*/
        var form = new FormData();
        form.append("cardId", cardId);
        form.append("picture", picture);
        form.append("state", state);
        xht.open("Post", "/人脸识别/savePic.aspx", false);
        xht.send(form);
        if (xht.status == 200 && xht.readyState == 4) {
            if (xht.responseText == "1") {     //识别结果为同一人
                alert("识别成功！");
                window.location.href = "/Administrator/setInformation/html/setInfo.html";
            }
            else if (xht.responseText == "-1") {   //人脸不存在
                alert("人脸识别失败，请重新识别！");
                window.onload();
            }
            else {
                alert(xht.responseText);
                window.onload();
            }
        }
    }
}
