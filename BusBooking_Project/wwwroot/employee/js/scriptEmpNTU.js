function DefaultFormBooking() {
    var Target = document.querySelector('#DetailBooking').children[0].getElementsByTagName("input");

    for (var i = 0; i < Target.length; i++) {
        Target[i].readOnly = true;
    }
}

function ContentSendMail(content, elem) {
    document.querySelector(elem).innerText = content;
}