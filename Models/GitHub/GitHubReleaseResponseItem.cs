using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MetaMaskDownloadManager.Models.GitHub
{
    public class GitHubReleaseResponseItem
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }
        [JsonPropertyName("assets")]
        public List<GitHubReleaseAssetResponseItem> Assets { get; set; }
    }
}
