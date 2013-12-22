using System;
using System.Runtime.Serialization;
using BitcasaSdk.Dao.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BitcasaSdk.Dao
{

    public class Item
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Category Category { get; set; }

        public ItemType Type { get; set; }

        [JsonProperty("sync_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SyncType SyncType { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        [JsonProperty("mtime")]
        [JsonConverter(typeof(BitcasaTimeConverter))]
        public DateTime ModifiedTime { get; set; }

        public bool Mirrored { get; set; }

        [JsonProperty("mount_point")]
        public string MountPoint { get; set; }

        public bool Deleted { get; set; }

        [JsonProperty("origin_device")]
        public string OriginDevice { get; set; }

        [JsonProperty("origin_device_id")]
        public string OriginDeviceId { get; set; }
    }

    public enum Category
    {
        Folders
    }

    public enum ItemType
    {
        File,
        Folder
    }

    public enum SyncType
    {
        Backup,

        [EnumMember(Value = "infinite drive")]
        InfiniteDrive
    }
}
