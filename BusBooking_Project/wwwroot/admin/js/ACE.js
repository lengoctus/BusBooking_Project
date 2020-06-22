$(document).ready(function () {
    //Kiểm tra xem đang show bao nhiêu dòng trên 1 bảng, để set cho lựa chọn
    //let totalRows = getCookie('QuantityRows');
    //if (totalRows != '') {
    //    $('.changQuantityRows').val(totalRows);
    //}
    //$('.select-2').select2();
    //End
    activeMenu();
})



function activeMenu() {
    var href = window.location.pathname;
    var currentItemMenu = href.split('/')[2];
    switch (currentItemMenu.toLowerCase()) {
        case 'home'.toLowerCase():
            $($('.item_menu')[0]).addClass('active');
            break;
        case 'user'.toLowerCase():
            $($('.item_menu')[1]).addClass('active');
            break;
        case 'station'.toLowerCase():
            $($('.item_menu')[2]).addClass('active');
            break; 
        case 'category':
            $($('.item_menu')[3]).addClass('active');
            break;  
        case 'bus':
            $($('.item_menu')[4]).addClass('active');
            break;    
        case 'seats':
            $($('.item_menu')[5]).addClass('active');
            break;     
        case 'routes':
            $($('.item_menu')[6]).addClass('active');
            break;       
    }
}

//Cookie
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

//Phân trang, baseElement: tag sẽ phân trang, totalPages: tổng số record của entitied, url: url get data
function phantrang(baseElement, totalPages, url) {
    $(baseElement).pagination({
        items: totalPages,
        itemsOnPage: getCookie("QuantityRows") == "" ? 5 : getCookie("QuantityRows"),
        currentPage: parseInt(new URL(document.URL).searchParams.get("page")),
        cssStyle: 'light-theme',
        hrefTextPrefix: url,
        hrefTextSuffix: '',
        onPageClick(pageNumber, event) {
            let urls = document.URL;
            let pageCurrent = new URL(urls).searchParams.get("page");
            urls = Boolean(pageCurrent) ? urls.replace("?page=" + pageCurrent, "?page=" + pageNumber) : urls + '?page=' + pageNumber;
            window.location.assign(urls);
        },
    });
}

//Alert success, sau khi thông báo thì reload();
function alertSuccess() {
    Swal.fire({
        title: 'Successfully.',
        text: '',
        showConfirmButton: false,
        icon: 'success'
    }).then(
        function (isConfirm) {
            if (isConfirm) {
                location.reload();
            }
        },
    );
}

function alertSuccess(text) {
    Swal.fire({
        title: 'Successfully.',
        text: text,
        showConfirmButton: false,
        icon: 'success'
    }).then(
        function (isConfirm) {
            if (isConfirm) {
                location.reload();
            }
        },
    );
}

function alertSuccessRedirect(text, location) {
    Swal.fire({
        title: 'Successfully.',
        text: text,
        showConfirmButton: false,
        icon: 'success'
    }).then(
        function (isConfirm) {
            if (isConfirm) {
                window.location = location;
            }
        },
    );
}

function alertSuccessNonReload() {
    Swal.fire({
        title: 'Successfully.',
        text: '',
        showConfirmButton: false,
        icon: 'success'
    })
}

function alertError() {
    Swal.fire({
        icon: 'error',
        title: 'Failed...',
        text: 'Something went wrong! Please again',
    });
}

function alertError(texterror) {
    Swal.fire({
        icon: 'error',
        title: 'Failed...',
        text: texterror,
    });
}

function alertQuestion(titleStr, textStr) {
    Swal.fire({
        title: titleStr,
        text: textStr,
        icon: 'question'
    });
}

// alert warning, cảnh báo trả về kết quả lực chọn
function alertWarning() {
    return Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    });
}


function ErrorMode(text, target) {
    var str = "";
    str += '<div class="card bg-danger">';
    str += '<div class="card-header">';
    str += '<span>' + text + '</span>';
    str += '<div class="card-tools">';
    str += '<button type="button" class="btn btn-tool" data-card-widget="remove">';
    str += '<i class="fas fa-times" style="color:white !important"></i>';
    str += '</button></div></div></div>';
    target.append(str);
}

function SuccessMode(text, target) {
    var str = "";
    str += '<div class="card bg-success">';
    str += '<div class="card-header">';
    str += '<span>' + text + '</span>';
    str += '<div class="card-tools">';
    str += '<button type="button" class="btn btn-tool" data-card-widget="remove">';
    str += '<i class="fas fa-times" style="color:white !important"></i>';
    str += '</button></div></div></div>';
    target.append(str);
}



//


