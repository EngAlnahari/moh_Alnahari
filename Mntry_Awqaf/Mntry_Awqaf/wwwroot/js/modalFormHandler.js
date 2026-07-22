
function showFormDialog(controllerName, id = null) {
    let url = id ? `/${controllerName}/Edit/${id}` : `/${controllerName}/Create`; // تحديد الرابط بناءً على الحالة

    $.ajax({
        url: url,
        type: 'GET',
        success: function (result) {
            $('#editPartialContainer').html(result);
            $('#editModal').modal('show');
        }
    });
}
function submitForm(controllerName, id = null) {
    let form = $('#formModal');
    let url = id ? `/${controllerName}/Edit/${id}` : `/${controllerName}/Create`;

    $.ajax({
        url: url,
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                // رسالة نجاح مؤقتة
                $('#editPartialContainer').html(`<div class="alert alert-success">${response.message}</div>`);

                // إغلاق المودال بعد 3 ثواني
                setTimeout(function () {
                    $('#editModal').modal('hide');
                }, 3000);

            } else {
                $('#editPartialContainer').html(response); // عرض الأخطاء داخل المودال
            }
        }


    });
}

