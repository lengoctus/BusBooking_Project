$(document).ready(function () {

    //  Create new Category
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
                                title: 'Category have been saved!!',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            setTimeout(function () { location.reload(); }, 1600);
                        } else if (data == "0") {
                            Swal.fire({
                                icon: 'error',
                                title: 'Failed',
                                text: 'Category already exists !'
                            })
                        }
                    }
                })
            }
        })
    })


    //  Event show modal Edit
    $('#editCate').on('show.bs.modal', function (event) {
        var btn = $(event.relatedTarget);

        var idCate = btn.data('id');  // mã id của category 

        var code = $(this).find('.modal-body #editCategory .EditCode');     // mã code loại sản phẩm
        var name = $(this).find('.modal-body #editCategory .EditName');     // Tên loai sản phẩm
        var active = $(this).find('.modal-body #editCategory .EditActive');     // Active loại sản phẩm
        var price = $(this).find('.modal-body #editCategory .EditPrice');
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
                    code.val(data.code);
                    name.val(data.name);
                    active.prop('checked', data.active);
                    price.val(data.price);
                };
            }, error: function (data) {
                console.log(data)
            }
        });


        // Event button Edit is clicked
        $(this).find('#btnEdit').on('click', function () {

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
                    Id: idCate,
                    Name: name.val(),
                    Active: active.is(':checked'),
                    Price: parseFloat(price.val()),
                };

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
                                title: 'Category has been updated',
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
            }


        })
    })
    //$.fn.changeManager = function (stringUrl) {
    //    $(this).on('click', function () {
    //        window.location.replace(stringUrl);
    //    })
    //};


    // Event checkbox remove is clicked
    var arr = [];
    $('.cbCate').on('change', function () {
        if ($(this).is(':checked') == true) {
            var table = $('#tableCate tbody');
            //var CategoryView = {
            //    Id: parseInt($(this).val()),
            //    Code: table.parent().parent().find('td:eq(1)').text(),
            //    Name: table.parent().parent().find('td:eq(2)').text(),
            //    Price: parseFloat(table.parent().parent().find('td:eq(3)').text()),
            //    Active: table.parent().parent().find('td:eq(4)').text().toLowerCase() == 'on' ? true : false,
            //};

            //arr.push(CategoryView);
            arr.push(parseInt($(this).val()));

        } else if ($(this).is(':checked') == false) {
            //var CategoryView = {
            //    Id: parseInt($(this).val()),
            //}

            arr.splice($.inArray(parseInt($(this).val()), arr), 1);
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
                        url: '/admin/category/upatestatus',
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
                return parseInt(this.value);
            }).get();
        } else if ($(this).is(':checked') == false) {
            $('.cbCate').prop('checked', false);
            arr = [];
        }
    })



    //$('.manager-seat').changeManager('/admin/seats');
    //$('.manager-category').changeManager('/admin/category');
    //$('.manager-car').changeManager('/admin/cars');

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

    //=====================================     Routes   ========================
    $('#addRoutes').on('show.bs.modal', function (event) {

        //  Event change category and choose bus
        $('#Add_CategoryBus').on('change', function () {
            var idcate = $(this).val();
            if (idcate != '0') {
                $.ajax({
                    type: 'post',
                    url: '/admin/routes/getbusbycateid',
                    cache: false,
                    contentType: 'application/json, charset=UTF-8',
                    dataType: 'json',
                    data: JSON.stringify(idcate),
                    success: function (data) {
                        if (data != "0") {
                            $('#Add_BusId').find('option').remove();

                            for (var i = 0; i < data.length; i++) {
                                var option = new Option(data[i].code, data[i].id);
                                $('#Add_BusId').append(option);
                            }
                        }
                    }
                })
            } else {
                $('#Add_BusId').find('option').remove();
                var option = new Option("Choose Bus", "0");
                $('#Add_BusId').append(option);
            }


        })

        //  Load clock for Add_TimeGo
        var clockTime2 = function () {
            var dt = new Date();
            $('.clockpicker2 #Add_TimeGo').val(dt.getHours() + ":" + dt.getMinutes());

            $('.clockpicker2').clockpicker({
                default: dt.getHours() + ":" + dt.getMinutes(),
                autoclose: true,
                twelvehour: false
            });
        }
        clockTime2();

        //  Event submit data add routes
        var modal = $(this);
        modal.find('.modal-footer #send').on('click', function () {
            var regexPrice = new RegExp(/^[0-9]+$/);
            var regexLength = new RegExp(/^[0-9]+$/);
            var regexTimeRun = new RegExp(/^[0-9a-zA-Z]+$/);

            var price = modal.find('#formaddRoutes #Add_Price');
            var length = modal.find('#formaddRoutes #Add_Length');
            var timerun = modal.find('#formaddRoutes #Add_TimeRun');
            var timego = modal.find('#formaddRoutes #Add_TimeGo');
            var fromid = modal.find('#formaddRoutes #Add_RouteFrom');
            var toid = modal.find('#formaddRoutes #Add_RouteTo');
            var active = modal.find('#formaddRoutes #Add_Active');
            var busid = modal.find('#formaddRoutes #Add_BusId');

            if (!regexPrice.test(price.val())) {
                price.next('span').remove();
                price.after('<span style="color: red">Price is Invalid!!</span>');
            } else {
                price.next('span').remove();
            }

            if (!regexLength.test(length.val())) {
                length.next('span').remove();
                length.after('<span style="color: red">Length is Invalid!!</span>');
            } else {
                length.next('span').remove();
            }

            if (!regexTimeRun.test(timerun.val())) {
                timerun.next('span').remove();
                timerun.after('<span style="color: red">TimeRun is Invalid!!</span>');
                timego.parent().after('<span>&nbsp;</span>');
            } else {
                timerun.next('span').remove();
                timego.parent().next('span').remove();
            }


            if ($('#Add_BusId').val() == "0") {
                $('#Add_BusId').next('span').remove();
                $('#Add_BusId').after('<span style="color: red">Bus is Invalid!!</span>');
                $('#Add_CategoryBus').after('<span>&nbsp;</span>');
            } else {
                $('#Add_BusId').next('span').remove();
                $('#Add_CategoryBus').next('span').remove();
            }


            if (regexPrice.test(price.val()) && regexLength.test(length.val()) && regexTimeRun.test(timerun.val()) && $('#Add_CategoryBus').val() != "0" && $('#Add_BusId').val() != "0") {
                var routesView = {
                    StationFrom: parseInt(fromid.val()),
                    StationTo: parseInt(toid.val()),
                    Price: parseFloat(price.val()),
                    Length: parseInt(length.val()),
                    TimeGo: timego.val(),
                    Active: active.is(':checked'),
                    TimeRun: timerun.val(),
                    BusId: parseInt(busid.val())
                };
                $.ajax({
                    type: 'post',
                    url: '/admin/routes/add',
                    cache: false,
                    dataType: 'json',
                    contentType: 'application/json,charset=UTF-8',
                    data: JSON.stringify(routesView),
                    success: function (data) {
                        if (data != '0') {
                            modal.modal('hide');
                            Swal.fire({
                                icon: 'success',
                                title: 'Routes have been saved!',
                                showConfirmButton: false,
                                timer: 1500
                            });

                            setTimeout(function () { location.reload(); }, 1600);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Failed',
                                text: 'Routes already exists !'
                            })
                        }
                    }
                })
            }

        })
    });


    //  ====================================    Pagination  ==============================

    $.fn.PaginationPage = function (options)
    {
        var defaults = {
            totalPage: 0,
            numberRow: 0,
            currentP: 0
        };
        var settings = $.extend(defaults, options);
        console.log(settings.currentP);

        // Get current Url
        var currentUrl = window.location.href.toLowerCase().trim();

        if (settings.totalPage > 0 && settings.numberRow > 0 && settings.currentP > 0) {
            $(this).pagination({
                items: settings.totalPage,
                itemsOnPage: settings.numberRow,
                currentPage: settings.currentP,
                onPageClick: function (pgNumber) {
                    if (currentUrl.indexOf('pgnb') >= 0) {
                        currentUrl = currentUrl.replace('&pgnb=' + settings.currentP, '&pgnb=' + pgNumber);
                    } else {
                        currentUrl += '&pgnb=' + pgNumber;
                    }
                    window.location.assign(currentUrl);
                }
            })
        }

    }

}) 