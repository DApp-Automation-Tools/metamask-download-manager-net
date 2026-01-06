using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetaMaskDownloadManager.Utils
{
    public static class SupportUtils
    {
        public static async Task DownloadLargeFileAsync(string fileUrl, string fullFilePath)
        {
            using var client = new HttpClient();
            byte[] fileBytes = await client.GetByteArrayAsync(fileUrl);
            await File.WriteAllBytesAsync(fullFilePath, fileBytes);
        }

        public static async Task<T> GetJson<T>(string url, Dictionary<string, string> headers = null, JsonSerializerOptions options = null)
        {
            using var client = new HttpClient();
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            T data = JsonSerializer.Deserialize<T>(jsonResponse, options);
            return data;
        }

        public static void UnzipFile(string zipFilePath, string extractPath, bool overwriteFiles)
        {
            if (!Directory.Exists(extractPath))
            {
                Directory.CreateDirectory(extractPath);
            }

            ZipFile.ExtractToDirectory(zipFilePath, extractPath, overwriteFiles: overwriteFiles);
        }

        public static bool IsDirectoryExistsAndIsNotEmpty(string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            bool hasFiles = Directory.EnumerateFiles(path).Any();
            bool hasSubdirectories = Directory.EnumerateDirectories(path).Any();

            return hasFiles || hasSubdirectories;
        }
    }
}
