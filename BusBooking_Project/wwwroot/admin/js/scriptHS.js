$(document).ready(function () {
    $('#addform').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        //var recipient = button.data('whatever') // Extract info from data-* attributes
        //// If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        //// Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var modal = $(this);
        //modal.find('.modal-title').text('New message to ' + recipient)
        modal.find('.modal-footer #send').on('click', function () {
            var code = modal.find('.modal-body #addSeat #CodeSeat');
            var busid = modal.find('.modal-body #addSeat #CodeBus');
            var status = modal.find('.modal-body #addSeat #StatusSeat');

            var regexCode = new RegExp(/^[A-Z0-9]{2,}$/);

            if (regexCode.test(code.val()) == false) {
                code.next('span').remove();
                code.after('<span style="color: red">Code Seat is Invalid!!</span>');
            } else {
                code.next('span').remove();
            }

            if (regexCode.test(code.val())) {
                var seatView = {
                    Code: code.val(),
                    BusId: parseInt(busid.val()),
                    Status: status.is(':checked')
                };
                $.ajax({
                    type: 'POST',
                    url: '/admin/seats/create',
                    cache: false,
                    contentType: 'application/json, charset=UTF-8',
                    dataType: 'json',
                    data: JSON.stringify(seatView),
                    success: function (data) {
                        modal.modal('hide');
                        if (data == "1") {
                            Swal.fire({
                                icon: 'success',
                                title: 'Your work has been saved',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            setTimeout(function () { location.reload(); }, 1600);
                        } else if (data == "0") {
                            Swal.fire({
                                icon: 'error',
                                title: 'Failed',
                                text: 'Item already exists !'
                            })
                        }
                    }
                })
            }
        })
    })

    $('#editSeat').on('show.bs.modal', function (event) {
        var btn = $(event.relatedTarget);

        var idSeat = btn.data('id');  // mã id của seat

        var code = $(this).find('.modal-body #editSeat .EditCodeSeat');
        //var busid = $(this).find('.model-body #editSeat #EditCodeBus');
        var modal = $(this);

        $.ajax({
            type: 'post',
            url: '/admin/seats/getbyid',
            cache: false,
            contentType: 'application/json, charset=UTF-8',
            dataType: 'json',
            data: JSON.stringify(idSeat),
            success: function (data) {
                var modal = $('#editSeat');
                if (data != '0') {
                    code.prop('readonly', false);
                    code.val(data.code);
                    //busid.val(data.busId)
                    $('#editSeat #EditCodeBus option').removeAttr('selected').filter('[value=' + data.busId + ']').attr('selected', true);
                    modal.modal('hide');
                };
            }, error: function (data) {
                console.log(data);
            }
        });


        $(this).find('#btnEdit').on('click', function () {

            var seatView = {
                Id: idSeat,
                Code: code.val(),
                BusId: parseInt($('#EditCodeBus').val()),
                Status: true
            };
            console.log(seatView);

            $.ajax({
                type: 'POST',
                url: '/admin/seats/modify',
                cache: false,
                contentType: 'application/json, charset=UTF-8',
                dataType: 'json',
                data: JSON.stringify(seatView),
                success: function (data) {
                    modal.modal('hide');
                    if (data == "1") {
                        Swal.fire({
                            icon: 'success',
                            title: 'Your work has been updated',
                            showConfirmButton: false,
                            timer: 1900
                        });

                        setTimeout(function () { location.reload(); }, 1990);
                    } else if (data == "0") {
                        Swal.fire({
                            icon: 'error',
                            title: 'Failed',
                            text: 'Update failed. Please check again!'
                        });
                    }
                }
            });

        });
    })

    var arr = [];
    $('.CbSeat').on('change', function () {
        if ($(this).is(':checked') == true) {
            arr.push($(this).attr('data-idseat'));
            console.log($(this).attr('data-idseat'));
        } else if ($(this).is(':checked') == false) {
            arr.splice($.inArray($(this).attr('data-idseat'), arr), 1);
        }
    })

    $('.btnRemove').on('click', function () {
        if (arr.length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: 'Remove ' + arr.length + ' Seat permanently !!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.value) {

                    $.ajax({
                        type: 'POST',
                        url: '/admin/seats/deleteseat',
                        cache: false,
                        contentType: 'application/json, charset=UTF-8',
                        dataType: 'json',
                        data: JSON.stringify(arr),
                        success: function (data) {
                            if (data == "1") {
                                Swal.fire(
                                    'Deleted!',
                                    'Seat has been deleted.',
                                    'success'
                                )

                                setTimeout(function () { location.reload() }, 1550);
                            }
                            else if (data == "0") {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error !!',
                                    text: 'Seat is here. Please check again!!',
                                })
                            }
                        }
                    });
                }
            })
        };
    });

    $('#AllCbSeat').on('change', function () {
        if ($(this).is(':checked') == true) {
            $('.CbSeat').prop('checked', true);
            arr = $('.CbSeat:checked').map(function () {
                return this.value;
            }).get();
        } else if ($(this).is(':checked') == false) {
            $('.CbSeat').prop('checked', false);
            arr = [];
        }
    })


    



})
function phantrang2(baseElement, totalPages, url) {
    $(baseElement).pagination({
        items: totalPages,
        itemsOnPage: getCookie("QuantityRows") == "" ? 10 : getCookie("QuantityRows"),
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

