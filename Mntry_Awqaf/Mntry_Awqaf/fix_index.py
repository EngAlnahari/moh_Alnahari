import sys

path = r'd:\alnahari\Mntry_Awqaf\Mntry_Awqaf\Controllers\TanjezOrderController.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

new_action = '''
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TanjezOrder>>(
                _urlApi
            );

            try
            {
                var workContractsUrl = _urlApi.Replace("TanjezOrders", "WorkContracts");
                var contracts = await _httpClient.GetFromJsonAsync<List<WorkContract>>(workContractsUrl);
                ViewBag.Contracts = contracts?
                    .Where(c => c.OrderId.HasValue)
                    .GroupBy(c => c.OrderId.Value)
                    .ToDictionary(g => g.Key, g => g.First().Id) ?? new Dictionary<int, int>();
            }
            catch (Exception ex)
            {
                ViewBag.Contracts = new Dictionary<int, int>();
                Console.WriteLine("Error fetching contracts: " + ex.Message);
            }

            try
            {
                // Fetch dynamic fields assignments
                var fieldsResponse = await _httpClient.GetStringAsync($"{_baseApiUrl}FieldAssignments/ByScreen/TanjezOrder");
                var assignments = JsonConvert.DeserializeObject<List<dynamic>>(fieldsResponse);
                
                // Extract unique dynamic fields to build table headers
                var dynamicFields = assignments.Select(a => new {
                    Id = (int)a.dynamicField.id,
                    Name = (string)a.dynamicField.name
                }).GroupBy(f => f.Id).Select(g => g.First()).ToList();
                ViewBag.DynamicFields = dynamicFields;

                // Fetch dynamic field values
                var valuesResponse = await _httpClient.GetStringAsync($"{_baseApiUrl}DynamicFieldValues/TanjezOrder");
                var dynamicValues = JsonConvert.DeserializeObject<List<DynamicFieldValue>>(valuesResponse);
                ViewBag.DynamicValues = dynamicValues;
            }
            catch (Exception ex)
            {
                ViewBag.DynamicFields = new List<dynamic>();
                ViewBag.DynamicValues = new List<DynamicFieldValue>();
                Console.WriteLine("Error fetching dynamic fields: " + ex.Message);
            }

            return View(response);
        }
'''

# Find the start of the old Index() method
index_start = content.find('public async Task<IActionResult> Index()')
if index_start != -1:
    # Find the end of the method by looking for 'return View(response);' and '}'
    index_end = content.find('return View(response);', index_start)
    index_end = content.find('}', index_end) + 1
    
    content = content[:index_start] + new_action + content[index_end:]
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)
    print('Index action replaced successfully!')
else:
    print('Could not find the Index method!')
