# MetaMask Download Manager

This C# library allows you to download MetaMask releases directly from GitHub, supporting multiple browsers and handling GitHub API rate limits via token authentication.

|Nuget Package | Statistics |
|-|-|
|*MetaMaskDownloadManager*|[![NuGet](https://img.shields.io/nuget/v/MetaMaskDownloadManager.svg)](https://www.nuget.org/packages/MetaMaskDownloadManager/) ![](https://img.shields.io/nuget/dt/MetaMaskDownloadManager)|

## Features

- Download the latest or a specific version of MetaMask from GitHub.
- Support for multiple browsers (e.g., Chrome, Firefox).
- Fetch available MetaMask versions.
- Uses GitHub API for release information and asset downloads.
- Supports GitHub token authentication to avoid API rate limits.

## Requirements

- .NET 8.0 or later

## Setup

1. **Install the package from NuGet:**
   ```sh
   dotnet add package MetaMaskDownloadManager
2. **Set up the GitHub token:**
   
   To avoid GitHub rate limit restrictions, set the `GITHUB_TOKEN` environment variable with your personal GitHub token:
   ```sh
   export GITHUB_TOKEN=your_github_token


## Usage

### Download MetaMask

To download MetaMask, use the MetaMaskDownloadManagerService class:

```csharp
using MetaMaskDownloadManager;

class Program
{
    static void Main(string[] args)
    {
        var service = new MetaMaskDownloadManagerService();
        var options = new DownloadManagerOptions
        {
            Browser = CustomBrowserType.Chrome,
            Version = "latest",
            DestinationFilePath = "/path/to/download"
        };
        string path = service.DownloadMetaMask(options);
        Console.WriteLine($"MetaMask downloaded to: {path}");
    }
}
```

### List Available MetaMask Versions

To get a list of available MetaMask versions:
```csharp
using MetaMaskDownloadManager;

class Program
{
    static void Main(string[] args)
    {
        var service = new MetaMaskDownloadManagerService();
        var versions = service.GetAvailableMetaMaskVersions(CustomBrowserType.Chrome);
        foreach (var version in versions)
        {
            Console.WriteLine(version);
        }
    }
}
```

## Notable Features

- GitHub API Integration: Fetches release information and downloads assets using the GitHub API.
- Token Authentication: Uses a GitHub token to avoid API rate limits.
- Flexible Download Options: Supports specifying browser type and version.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

Branch naming convention:
- Breaking changes - `major/${meaningful change name}`
- Significant changes -  `feature/${meaningful change name}`
- Small insignificant changes or fixes - `fix/${meaningful change name}`

**Branch naming affects versioning!!!**

## License

This project is licensed under the MIT License.


## Appreciation
Give it a Star! :star:

If you liked the project or if it helped you, please **give it a star**.
