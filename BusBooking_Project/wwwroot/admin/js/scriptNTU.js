$(document).ready(function () {
    $('#addform').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        //var recipient = button.data('whatever') // Extract info from data-* attributes
        //// If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        //// Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        var modal = $(this);
        //modal.find('.modal-title').text('New message to ' + recipient)
        modal.find('.modal-footer #send').on('click', function () {
            var code = modal.find('.modal-body #addCategory #CodeCategory');
            var name = modal.find('.modal-body #addCategory #NameCategory');
            var price = modal.find('.modal-body #addCategory #PriceCategory');
            var active = modal.find('.modal-body #addCategory #ActiveCategory');
            var status = modal.find('.modal-body #addCategory #StatusCategory');

            var regexName = new RegExp(/^[a-z0-9_]+(\s){0,1}?[a-z0-9_]*$/i);
            var regexCode = new RegExp(/^[a-z0-9_]+$/i);
            var regexPrice = new RegExp(/^[0-9]+$/);

            if (regexCode.test(code.val()) == false) {
                code.next('span').remove();
                code.after('<span style="color: red">Code Category is Invalid!!</span>');
            } else {
                code.next('span').remove();
            }

            if (regexName.test(name.val()) == false) {
                name.next('span').remove();
                name.after('<span style="color: red">Name Category is Invalid!!</span>');
            } else {
                name.next('span').remove();
            }

            if (regexPrice.test(price.val()) == false || parseFloat(price.val()) <= 10000) {
                price.next('span').remove();
                price.after('<span style="color: red">Price Category is Invalid!!</span>');
            } else {
                price.next('span').remove();
            }

            if (regexCode.test(code.val()) && regexName.test(name.val()) && regexPrice.test(price.val()) && parseFloat(price.val()) > 10000) {
                var categoryView = {
                    Code: code.val(),
                    Name: name.val(),
                    Active: active.is(':checked'),
                    Price: parseFloat(price.val()),
                    Status: status.is(':checked')
                };

                $.ajax({
                    type: 'POST',
                    url: '/admin/category/add',
                    cache: false,
                    contentType: 'application/json, charset=UTF-8',
                    dataType: 'json',
                    data: JSON.stringify(categoryView),
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


    $('#editCate').on('show.bs.modal', function (event) {
        var btn = $(event.relatedTarget);

        var idCate = btn.data('id');  // mã id của category 

        var code = $(this).find('.modal-body #editCategory .EditCode');     // mã code loại sản phẩm
        var name = $(this).find('.modal-body #editCategory .EditName');     // Tên loai sản phẩm
        var active = $(this).find('.modal-body #editCategory .EditActive');     // Active loại sản phẩm
        var price = $(this).find('.modal-body #editCategory .EditPrice');
        var status = $(this).find('.modal-body #editCategory .EditStatus');
        var modal = $(this);

        $.ajax({
            type: 'post',
            url: '/admin/category/getbyid',
            cache: false,
            contentType: 'application/json, charset=UTF-8',
            dataType: 'json',
            data: JSON.stringify(idCate),
            success: function (data) {
                var modal = $('#editCate');
                if (data != '0') {
                    code.prop('readonly', false);
                    code.val(data.code);
                    code.prop('readonly', true);
                    name.val(data.name);
                    active.prop('checked', data.active);
                    price.val(data.price);
                    status.prop('checked', data.status)
                };
            }, error: function (data) {
                console.log(data)
            }
        });



        $(this).find('#btnEdit').on('click', function () {
            var categoryView = {
                Code: code.val(),
                Id: idCate,
                Name: name.val(),
                Active: active.is(':checked'),
                Price: parseFloat(price.val()),
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
                url: '/admin/category/update',
                cache: false,
                contentType: 'application/json, charset=UTF-8',
                dataType: 'json',
                data: JSON.stringify(categoryView),
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
    $('.cbCate').on('change', function () {
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
                text: 'Remove ' + arr.length + ' Category permanently !!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        type: 'POST',
                        url: '/admin/category/delete',
                        cache: false,
                        contentType: 'application/json, charset=UTF-8',
                        dataType: 'json',
                        data: JSON.stringify(arr),
                        success: function (data) {
                            if (data == "1") {
                                Swal.fire(
                                    'Deleted!',
                                    'Category has been deleted.',
                                    'success'
                                )

                                setTimeout(function () { location.reload() }, 1550);
                            }
                            else if (data == "0") {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error !!',
                                    text: 'Category is active. Please check again!!',
                                })
                            }

                        }
                    });
                }
            })
        };
    });

    $('#AllCbCategory').on('change', function () {
        if ($(this).is(':checked') == true) {
            $('.cbCate').prop('checked', true);
            arr = $('.cbCate:checked').map(function () {
                return this.value;
            }).get();
        } else if ($(this).is(':checked') == false) {
            $('.cbCate').prop('checked', false);
            arr = [];
        }
    })

    $('.manager-category').on('click', function () {
        window.location.replace('/admin/category')
    });

    $('.manager-car').on('click', function () {
        window.location.replace('/admin/cars')
    });

    $('.manager-seat').on('click', function () {
        window.location.replace('/admin/seats')
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

    
    var elem = $('.paginate');       // Đối tượng cần được phân trang
    var totalPage = elem.length;    // Tổng số trang
    var numberRow = 3;          // Số dòng mỗi trang


    elem.slice(numberRow).hide();
    $('#page-nav').pagination({
        items: totalPage,       
        itemsOnPage: numberRow,       
        onPageClick: function (pageNum) {
            var start = numberRow * (pageNum - 1);      // PageNum = 2 => start = 3
            var end = start + numberRow;             // end = 6

            elem.hide().slice(start, end).show();
        }
    })
    
})