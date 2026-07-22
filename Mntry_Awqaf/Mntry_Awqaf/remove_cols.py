import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Views\TanjezOrder\Index.cshtml'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

old_thead = '''                                <th>رقم الطلب</th>
                                @if (ViewBag.DynamicFields != null)
                                {
                                    foreach (var field in ViewBag.DynamicFields)
                                    {
                                        <th>@field.Name</th>
                                    }
                                }
                                <th>حالة الطلب</th>
                                <th>مقدم الطلب</th>
                                <th>اختيار المهندس / إنشاء العقد</th>
                                <th>العمليات</th>'''

new_thead = '''                                <th>رقم الطلب</th>
                                @if (ViewBag.DynamicFields != null)
                                {
                                    foreach (var field in ViewBag.DynamicFields)
                                    {
                                        <th>@field.Name</th>
                                    }
                                }
                                <th>العمليات</th>'''

content = content.replace(old_thead, new_thead)


old_tbody = '''                                    <td>@item.Status</td>
                                    <td>@item.CreateOrderName</td>
                                    <td>
                                         @{
                                             var contractsDict = ViewBag.Contracts as Dictionary<int, int> ?? new Dictionary<int, int>();
                                             if (contractsDict.TryGetValue(item.Id, out var contractId))
                                             {
                                                 <a href="#" class="btn btn-sm btn-info view-contract-btn" data-contract-id="@contractId">
                                                     عرض العقد
                                                 </a>
                                             }
                                             else
                                             {
                                                 <button class="btn btn-sm btn-primary create-contract-btn"
                                                         data-order-id="@item.Id"
                                                         data-type="@item.TypePiece"
                                                         data-space="@item.Space"
                                                         data-governorate="@item.City"
                                                         data-transportation="@item.Transportation"
                                                         data-overnight="@item.OverNight"
                                                         data-allowance="@item.Allowance">
                                                     إنشاء عقد
                                                 </button>
                                             }
                                         }
                                    </td>

                                    <td>
                                         @if (contractsDict.TryGetValue(item.Id, out var cid))
                                         {
                                             <a href="#" class="view-contract-btn" data-contract-id="@cid">
                                                 <i class="feather icon-eye f-w-600 f-16 m-r-15 text-primary"> عرض المزيد</i>
                                             </a>
                                         }
                                         else
                                         {
                                             <span class="text-muted">-</span>
                                         }
                                    </td>
                                    <td>
                                        <a href="#!"><i class="icon feather icon-edit f-w-600 f-16 m-r-15 text-success"></i></a><a href="#!"><i class="feather icon-trash-2 f-w-600 f-16 text-danger"></i></a>
                                    </td>'''

new_tbody = '''                                    <td>
                                        <a href="#!"><i class="icon feather icon-edit f-w-600 f-16 m-r-15 text-success"></i></a><a href="#!"><i class="feather icon-trash-2 f-w-600 f-16 text-danger"></i></a>
                                    </td>'''

content = content.replace(old_tbody, new_tbody)

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print('Index view updated successfully!')
