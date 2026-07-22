import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Views\TanjezOrder\Index.cshtml'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

# Replace table headers
old_thead = '''                                <th> </th>
                                <th> </th>
                                <th> </th>
                                <th> </th>
                                <th> </th>
                                <th> </th>
                                <th> </th>
                                <th>اسم الموضوع</th>
                                <th> نوع القطعة </th>
                                <th>   المساحة</th>
                                <th> نوع طريق الوصول</th>
                                <th> المساحة مختلفة عن الوثيقة</th>
                               
                                <th>وحدة الجوار</th>
                                <th>المساحة بالمتر</th>
                                <th>الحدود</th>
                                <th>المدة الزمنية</th>
                                <th>المبلغ المقترح</th>
                                <th> الحيازة</th>
                                <th> ملفات المشروع</th>
                                <th>  حالة الطلب</th>
                                <th>  مقدم الطلب</th>
                                <th>   العرض</th>
                                <th>اختيار المهندس / إنشاء العقد</th>
                                <th>
                                   العمليات
                                </th>'''

new_thead = '''                                <th>رقم الطلب</th>
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

content = content.replace(old_thead, new_thead)

# Replace table body rows
old_tbody = '''                                     <td>@Html.DisplayFor(modelItem => item.Id)</td>   @* قم الطلب *@

                                    <td>@Html.DisplayFor(modelItem => item.TypePiece)</td>     @* الموضع *@
                                    <td>@Html.DisplayFor(modelItem => item.Space)</td>     @* المساحة *@
                                    <td>@Html.DisplayFor(modelItem => item.City)</td>     @* المحافضة *@
                                    <td>@Html.DisplayFor(modelItem => item.Transportation)</td>     @* التنقلات *@
                                    <td>@Html.DisplayFor(modelItem => item.Allowance)</td>     @* النثريات *@
                                    <td>@Html.DisplayFor(modelItem => item.OverNight)</td>     @* المبيت *@
                                    <td>@Html.DisplayFor(modelItem => item.PlaceName)</td>
                                    <td>@Html.DisplayFor(modelItem => item.TypePiece)</td>
                                    <td>@Html.DisplayFor(modelItem => item.AcountPiese)</td>
                                    <td>@Html.DisplayFor(modelItem => item.AccessRoadType)</td>
                                    <td>@(item.AreaDifferent == 1 ? "نعم" : "لا")</td>
   
                                    <td></td>
                                    @* <td>@Html.DisplayFor(modelItem => item.Notes)</td> *@
                                    <td>
                                        <a href="#" class="btn btn-sm btn-primary show-borders"
                                           data-order-id="@item.Id">
                                            عرض
                                        </a>
                                    </td>

                                    @* <td>@Html.DisplayFor(modelItem => item.EarthBorders)</td> *@
                                    <td>@Html.DisplayFor(modelItem => item.MaximumDuration)</td>
                                    <td>@Html.DisplayFor(modelItem => item.PeriodType)</td>
                                    <td>@Html.DisplayFor(modelItem => item.TotalCost) </td>
                                    <td>@Html.DisplayFor(modelItem => item.Alhiaza) </td>
                                   
                                    <td>.pdf</td>
                                    <td>@Html.DisplayFor(modelItem => item.Status)</td>
                                    <td>@Html.DisplayFor(modelItem => item.CreateOrderName)</td>
                                    <td>
                                    <td>@Html.DisplayFor(modelItem => item.Width)</td>
                                    <td>'''

new_tbody = '''                                     <td>@item.Id</td>
                                    @{
                                        var dynamicValues = ViewBag.DynamicValues as List<Mntry_Awqaf.Models.DynamicFieldValue>;
                                    }
                                    @if (ViewBag.DynamicFields != null)
                                    {
                                        foreach (var field in ViewBag.DynamicFields)
                                        {
                                            var val = dynamicValues?.FirstOrDefault(v => v.RecordId == item.Id && v.DynamicFieldId == field.Id)?.Value ?? "-";
                                            <td>@val</td>
                                        }
                                    }
                                    <td>@item.Status</td>
                                    <td>@item.CreateOrderName</td>
                                    <td>'''

content = content.replace(old_tbody, new_tbody)

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print('Index view updated successfully!')
