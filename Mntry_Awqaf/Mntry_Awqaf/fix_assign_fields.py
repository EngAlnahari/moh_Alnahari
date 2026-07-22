import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Views\DynamicForms\AssignFields.cshtml'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

# Replace table header
old_th = '''                                        <th style="width: 50px;">تحديد</th>
                                        <th>اسم الحقل</th>
                                        <th>نوع الحقل</th>
                                        <th>مطلوب؟</th>'''

new_th = '''                                        <th style="width: 50px;">تحديد</th>
                                        <th>اسم الحقل</th>
                                        <th>نوع الحقل</th>
                                        <th>مطلوب؟</th>
                                        <th style="width: 150px;">إجراءات</th>'''

content = content.replace(old_th, new_th)

# Replace table body rows
old_tr = '''                                                <td class="text-center">
                                                    <input type="checkbox" name="fieldIds" value="@field.Id" class="form-check-input" />
                                                </td>
                                                <td>@field.Name</td>
                                                <td>@field.Type</td>
                                                <td>@(field.IsRequired ? "نعم" : "لا")</td>'''

new_tr = '''                                                <td class="text-center">
                                                    <input type="checkbox" name="fieldIds" value="@field.Id" class="form-check-input" />
                                                </td>
                                                <td>@field.Name</td>
                                                <td>@field.Type</td>
                                                <td>@(field.IsRequired ? "نعم" : "لا")</td>
                                                <td>
                                                    <button type="button" class="btn btn-sm btn-primary edit-btn" 
                                                        data-bs-toggle="modal" data-bs-target="#editFieldModal" 
                                                        data-id="@field.Id" data-name="@field.Name" 
                                                        data-type="@field.Type" data-req="@field.IsRequired.ToString().ToLower()" 
                                                        data-opts="@field.Options">تعديل</button>
                                                    <form asp-action="DeleteField" method="post" style="display:inline;" onsubmit="return confirm('هل أنت متأكد من حذف الحقل نهائياً وكل التعيينات المرتبطة به؟');">
                                                        <input type="hidden" name="id" value="@field.Id" />
                                                        <button type="submit" class="btn btn-sm btn-danger">حذف</button>
                                                    </form>
                                                </td>'''

content = content.replace(old_tr, new_tr)

# Replace Tab 2 list item
old_li = '''                                                    <li class="list-group-item d-flex justify-content-between align-items-center">
                                                        @assignment.DynamicField.Name
                                                        <span class="badge bg-primary rounded-pill">@assignment.DynamicField.Type</span>
                                                    </li>'''

new_li = '''                                                    <li class="list-group-item d-flex justify-content-between align-items-center">
                                                        <div>
                                                            @assignment.DynamicField.Name
                                                            <span class="badge bg-primary rounded-pill ms-2">@assignment.DynamicField.Type</span>
                                                        </div>
                                                        <form asp-action="DeleteAssignment" method="post" onsubmit="return confirm('هل أنت متأكد من إلغاء تعيين هذا الحقل؟');">
                                                            <input type="hidden" name="id" value="@assignment.Id" />
                                                            <button type="submit" class="btn btn-sm btn-outline-danger">إلغاء التعيين</button>
                                                        </form>
                                                    </li>'''

content = content.replace(old_li, new_li)

# Add Edit Modal and Script before @section Scripts
edit_modal = '''
<!-- Edit Modal -->
<div class="modal fade" id="editFieldModal" tabindex="-1" aria-labelledby="editFieldModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="editFieldModalLabel">تعديل الحقل الأساسي</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <form asp-action="EditField" method="post">
          <div class="modal-body">
            <input type="hidden" name="id" id="edit-id" />
            
            <div class="mb-3">
              <label for="edit-name" class="col-form-label">اسم الحقل:</label>
              <input type="text" class="form-control" name="name" id="edit-name" required>
            </div>
            
            <div class="mb-3">
              <label for="edit-type" class="col-form-label">نوع الحقل:</label>
              <select class="form-select" name="type" id="edit-type" required>
                    <option value="text">نص قصير (Text)</option>
                    <option value="textarea">نص طويل (Textarea)</option>
                    <option value="number">رقم (Number)</option>
                    <option value="date">تاريخ (Date)</option>
                    <option value="time">وقت (Time)</option>
                    <option value="email">بريد إلكتروني (Email)</option>
                    <option value="password">كلمة مرور (Password)</option>
                    <option value="dropdown">قائمة منسدلة (Dropdown)</option>
                    <option value="checkbox">خيارات متعددة (Checkbox)</option>
                    <option value="radio">اختيار فردي (Radio)</option>
                    <option value="file">رفع ملف (File Upload)</option>
                    <option value="table">جدول بيانات (Table)</option>
                    <option value="coordinate">إحداثيات جغرافية (Coordinate)</option>
              </select>
            </div>

            <div class="mb-3" id="edit-opts-group" style="display:none;">
              <label for="edit-options" class="col-form-label">الخيارات (مفصولة بفاصلة):</label>
              <input type="text" class="form-control" name="options" id="edit-options">
            </div>

            <div class="form-check mb-3">
              <input class="form-check-input" type="checkbox" name="isRequired" id="edit-req" value="true">
              <label class="form-check-label" for="edit-req">مطلوب؟</label>
            </div>
            
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">إلغاء</button>
            <button type="submit" class="btn btn-primary">حفظ التغييرات</button>
          </div>
      </form>
    </div>
  </div>
</div>
'''

script_addition = '''
            $('.edit-btn').on('click', function() {
                var id = $(this).data('id');
                var name = $(this).data('name');
                var type = $(this).data('type');
                var req = $(this).data('req');
                var opts = $(this).data('opts');

                $('#edit-id').val(id);
                $('#edit-name').val(name);
                $('#edit-type').val(type);
                $('#edit-req').prop('checked', req === true || req === 'true');
                $('#edit-options').val(opts);

                if (type === 'dropdown' || type === 'checkbox' || type === 'radio') {
                    $('#edit-opts-group').show();
                } else {
                    $('#edit-opts-group').hide();
                }
            });

            $('#edit-type').on('change', function() {
                var type = $(this).val();
                if (type === 'dropdown' || type === 'checkbox' || type === 'radio') {
                    $('#edit-opts-group').slideDown();
                } else {
                    $('#edit-opts-group').slideUp();
                }
            });
'''

content = content.replace('@section Scripts {', edit_modal + '\n@section Scripts {')
content = content.replace('const treeData = [', script_addition + '\n        const treeData = [')

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print("Updated AssignFields.cshtml")
