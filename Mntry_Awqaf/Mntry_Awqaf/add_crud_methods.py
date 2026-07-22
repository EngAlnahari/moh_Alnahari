import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Controllers\DynamicFormsController.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

new_methods = """
        [HttpPost]
        public async Task<IActionResult> EditField(int id, string name, string type, string options, bool isRequired)
        {
            var field = await _context.DynamicFields.FindAsync(id);
            if (field != null)
            {
                field.Name = name;
                field.Type = type;
                field.Options = options;
                field.IsRequired = isRequired;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم تعديل الحقل بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "لم يتم العثور على الحقل.";
            }
            return RedirectToAction(nameof(AssignFields));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteField(int id)
        {
            var field = await _context.DynamicFields.FindAsync(id);
            if (field != null)
            {
                var assignments = _context.FieldAssignments.Where(a => a.DynamicFieldId == id);
                _context.FieldAssignments.RemoveRange(assignments);
                
                _context.DynamicFields.Remove(field);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم حذف الحقل وتعييناته بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "لم يتم العثور على الحقل.";
            }
            return RedirectToAction(nameof(AssignFields));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var assignment = await _context.FieldAssignments.FindAsync(id);
            if (assignment != null)
            {
                _context.FieldAssignments.Remove(assignment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم إزالة التعيين بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "لم يتم العثور على التعيين.";
            }
            return RedirectToAction(nameof(AssignFields));
        }
"""

# Insert before 'public async Task<IActionResult> ViewAssignments()'
target = "        // 3. View All Assignments"
content = content.replace(target, new_methods + "\n" + target)

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print("Added CRUD methods to DynamicFormsController.cs")
