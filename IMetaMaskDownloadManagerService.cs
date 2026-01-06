using MetaMaskDownloadManager.Meta;
using MetaMaskDownloadManager.Models.Common;
using System.Collections.Generic;

namespace MetaMaskDownloadManager
{
    public interface IMetaMaskDownloadManagerService
    {
        public string DownloadMetaMask(DownloadManagerOptions options = null);

        public ICollection<string> GetAvailableMetaMaskVersions(CustomBrowserType browser = CustomBrowserType.None);
    }
}
