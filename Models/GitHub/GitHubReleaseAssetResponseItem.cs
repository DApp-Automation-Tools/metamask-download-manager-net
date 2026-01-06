using System.Text.Json.Serialization;

namespace MetaMaskDownloadManager.Models.GitHub
{
    public class GitHubReleaseAssetResponseItem
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }
        [JsonPropertyName("browser_download_url")]
        public string BrowserDownloadUrl { get; set; }
    }
}
