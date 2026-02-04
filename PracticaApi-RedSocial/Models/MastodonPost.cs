using System.Text.Json.Serialization;

namespace PracticaApi_RedSocial.Models
{
    public class MastodonPost
    {
        public required string Id { get; set; }
        public required string Content { get; set; }
        public required MastodonAccount Account { get; set; }

        [JsonPropertyName("replies_count")]
        public int RepliesCount { get; set; }

        [JsonPropertyName("reblogs_count")]
        public int ReblogsCount { get; set; }

        [JsonPropertyName("favourites_count")]
        public int FavouritesCount { get; set; }

        public int EngagementScore { get; set; }
    }

    public class MastodonAccount
    {
        public required string Username { get; set; }
        public string? DisplayName { get; set; }
    }
}
