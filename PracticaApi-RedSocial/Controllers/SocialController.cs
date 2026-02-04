using Microsoft.AspNetCore.Mvc;
using PracticaApi_RedSocial.Helpers;
using PracticaApi_RedSocial.Models;
using PracticaApi_RedSocial.Services;
using System.Text.Json;

namespace PracticaApi_RedSocial.Controllers
{
    public class SocialController : Controller
    {
        private readonly MastodonService _mastodonService;

        public SocialController (MastodonService mastodonService)
        {
            _mastodonService = mastodonService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? keyword, bool topInteraction = false)
        {
            // Obtener muchos posts (paginacion)
            var posts = await _mastodonService.GetPublicPostsAsync();

            // Filtro por palabra clave
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                posts = posts
                    .Where(p =>
                        !string.IsNullOrEmpty(p.Content) &&
                        p.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Calcular engagement
            foreach (var post in posts)
            {
                post.EngagementScore =
                    post.FavouritesCount * 1 +
                    post.ReblogsCount * 3 +
                    post.RepliesCount * 2;
            }

            // Top interacciones / tendencia
            if (topInteraction)
            {
                posts = posts
                    .Where(p => p.EngagementScore >= 5) // umbral mínimo
                    .OrderByDescending(p => p.EngagementScore)
                    .Take(20)
                    .ToList();
            }

            // Enviar a la vista
            return View(posts);
        }
    }
}