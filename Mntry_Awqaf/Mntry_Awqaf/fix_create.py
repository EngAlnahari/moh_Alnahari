import sys

content = open('d:/alnahari/Mntry_Awqaf/Mntry_Awqaf/Views/TanjezOrder/Create.cshtml', 'r', encoding='utf-8').read()
idx = content.find('<div class="form-title mb-2">نوع الحيازة</div>')

if idx == -1:
    print('Not found')
    sys.exit(1)

new_content = content[:idx] + """<div class="form-title mb-2">نوع الحيازة</div>


            <div class="col-md">
                <div class="input-label-en">نوع طريق الوصول</div>
                <select asp-for="tanjezOrder.AccessRoadType"
                        asp-items="ViewBag.AccessRoadTypes ?? new List<SelectListItem>()"
                        class="form-select add-option-enabled"
                        data-controller="AccessRoadTypes">
                    <option value="">-- اختر طريق الوصول --</option>
                    <option value="add">+ إضافة</option>
                </select>
            </div>


            <!-- الفترة والعرض -->

            <div class="col-md">
                <div class="input-label-en">(فترة تسليم العمل)</div>
                @* <select asp-for="tanjezOrder.PeriodType" asp-items="ViewBag.accessRoadType" class="form-select"> *@

                <select class="form-select" id="periodType" asp-for="tanjezOrder.PeriodType">
                    <option value="طارئ ومستعجل">طارئ ومستعجل</option>
                    <option value="حسب الاتفاق مع المهندس">حسب الاتفاق مع المهندس</option>
                    <option value="طويلة">طويلة</option>
                </select>
            </div>
            <div class="col-md">
                <div class="input-label-en">أقصى مدة</div>
                <input type="text" class="form-control" id="maximumDuration" asp-for="tanjezOrder.MaximumDuration" placeholder="اختر التاريخ" />
            </div>
        </div>


        <div class="row mb-3">


            <div class="col-md">
                <div class="input-label-en">انتقال المهندس الى القطة المراد مسحها</div>
                <select class="form-select" id="Transportation" asp-for="tanjezOrder.Transportation">
                    <option value=""></option>
                    <option value="3"> سأقوم (بنقله وارجاعه) بسيارتي الخاصة</option>
                    <option value="2">يتكفل المهندس بتنقلاته</option>

                </select>
            </div>



            <!-- العرض -->

            <div class="col-md">
                <div class="input-label-en">المبيت</div>
                <select class="form-select" id="OverNight" asp-for="tanjezOrder.OverNight">
                    <option value=""></option>
                    <option value="3"> سوف أوفر مبيت ومكان اقامة للمهندس خلال ايام العمل</option>
                    <option value="2">︎يتكفل المهندس بمبيته واقامته</option>

                </select>
            </div>



            <div class="col-md">
                <div class="input-label-en">النثريات (تغذية، قات، ..الخ)</div>
                <select class="form-select" id="Allowance" asp-for="tanjezOrder.Allowance">
                    <option value=""></option>
                    <option value="3"> نثريات المهندس على حسابي الخاص</option>
                    <option value="2">يتكفل المهندس بجميع نثرياته</option>

                </select>
            </div>


        </div>



        <div class="text-center mt-4">
            <button type="submit" id="saveRequestBtn" class="btn btn-success px-5">
                <i class="fas fa-save me-2"></i>حفظ الطلب 
            </button>
        </div>
        @if (!ViewData.ModelState.IsValid)
        {
            <div class="alert alert-danger">
                <ul>
                    @foreach (var error in ViewData.ModelState.Values.SelectMany(v => v.Errors))
                    {
                        <li>@error.ErrorMessage</li>
                    }
                </ul>
            </div>
        }
    </form>
    <!-- هذا العنصر لعرض رسائل النجاح أو الفشل -->
    <div id="pageAlert"></div>

</div>
<div id="bottomAlerts" style="position: fixed; bottom: 20px; right: 20px; z-index: 1050;"></div>

<!-- Modal لعرض الـ PartialView -->
<div class="modal fade" id="formModal" tabindex="-1" aria-labelledby="formModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="formModalLabel">إضافة جديد</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body" id="modalBodyContent">
                <!-- سيتم تحميل الـ PartialView هنا -->
            </div>
        </div>
    </div>
</div>
<!-- مودال صغير لإدخال الاسم أو الرقم -->
<div class="modal fade" id="agentModal" tabindex="-1" aria-labelledby="agentModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="agentModalLabel">بيانات الوكيل</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <input type="text" id="agentNameOrNumber" class="form-control" placeholder="أدخل الاسم أو الرقم">
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" id="saveAgentBtn">حفظ</button>
            </div>
        </div>
    </div>
</div>

<input type="hidden" id="hiddenAgentField" name="tanjezOrder.AgentValue" />



<!-- CSS -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">

<!-- JS -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

<script>
    document.querySelectorAll(".add-option-enabled").forEach(function(dropdown) {
        dropdown.addEventListener("change", function() {
            if (this.value === "add") {
                var controllerName = this.getAttribute("data-controller");
                var targetDropdown = this;

                // استدعاء صفحة الإضافة الخاصة بالمتحكم عبر Ajax
                fetch(`/${controllerName}/Create`)
                    .then(response => response.text())
                    .then(html => {
                        document.getElementById("modalBodyContent").innerHTML = html;
                        var myModal = new bootstrap.Modal(document.getElementById('formModal'));
                        myModal.show();

                        // إضافة حدث الفورم داخل الـ PartialView
                        var form = document.querySelector("#modalBodyContent form");
                        if(form) {
                            form.addEventListener("submit", function(e) {
                                e.preventDefault();

                                var formData = {};
                                new FormData(form).forEach((value, key) => formData[key] = value);

                                fetch(`/${controllerName}/Create`, {
                                    method: "POST",
                                    headers: { "Content-Type": "application/json" },
                                    body: JSON.stringify(formData)
                                })
                                .then(res => res.json())
                                .then(data => {
                                    if(data.success) {
                                        // إغلاق المودال
                                        myModal.hide();

                                        // عرض رسالة النجاح داخل الصفحة فقط
                                        var alertDiv = document.getElementById("pageAlert");
                                        alertDiv.className = "alert alert-success mt-2";
                                        alertDiv.innerText = "تم الإضافة بنجاح";

                                        // تحديث الـ DropDown
                                        var option = document.createElement("option");
                                        option.value = data.item.id;
                                        option.text = data.item.name;
                                        targetDropdown.add(option);
                                    } else {
                                        // عرض رسالة خطأ داخل الـ Modal
                                        var alertDiv = document.getElementById("modalAlert");
                                        if(!alertDiv) {
                                            alertDiv = document.createElement("div");
                                            alertDiv.id = "modalAlert";
                                            document.getElementById("modalBodyContent").prepend(alertDiv);
                                        }
                                        alertDiv.className = "alert alert-danger";
                                        alertDiv.innerText = data.message || "حدث خطأ أثناء الحفظ";
                                    }
                                });
                            });
                        }
                    });

                // إعادة تعيين القيمة
                this.value = "";
            }
        });
    });
</script>




<!-- سكربت -->
<script>
    document.addEventListener('DOMContentLoaded', function() {
        const descripSelect = document.querySelector('select[asp-for="tanjezOrder.DescripPerson"]');

        if (descripSelect) {
            descripSelect.addEventListener('change', function() {
                if (this.value === 'وكيل') {
                    // فتح المودال
                    const modalEl = document.getElementById('agentModal');
                    const agentModal = new bootstrap.Modal(modalEl);
                    agentModal.show();

                    // إعادة تعيين قيمة select إلى فارغ بعد الفتح
                    this.value = '';
                }
            });
        }

        document.getElementById('saveAgentBtn').addEventListener('click', function() {
            const agentValue = document.getElementById('agentNameOrNumber').value;
            if(agentValue.trim() !== '') {
                document.getElementById('hiddenAgentField').value = agentValue;
            }
            const modalEl = document.getElementById('agentModal');
            const modal = bootstrap.Modal.getInstance(modalEl);
            modal.hide();
        });
    });
</script>

@section Scripts {
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            // جلب الحقول المخصصة بناءً على الشاشة مباشرة
            $.get('/DynamicForms/GetFieldsByScreenTypeOnly?screenType=TanjezOrder', function (groupedFields) {
                const container = $('#dynamicFieldsContainer');
                container.empty();
                
                if(groupedFields && groupedFields.length > 0) {
                    groupedFields.forEach(group => {
                        // Add Group Header
                        container.append(`<div class="col-12"><h4 class="fw-bold mt-3 text-primary">${group.groupName}</h4><hr></div>`);
                        
                        // Add Fields in this Group
                        if(group.fields && group.fields.length > 0) {
                            let rowHtml = '<div class="row w-100">';
                            group.fields.forEach(field => {
                                let inputHtml = '';
                                const requiredAttr = field.isRequired ? 'required' : '';
                                const reqSpan = field.isRequired ? '<span class="text-danger">*</span>' : '';
                                
                                if (field.type === 'dropdown' && field.options) {
                                    const options = field.options.split(',');
                                    inputHtml = `<select name="DynamicField_${field.id}" class="form-select" ${requiredAttr}>
                                        <option value="">-- اختر --</option>
                                        ${options.map(o => `<option value="${o}">${o}</option>`).join('')}
                                    </select>`;
                                } else if (field.type === 'textarea') {
                                    inputHtml = `<textarea name="DynamicField_${field.id}" class="form-control" rows="2" ${requiredAttr}></textarea>`;
                                } else if (field.type === 'file') {
                                    inputHtml = `<input type="file" name="DynamicField_${field.id}" class="form-control" ${requiredAttr}>`;
                                } else {
                                    const type = field.type === 'number' ? 'number' : (field.type === 'date' ? 'date' : 'text');
                                    inputHtml = `<input type="${type}" name="DynamicField_${field.id}" class="form-control" ${requiredAttr}>`;
                                }

                                rowHtml += `
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label">${field.name} ${reqSpan}</label>
                                        ${inputHtml}
                                    </div>
                                `;
                            });
                            rowHtml += '</div>';
                            container.append(rowHtml);
                        }
                    });
                } else {
                    container.append('<div class="col-12"><div class="alert alert-info">لا توجد حقول ديناميكية مخصصة لهذه الشاشة حالياً. يمكنك التعيين من إعدادات النماذج.</div></div>');
                }
            });
        });
    </script>
}
"""
open('d:/alnahari/Mntry_Awqaf/Mntry_Awqaf/Views/TanjezOrder/Create.cshtml', 'w', encoding='utf-8').write(new_content)
print('Successfully fixed Create.cshtml')
