// دالة لعرض البارتشل في مودال
function showFormDialogUniversal(button) {
    // استخرج البيانات من خصائص الـ data-* للزر
    const controllerName = button.dataset.controller;
    const actionName = button.dataset.action;
    const id = button.dataset.id || null;

    // تكوين الرابط
    let url = id ? `/${controllerName}/${actionName}/${id}` : `/${controllerName}/${actionName}`;

    // AJAX لتحميل البارتشل
    $.ajax({
        url: url,
        type: 'GET',
        success: function (result) {
            $('#editPartialContainer').html(result); // عرض البارتشل
            $('#editModal').modal('show');           // فتح المودال
        },
        error: function (xhr, status, error) {
            console.error(`فشل تحميل البارتشل: ${error}`);
            alert("حدث خطأ أثناء تحميل النموذج.");
        }
    });
}

// دالة لإرسال الفورم من المودال
function submitFormUniversal(button) {
    const form = $('#formModal');
    const controllerName = form.data('controller');
    const actionName = form.data('action');
    const id = form.data('id') || null;

    const url = id ? `/${controllerName}/${actionName}/${id}` : `/${controllerName}/${actionName}`;

    $.ajax({
        url: url,
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                window.location.reload(); // إعادة تحميل الصفحة بعد الحفظ
            } else {
                $('#editPartialContainer').html(response); // تحديث الأخطاء
            }
        },
        error: function (xhr, status, error) {
            console.error(`فشل الإرسال: ${error}`);
            alert("حدث خطأ أثناء إرسال البيانات.");
        }
    });
}



  

async function showFormDialog(controllerName, id = null, modalId = 'additionalCostModal') {
    const url = id ? `/${controllerName}/Edit/${id}` : `/${controllerName}/Create`;

    try {
        const res = await fetch(url);
        if (!res.ok) throw new Error(res.statusText);

        const html = await res.text();

        // تحميل HTML فقط داخل المودال
        const modalContent = document.querySelector(`#${modalId} .modal-content`);

        // إزالة أي سكربت موجود داخل PartialView
        const tempDiv = document.createElement('div');
        tempDiv.innerHTML = html;
        tempDiv.querySelectorAll('script').forEach(s => s.remove());

        modalContent.innerHTML = tempDiv.innerHTML;

        // عرض المودال باستخدام Bootstrap 5 API
        const modalEl = document.getElementById(modalId);
        if (modalEl) {
            const modalInstance = new bootstrap.Modal(modalEl);
            modalInstance.show();
        }

    } catch (err) {
        alert("⚠️ حدث خطأ أثناء تحميل الصفحة: " + err.message);
    }
}


document.addEventListener('submit', function (e) {
    if (e.target && e.target.id === 'formModal') {
        e.preventDefault();

        const form = e.target;
        const modalEl = form.closest('.modal');
        const modalId = modalEl.id;

        const formData = new FormData(form);

        // div الرسائل
        let msgDiv = form.querySelector('#formMessages');
        if (!msgDiv) {
            msgDiv = document.createElement('div');
            msgDiv.id = 'formMessages';
            form.prepend(msgDiv);
        }
        msgDiv.innerHTML = '';

        fetch(form.action, {
            method: 'POST',
            body: formData
        })
            .then(res => res.json())
            .then(data => {
                if (data.success) {
                    msgDiv.innerHTML = `<div class="alert alert-success text-center">${data.message}</div>`;

                    setTimeout(() => {
                        msgDiv.innerHTML = '';

                        // إغلاق المودال
                        const modalInstance = bootstrap.Modal.getInstance(modalEl);
                        if (modalInstance) modalInstance.hide();
                    }, 3000);

                } else {
                    msgDiv.innerHTML = `<div class="alert alert-danger text-center">${data.message}</div>`;
                    setTimeout(() => { msgDiv.innerHTML = ''; }, 3000);
                }
            })
            .catch(err => {
                msgDiv.innerHTML = `<div class="alert alert-danger text-center">⚠️ خطأ بالشبكة: ${err.message}</div>`;
                setTimeout(() => { msgDiv.innerHTML = ''; }, 3000);
            });
    }
});


