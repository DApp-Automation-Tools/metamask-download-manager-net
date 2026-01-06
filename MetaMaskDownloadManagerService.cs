using MetaMaskDownloadManager.GitHub;
using MetaMaskDownloadManager.Meta;
using MetaMaskDownloadManager.MetaMask;
using MetaMaskDownloadManager.Models.Common;
using MetaMaskDownloadManager.Models.GitHub;
using MetaMaskDownloadManager.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MetaMaskDownloadManager
{
    public class MetaMaskDownloadManagerService : IMetaMaskDownloadManagerService
    {
        public string DownloadMetaMask(DownloadManagerOptions options = null)
        {
            if (options == null)
            {
                options = new DownloadManagerOptions();
            }

            var version = options.Version;
            var browser = options.BrowserType;
            var destinationFilePath = options.DownloadPath;
            var isForseDownload = options.IsForseDownload;

            //if false -> it means we need to check if we already downloaded metamask
            if (!isForseDownload)
            {
                //check if folder with unzipped MetaMask for the specified path & version exists and not empty
                var directoryNameExpected = MetaMaskSupport.GetMetaMaskFolderName(browser, version);
                var filePathToCheck = Path.Combine(destinationFilePath, directoryNameExpected);
                if (SupportUtils.IsDirectoryExistsAndIsNotEmpty(filePathToCheck))
                {
                    return filePathToCheck;
                }
            }

            GitHubReleaseResponseItem releaseResponseItem;

            if (version == Constants.LATEST_VERSION)
            {
                releaseResponseItem = GitHubSupport.GetLatestRelease();
            }
            else
            {
                releaseResponseItem = GitHubSupport.GetReleaseByTagName(version);
            }

            var asset = FindAsset(browser, releaseResponseItem.Assets);

            if (asset == null)
            {
                throw new Exception($"No release asset found for '{browser}' of version '{version}'");
            }

            var downloadUrl = asset.BrowserDownloadUrl;
            string fileName = downloadUrl.Split('/').Last();
            var fullFilePath = Path.Combine(destinationFilePath, fileName);
            SupportUtils.DownloadLargeFileAsync(fileUrl: downloadUrl, fullFilePath: fullFilePath).Wait();
            var extractPath = Path.Combine(destinationFilePath, Path.GetFileNameWithoutExtension(fullFilePath));

            SupportUtils.UnzipFile(zipFilePath: fullFilePath, extractPath: extractPath, overwriteFiles: true);
            Console.WriteLine($"MetaMask extension extracted to: {extractPath}");
            return extractPath;
        }

        public ICollection<string> GetAvailableMetaMaskVersions(CustomBrowserType browser = CustomBrowserType.None)
        {
            var allReleases = GitHubSupport.GetAllReleases();
            var pattern = MetaMaskSupport.GetMetaMaskReleasePatternShort(browser);
            return allReleases.Where(item =>
            {
                if (browser == CustomBrowserType.None)
                {
                    return true;
                }
                var asset = FindAsset(browser, item.Assets);
                return asset != null;
            }).Select(item => item.TagName.Replace(Constants.METAMASK_VERSION_PREFIX, string.Empty)).ToList();
        }

        private GitHubReleaseAssetResponseItem FindAsset(CustomBrowserType browser, ICollection<GitHubReleaseAssetResponseItem> assets)
        {
            var pattern = MetaMaskSupport.GetMetaMaskReleasePatternShort(browser);
            return assets.FirstOrDefault(asset => Regex.IsMatch(asset.Name, pattern));
        }
    }
}
