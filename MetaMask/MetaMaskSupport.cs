using MetaMaskDownloadManager.Meta;
using System.Linq;

namespace MetaMaskDownloadManager.MetaMask
{
    public static class MetaMaskSupport
    {
        public static string GetMetaMaskReleasePatternShort(CustomBrowserType browser)
        {
            return $@"metamask-{browser.ToString().ToLowerInvariant()}-\d+(\.\d+)*\.zip$";
        }

        public static string GetMetaMaskFolderName(CustomBrowserType browser, string version)
        {
            if (version.StartsWith(Constants.METAMASK_VERSION_PREFIX))
            {
                version = new string(version.Skip(1).ToArray());
            }

            return $"metamask-{browser.ToString().ToLowerInvariant()}-{version}";
        }
    }
}
