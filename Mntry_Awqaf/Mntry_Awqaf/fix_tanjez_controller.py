import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Controllers\TanjezOrderController.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

old_logic = '''                // Extract unique dynamic fields to build table headers
                var dynamicFields = assignments.Select(a => new {
                    Id = a.DynamicField.Id,
                    Name = a.DynamicField.Name
                }).GroupBy(f => f.Id).Select(g => g.First()).ToList<dynamic>();'''

new_logic = '''                // Extract unique dynamic fields to build table headers based on FieldAssignments
                var dynamicFields = assignments.Select(a => new {
                    Id = a.Id, // Group by FieldAssignment ID so each assigned column is distinct
                    Name = string.IsNullOrEmpty(a.GroupName) ? a.DynamicField.Name : a.GroupName
                }).GroupBy(f => f.Id).Select(g => g.First()).ToList<dynamic>();'''

content = content.replace(old_logic, new_logic)

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print('TanjezOrderController replaced successfully')
