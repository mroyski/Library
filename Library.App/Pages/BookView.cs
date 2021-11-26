using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Library.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace Library.App.Pages
{
    public partial class BookView
    {
        static readonly HttpClient _httpClient = new HttpClient();
        static string LibraryApiAddress { get; set; }
        public IEnumerable<Book> Books { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        static async Task<IEnumerable<Book>> GetBooks()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Book>>
            (await _httpClient.GetStreamAsync($"{LibraryApiAddress}/api/books"), new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        protected async override Task OnInitializedAsync()
        {
            LibraryApiAddress = Configuration["Library.Api.Address"];
            Books = await GetBooks();
        }
    }
}
