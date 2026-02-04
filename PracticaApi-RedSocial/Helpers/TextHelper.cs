using PracticaApi_RedSocial.Models;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace PracticaApi_RedSocial.Helpers
{
    public static class TextHelper
    {
        // Calcula el engagement score de una publicación de Mastodon
        public static int CalcularEngagement(MastodonPost post)
        {
            return post.FavouritesCount
                 + (post.ReblogsCount * 2)
                 + (post.RepliesCount * 3);
        }
        // Resalta todas las keyword encontradas en un texto con etiquetas HTML
        public static string ResaltarPalabra(string texto, string palabra)
        {
            if (string.IsNullOrEmpty(texto) || string.IsNullOrEmpty(palabra))
                return texto;

            return Regex.Replace(
                texto,
                Regex.Escape(palabra),
                match => $"<span class='highlight'>{match.Value}</span>",
                RegexOptions.IgnoreCase
            );
        }
        // Elimina todas las etiquetas HTML de un texto
        public static string LimpiarHtml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return Regex.Replace(input, "<.*?>", string.Empty);
        }
        // Normaliza un texto eliminando acentos y caracteres especiales
        public static string NormalizarTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            var normalized = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
