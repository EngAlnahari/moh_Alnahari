using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
// هذا المتحكم الرئيسي للدوال http بدلا من تكرارها في جميع المتحكمات وايضا لسهوله الصيانه

public class BaseApiController<T> : Controller where T : class
{
    protected readonly HttpClient _httpClient;
    protected readonly string _urlApi;

    public BaseApiController(HttpClient httpClient, IConfiguration configuration, string apiEndpoint)
    {
        _httpClient = httpClient;
        //  نجلب الرابط ارئيسي او الدومين من appsettings.json
        var baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        _urlApi = $"{baseUrl}{apiEndpoint}";
    }
    // من الممكن استخدام returnpartiolview لجل الموقع اسرع
    // GET ALL
    protected async Task<List<T>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(_urlApi);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(data);
        }
        return new List<T>();
    }

    // GET BY ID
    protected async Task<T> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<T>($"{_urlApi}/{id}");
    }
    //string baseUrls = "https://localhost:7027/api"; // الرابط الأساسي

    //protected async Task<T> GetModelAsync(string endpoint)
    //{
    //    return await _httpClient.GetFromJsonAsync<T>($"{baseUrls}/{endpoint}");
    //}

    // CREATE
    protected async Task<bool> CreateAsync(T entity)
    {
        var response = await _httpClient.PostAsJsonAsync(_urlApi, entity);
        return response.IsSuccessStatusCode;
    }

    // EDIT
    protected async Task<bool> EditAsync(int id, T entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_urlApi}/{id}", entity);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    protected async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_urlApi}/{id}");
        return response.IsSuccessStatusCode;
    }
}
