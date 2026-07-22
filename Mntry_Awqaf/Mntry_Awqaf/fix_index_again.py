import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Views\TanjezOrder\Index.cshtml'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

# I will find the exact <tr> ... </tr> block inside the tbody
start_tbody = content.find('<tbody>')
start_tr = content.find('<tr>', start_tbody)
end_tr = content.find('</tr>', start_tr) + 5

new_tr = '''                                <tr>
                                    <td>@item.Id</td>
                                    @{
                                        var dynamicValues = ViewBag.DynamicValues as IEnumerable<Mntry_Awqaf.Models.DynamicFieldValue>;
                                    }
                                    @if (ViewBag.DynamicFields != null)
                                    {
                                        foreach (var field in ViewBag.DynamicFields)
                                        {
                                            int fId = (int)field.Id;
                                            var val = dynamicValues?.FirstOrDefault(v => v.RecordId == item.Id && v.DynamicFieldId == fId)?.Value ?? "-";
                                            <td>@val</td>
                                        }
                                    }
                                    <td>
                                        <a href="#!"><i class="icon feather icon-edit f-w-600 f-16 m-r-15 text-success"></i></a><a href="#!"><i class="feather icon-trash-2 f-w-600 f-16 text-danger"></i></a>
                                    </td>
                                </tr>'''

content = content[:start_tr] + new_tr + content[end_tr:]

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print('Index view replaced perfectly!')
