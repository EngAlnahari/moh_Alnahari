import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Controllers\DynamicFormsController.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

old_logic = '''                .Select(g => new
                {
                    groupName = g.Key,
                    fields = g.Select(a => new {
                                  id = a.Id, // Treat FieldAssignment Id as the unique field ID for saving
                                  name = string.IsNullOrEmpty(a.GroupName) ? a.DynamicField.Name : a.GroupName, // Or just use the GroupName if custom
                                  type = a.DynamicField.Type,
                                  isRequired = a.DynamicField.IsRequired,
                                  options = a.DynamicField.Options
                              })
                              .ToList()
                })'''

new_logic = '''                .Select(g => new
                {
                    groupName = g.Key,
                    fields = g.Select(a => new {
                                  id = a.Id, // Treat FieldAssignment Id as the unique field ID for saving
                                  name = a.DynamicField.Name, // Use the actual field name
                                  type = a.DynamicField.Type,
                                  isRequired = a.DynamicField.IsRequired,
                                  options = a.DynamicField.Options
                              })
                              .ToList()
                })'''

content = content.replace(old_logic, new_logic)

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print('DynamicFormsController name replaced successfully')
