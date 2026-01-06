using MetaMaskDownloadManager.Meta;
using System.IO;

namespace MetaMaskDownloadManager.Models.Common
{
    public class DownloadManagerOptions
    {
        public string DownloadPath { get; set; } = Path.GetTempPath();
        public CustomBrowserType BrowserType { get; set; } = CustomBrowserType.Chrome;
        public string Version { get; set; } = Constants.LATEST_VERSION;
        public bool IsForseDownload { get; set; } = false;
    }
}
