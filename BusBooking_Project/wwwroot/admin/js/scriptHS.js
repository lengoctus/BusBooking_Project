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

            var regexCode = new RegExp(/^[A-Z0-9]{1,}$/);
           
            if (regexCode.test(code.val()) == false) {
                code.next('span').remove();
                code.after('<span style="color: red">Code Seat is Invalid!!</span>');
            } else {
                code.next('span').remove();
            }

            if (regexCode.test(code.val()) ) {
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
        var busid = parseInt($(this).find('.model-body #editSeat #EditCodeBus'));
        console.log(busid)
        var status = $(this).find('.modal-body #editSeat .EditStatus');
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
                    busid.prop('selected', data.busid)
                    status.prop('checked', data.status)
                };
            }, error: function (data) {
                console.log(data)
            }
        });


        $(this).find('#btnEdit').on('click', function () {
            var seatView = {
                Code: code.val(),
                Id: id,
                BusId: parseInt(busId.val()),
                Status: status.is(':checked')
            };

            if (status.is(':checked') == false) {
                Swal.fire({
                    title: 'Are you sure?',
                    text: "Are you sure you want to change the status?!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, delete it!'
                }).then((result) => {
                    if (result.value) {
                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        )
                    }
                })
            }


            $.ajax({
                type: 'POST',
                url: '/admin/seat/modify',
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
                            timer: 1500
                        });

                        setTimeout(function () { location.reload(); }, 1550);
                    } else if (data == "0") {
                        Swal.fire({
                            icon: 'error',
                            title: 'Failed',
                            text: 'Update failed. Please check again!'
                        })
                    }
                }
            })
        })
    })

    var arr = [];
    $('.cbSeat').on('change', function () {
        if ($(this).is(':checked') == true) {
            arr.push($(this).val());
        } else if ($(this).is(':checked') == false) {
            arr.splice($.inArray($(this).val(), arr), 1);
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
                        url: '/admin/seat/delete',
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
                                    text: 'Seat is active. Please check again!!',
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
            $('.cbSeat').prop('checked', true);
            arr = $('.cbSeat:checked').map(function () {
                return this.value;
            }).get();
        } else if ($(this).is(':checked') == false) {
            $('.cbSeat').prop('checked', false);
            arr = [];
        }
    })


    //$('.buschecked').on('change', function () {
    //    var idbus = $(this).val();

    //    if (idbus.trim()) {
    //        $.ajax({
    //            type: 'post',
    //            url: '/admin/seats/GetByBusId',
    //            cache: false,
    //            contentType: 'application/json, charset=UTF-8',
    //            dataType: 'json',
    //            data: JSON.stringify(idbus),
    //            success: function (data) {
                    
    //            }
    //        })
    //    }
    //})

    
    
    
})