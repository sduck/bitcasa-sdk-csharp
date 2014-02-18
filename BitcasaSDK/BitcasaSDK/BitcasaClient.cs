using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BitcasaSdk.Dao;
using BitcasaSdk.Dao.Converters;
using System.Threading.Tasks;
using BitcasaSDK.Exception;
using BitcasaSdk.Http;
using Newtonsoft.Json;

namespace BitcasaSdk
{
    public class BitcasaClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private IHttpRequestor _httpRequestor;

        public string AccessToken { get; private set; }

        public IHttpRequestor HttpRequestor
        {
            get { return _httpRequestor; }
            set { _httpRequestor = value; }
        }

        public BitcasaClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _httpRequestor = new HttpRequestor();
        }

        public string GetAuthenticateUrl()
        {
            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.Oauth);
            urlBuilder.AddParameter(Constants.Parameters.ClientId, _clientId);
            urlBuilder.AddParameter(Constants.Parameters.ResponseType, "code");

            return urlBuilder.ToString();
        }

        public async Task RequestAccessToken(string authorizationCode)
        {
            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.AccessToken);
            urlBuilder.AddParameter(Constants.Parameters.Secret, _clientSecret);
            urlBuilder.AddParameter(Constants.Parameters.Code, authorizationCode);

            var result = await _httpRequestor.GetString(HttpMethod.Get, urlBuilder.ToString());

            var response = JsonConvert.DeserializeObject<Response>(result);

            if (null == response)
            {
                throw new BitcasaSdkAuthenticationException("Failed parsing the response from Bitcasa");
            }

            if (response.HasError())
            {
                throw new BitcasaSdkAuthenticationException(response.Error);
            }

            AccessToken = response.Result.AccessToken;
        }

        public async Task<List<Item>> GetItemsInFolder(Folder folder)
        {
            ValidateAccessToken();

            List<Item> result = null;

            // Inspired by JavaSdk, we do some "magic" here
            var syncType = null == folder ? (SyncType?)null : folder.SyncType;

            if (null == folder || SyncType.Device == syncType || SyncType.MirroredFolder == syncType)
            {
                var items = await GetItemsInPath(null);

                var infiniteDrive = ExtractInfiniteDrive(items);
                var devices = ExtractMirroredDevices(items);

                switch (syncType)
                {
                    case SyncType.MirroredFolder:
                        return devices.Keys.ToList<Item>();
                    case SyncType.Device:
                        return devices[folder];
                    default:
                    {
                        var infDriveContent = await GetItemsInFolder(infiniteDrive);
                        items.AddRange(infDriveContent);
                        result = items;
                    }
                        break;
                }
            }
            else
            {
                result = await GetItemsInPath(folder.Path);
            }

            return result;
        }

        public void DownloadFile()
        {

        }

        private async Task<List<Item>> GetItemsInPath(string path)
        {
            path = path ?? "/";

            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.Folders, path);
            urlBuilder.AddParameter(Constants.Parameters.AccessToken, AccessToken);

            var result = await _httpRequestor.GetString(HttpMethod.Get, urlBuilder.ToString());

            var response = JsonConvert.DeserializeObject<Response>(result, new ItemConverter(), new BitcasaTimeConverter());

            if (response.HasError())
            {
                throw new BitcasaSdkServerException(response.Error);
            }

            return response.Result.Items;
        }

        private void ValidateAccessToken()
        {
            if (null == AccessToken)
            {
                throw new BitcasaSdkAuthenticationException("No Access Token found");
            }
        }

        private Folder ExtractInfiniteDrive(List<Item> items)
        {
            var infiniteDrive = items.FirstOrDefault(i => i.SyncType == SyncType.InfiniteDrive) as Folder;

            if (null != infiniteDrive)
            {
                items.Remove(infiniteDrive);
            }

            return infiniteDrive;
        }

        private Dictionary<Folder, List<Item>> ExtractMirroredDevices(IList<Item> items)
        {
            Dictionary<Folder, List<Item>> mirroredDevices = null;

            var mirrored = (from item in items
                where item.SyncType == SyncType.Backup || item.SyncType == SyncType.Sync
                select item).ToList();

            if (mirrored.Count > 0)
            {
                mirroredDevices = new Dictionary<Folder, List<Item>>();

                foreach (var item in mirrored)
                {
                    var device = new Folder()
                    {
                        Category = Category.Folders,
                        SyncType = SyncType.Device,
                        Mirrored = true,
                        Type = ItemType.Folder,
                        Name = item.OriginDevice ?? "No Device Name"
                    };

                    if (!mirroredDevices.ContainsKey(device))
                    {
                        mirroredDevices.Add(device, new List<Item>());
                    }
                    mirroredDevices[device].Add(device);
                    items.Remove(item);
                }

                items.Add(new Folder()
                {
                    Category = Category.Folders,
                    SyncType = SyncType.MirroredFolder,
                    Name = "Mirrored Folders",
                    Type = ItemType.Folder,
                    Mirrored = true
                });
            }

            return mirroredDevices;
        }
    }
}
