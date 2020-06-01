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
                                timer: 2100
                            });
                            location.reload();
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


})