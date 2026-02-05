using System.Text.Json;
using PracticaApi_RedSocial.Models;

namespace PracticaApi_RedSocial.Services
{
    public class MastodonService
    {
        private readonly HttpClient _httpClient;

        public MastodonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Obtiene publicaciones públicas de Mastodon con paginación(limite de 40 por peticion a la api)
        public async Task<List<MastodonPost>> GetPublicPostsAsync(int pages = 5)
        {
            var allPosts = new List<MastodonPost>();
            string? maxId = null;

            for (int i = 0; i < pages; i++)
            {
                var url = "https://mastodon.world/api/v1/timelines/public?limit=40";

                if (!string.IsNullOrEmpty(maxId))
                    url += $"&max_id={maxId}";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("ERROR Mastodon: " + error);
                    break;
                }


                var json = await response.Content.ReadAsStringAsync();

                var posts = JsonSerializer.Deserialize<List<MastodonPost>>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (posts == null || posts.Count == 0)
                    break;

                allPosts.AddRange(posts);
                // delay entre peticiones para no saturar la API
                maxId = posts.Last().Id;
                await Task.Delay(300);
            }

            return allPosts;
        }
    }
}
