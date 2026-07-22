import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Views\TanjezOrder\Create.cshtml'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

old_logic = '''                                if (field.type === 'dropdown' && field.options) {
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
                                }'''

new_logic = '''                                if (field.type === 'dropdown' && field.options) {
                                    const options = field.options.split(',');
                                    inputHtml = `<select name="DynamicField_${field.id}" class="form-select" ${requiredAttr}>
                                        <option value="">-- اختر --</option>
                                        ${options.map(o => `<option value="${o}">${o}</option>`).join('')}
                                    </select>`;
                                } else if (field.type === 'radio' && field.options) {
                                    const options = field.options.split(',');
                                    inputHtml = '<div class="d-flex flex-wrap gap-3">';
                                    options.forEach((o, index) => {
                                        inputHtml += `
                                            <div class="form-check">
                                                <input class="form-check-input" type="radio" name="DynamicField_${field.id}" id="radio_${field.id}_${index}" value="${o}" ${requiredAttr}>
                                                <label class="form-check-label" for="radio_${field.id}_${index}">${o}</label>
                                            </div>`;
                                    });
                                    inputHtml += '</div>';
                                } else if (field.type === 'checkbox' && field.options) {
                                    const options = field.options.split(',');
                                    inputHtml = '<div class="d-flex flex-wrap gap-3">';
                                    options.forEach((o, index) => {
                                        inputHtml += `
                                            <div class="form-check">
                                                <input class="form-check-input" type="checkbox" name="DynamicField_${field.id}" id="checkbox_${field.id}_${index}" value="${o}">
                                                <label class="form-check-label" for="checkbox_${field.id}_${index}">${o}</label>
                                            </div>`;
                                    });
                                    inputHtml += '</div>';
                                } else if (field.type === 'textarea') {
                                    inputHtml = `<textarea name="DynamicField_${field.id}" class="form-control" rows="2" ${requiredAttr}></textarea>`;
                                } else if (field.type === 'file') {
                                    inputHtml = `<input type="file" name="DynamicField_${field.id}" class="form-control" ${requiredAttr}>`;
                                } else {
                                    let htmlType = 'text';
                                    if(field.type === 'number') htmlType = 'number';
                                    if(field.type === 'date') htmlType = 'date';
                                    if(field.type === 'time') htmlType = 'time';
                                    if(field.type === 'email') htmlType = 'email';
                                    if(field.type === 'password') htmlType = 'password';
                                    
                                    inputHtml = `<input type="${htmlType}" name="DynamicField_${field.id}" class="form-control" ${requiredAttr}>`;
                                }'''

content = content.replace(old_logic, new_logic)

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print("Updated Create.cshtml JS logic")
