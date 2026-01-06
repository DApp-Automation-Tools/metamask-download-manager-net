using MetaMaskDownloadManager.Meta;
using MetaMaskDownloadManager.Models.GitHub;
using MetaMaskDownloadManager.Utils;
using System;
using System.Collections.Generic;

namespace MetaMaskDownloadManager.GitHub
{
    public static class GitHubSupport
    {
        private static readonly Dictionary<string, string> defaultGitHubHeaders = new()
        {
            { "Accept", "application/vnd.github+json" },
            { "User-Agent", "HttpClient" }
        };

        static GitHubSupport()
        {
            var gitHubToken = Environment.GetEnvironmentVariable(Constants.GITHUB_TOKEN_ENVIRONMENT_VARIABLE_KEY);
            if (!string.IsNullOrEmpty(gitHubToken))
            {
                if (!gitHubToken.Contains("Bearer", StringComparison.InvariantCultureIgnoreCase))
                {
                    gitHubToken = "Bearer " + gitHubToken;
                }
                defaultGitHubHeaders.Add("Authorization", gitHubToken);
            }
        }


        public static GitHubReleaseResponseItem GetLatestRelease()
        {
            string url = GetLatestReleaseUrl();
            return SupportUtils.GetJson<GitHubReleaseResponseItem>(url, headers: defaultGitHubHeaders).Result;
        }

        public static GitHubReleaseResponseItem GetReleaseByTagName(string tagName)
        {
            if (!tagName.Contains(Constants.METAMASK_VERSION_PREFIX, StringComparison.InvariantCultureIgnoreCase))
            {
                tagName = Constants.METAMASK_VERSION_PREFIX + tagName;
            }
            string url = GetReleaseByTagNameUrl(tagName);
            return SupportUtils.GetJson<GitHubReleaseResponseItem>(url, headers: defaultGitHubHeaders).Result;
        }

        public static List<GitHubReleaseResponseItem> GetAllReleases(int pageSize = Constants.GITHUB_MAX_PAGE_SIZE)
        {
            int currentPage = 1;
            bool hasMoreData = true;

            var resultList = new List<GitHubReleaseResponseItem>();
            while (hasMoreData)
            {
                string url = GetReleasesUrl(page: currentPage, pageSize: pageSize);
                var data = SupportUtils.GetJson<List<GitHubReleaseResponseItem>>(url, headers: defaultGitHubHeaders).Result;

                resultList.AddRange(data);

                if (data.Count == 0)
                {
                    hasMoreData = false;
                }
                else
                {
                    currentPage++;
                }
            }
            return resultList;
        }

        private static string GetReleasesUrl(int page, int pageSize)
        {
            return $"https://api.github.com/repos/metamask/metamask-extension/releases?per_page={pageSize}&page={page}";
        }

        private static string GetReleaseByTagNameUrl(string tag)
        {
            return $"https://api.github.com/repos/metamask/metamask-extension/releases/tags/{tag}";
        }

        private static string GetLatestReleaseUrl()
        {
            return "https://api.github.com/repos/metamask/metamask-extension/releases/latest";
        }
    }
}
