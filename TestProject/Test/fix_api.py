import sys
import os

path = r'd:\alnahari\TestProject\Test\Controllers\DynamicFieldValuesController.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

new_action = '''
        // GET: api/DynamicFieldValues/TanjezOrder
        [HttpGet("{screenType}")]
        public async Task<ActionResult<IEnumerable<DynamicFieldValue>>> GetAllValuesByScreen(string screenType)
        {
            return await _context.DynamicFieldValues
                .Where(v => v.ScreenType == screenType)
                .ToListAsync();
        }
'''

index = content.find('public async Task<ActionResult<IEnumerable<DynamicFieldValue>>> GetValues(string screenType, int recordId)')
if index != -1:
    content = content[:index-70] + new_action + content[index-70:]
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)
    print('Action added successfully!')
else:
    print('Could not find the insertion point!')
