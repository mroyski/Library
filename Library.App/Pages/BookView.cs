using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Library.App.Services;
using Library.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace Library.App.Pages
{
    public partial class BookView
    {
        // static readonly HttpClient _httpClient = new HttpClient();
        [Inject]
        public IDataService DataService { get; set; }
        static string LibraryApiUrl { get; set; }
        public IEnumerable<Book> Books { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Books = await DataService.GetBooks();
        }
    }
}
