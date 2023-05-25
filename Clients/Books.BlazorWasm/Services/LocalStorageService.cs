using Books.BlazorWasm.Contracts;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Books.BlazorWasm.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jSRuntime;

        public LocalStorageService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            string? json = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", key);
            if (json is null)
                return default;
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _jSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetItemAsync<T>(string key, T item)
        {
            await _jSRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonConvert.SerializeObject(item));
        }
    }
}
