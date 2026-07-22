import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Controllers\TanjezOrderController.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

# Add missing using statement
if 'using Microsoft.EntityFrameworkCore;' not in content:
    content = 'using Microsoft.EntityFrameworkCore;\n' + content

old_code = '''                // Fetch dynamic fields assignments
                var fieldsResponse = await _httpClient.GetStringAsync($"{_baseApiUrl}FieldAssignments/ByScreen/TanjezOrder");
                var assignments = JsonConvert.DeserializeObject<List<dynamic>>(fieldsResponse);
                
                // Extract unique dynamic fields to build table headers
                var dynamicFields = assignments.Select(a => new {
                    Id = (int)a.dynamicField.id,
                    Name = (string)a.dynamicField.name
                }).GroupBy(f => f.Id).Select(g => g.First()).ToList();
                ViewBag.DynamicFields = dynamicFields;'''

new_code = '''                // Fetch dynamic fields assignments from local context
                var _dynamicFormsContext = HttpContext.RequestServices.GetService<Mntry_Awqaf.Data.DynamicFormsDbContext>();
                var assignments = await _dynamicFormsContext.FieldAssignments
                    .Include(a => a.DynamicField)
                    .Where(a => a.ScreenType == "TanjezOrder")
                    .ToListAsync();
                
                // Extract unique dynamic fields to build table headers
                var dynamicFields = assignments.Select(a => new {
                    Id = a.DynamicField.Id,
                    Name = a.DynamicField.Name
                }).GroupBy(f => f.Id).Select(g => g.First()).ToList<dynamic>();
                
                ViewBag.DynamicFields = dynamicFields;'''

content = content.replace(old_code, new_code)

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print('Index action fixed successfully!')
