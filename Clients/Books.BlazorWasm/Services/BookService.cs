using AutoMapper;
using Books.BlazorWasm.Auth;
using Books.BlazorWasm.Contracts;
using Books.BlazorWasm.Exceptions;
using Books.BlazorWasm.External.Models.BookDtos;
using Books.BlazorWasm.Models.Books;
using Books.BlazorWasm.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace Books.BlazorWasm.Services
{
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;

        public BookService(IMapper mapper, IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {

            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
        }

        public async Task<(List<BookOverviewViewModel>, PaginationMetadata?)> GetBooksList(int pageNumber, int pageSize)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("Books.API");
            var response = await httpClient.GetAsync($"api/books?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<BookDto>>(content);
            var booksViewModel = _mapper.Map<List<BookOverviewViewModel>>(books);

            PaginationMetadata? paginationMetadata = null;
            if (response.Headers.TryGetValues("X-Pagination", out var headerValues))
            {
                var headerValue = headerValues.FirstOrDefault();
                paginationMetadata = JsonConvert.DeserializeObject<PaginationMetadata>(headerValue!)!;
            }
            return (booksViewModel, paginationMetadata);
        }

        public async Task<BookDetailsViewModel> GetBookByIdAsync(Guid bookId, bool includeCovers)
        {
            var httpClient = _httpClientFactory.CreateClient("Books.API");
            var response = await httpClient.GetAsync($"api/books/{bookId}?includeCovers={includeCovers}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<BookDetailDto>(content);
                var bookDetailsViewModel = _mapper.Map<BookDetailsViewModel>(books);
                return bookDetailsViewModel;
            }
            else
            {
                var problemDetailsContent = await response.Content.ReadAsStringAsync();
                var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(problemDetailsContent);
                throw new ApiException(problemDetails);
            }
        }

        public async Task<FileExportModel> DownloadCsv()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("Books.API");
            string? token = await _localStorageService.GetItemAsync<string>(AuthUtilities.LocalStorageTokenKey);
            if (token is null)
                throw new UnauthorizedException();
            HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, "api/books/export");
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                byte[] fileContent = await httpResponseMessage.Content.ReadAsByteArrayAsync();
                string? fileName = httpResponseMessage.Content.Headers.ContentDisposition?.FileName;
                string? mediaType = httpResponseMessage.Content.Headers.ContentType?.MediaType;
                var file = new FileExportModel
                {
                    FileName = fileName ?? "books-csv",
                    ContentType = mediaType ?? "text/csv",
                    Data = fileContent
                };
                return file;
            }
            else
            {
                var problemDetailsContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(problemDetailsContent);
                throw new ApiException(problemDetails);
            }
        }
    }
}
