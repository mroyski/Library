using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Library.Shared;
using Microsoft.Extensions.Configuration;

namespace Library.App.Services
{

    public class DataService : IDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public DataService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var url = _config.GetValue<string>("LibraryApiUrl");
            var response = await _httpClient.GetAsync($"{url}/api/books");
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Book>>(data,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
