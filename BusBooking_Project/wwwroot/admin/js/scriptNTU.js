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
            var active = modal.find('.modal-body #addCategory #ActiveCategory');

            var regexName = new RegExp(/^[a-z0-9_]+(\s){0,1}?[a-z0-9_]*$/i);
            var regexCode = new RegExp(/^[a-z0-9_]+$/i);

            if (regexCode.test(code.val()) == false) {
                code.next('span').remove();
                code.after('<span style="color: red">Code Cateogry is Invalid!!</span>');
            }

            if (regexName.test(name.val()) == false) {
                name.next('span').remove();
                name.after('<span style="color: red">Name Cateogry is Invalid!!</span>');
            }

            if (regexCode.test(code.val()) == true && regexName.test(name.val()) == true) {
                var categoryView = {
                    Code: code.val(),
                    Name: name.val(),
                    Active: active.is(':checked')
                };

                $.ajax({
                    type: 'POST',
                    url: '/admin/category/add',
                    cache: false,
                    contentType: 'application/json, charset=UTF-8',
                    data: JSON.stringify(categoryView),
                    dataType: 'json',
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
                if (data != 'Error') {
                    code.prop('readonly', false);
                    code.val(data.code);
                    //code.prop('readonly', true);
                    name.val(data.name);
                    active.prop('checked', data.active);
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
                Active: active.is(':checked')
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
                            title: 'Your work has been updated',
                            showConfirmButton: false,
                            timer: 1500
                        });

                        setTimeout(function () { location.reload(); }, 1600);
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
});
