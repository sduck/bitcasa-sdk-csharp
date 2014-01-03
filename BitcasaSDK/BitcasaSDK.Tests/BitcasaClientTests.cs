using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BitcasaSdk.Dao;
using BitcasaSdk.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BitcasaSdk.Tests
{
    [TestClass]
    public class BitcasaClientTests
    {
        [TestMethod]
        public async Task RequestAccessToken()
        {
            // arrange
            const string output = @"{   ""error"" : null ,
                             ""result"" :{
                                 ""access_token"" : ""access_token""
                             }
                        }";
            var requestorMock = new Mock<IHttpRequestor>();
            var tcs = new TaskCompletionSource<string>();
            tcs.SetResult(output);
            requestorMock.Setup(req => req.GetString(HttpMethod.Get, It.IsAny<string>())).Returns(tcs.Task);
            var client = new BitcasaClient("", "")
            {
                HttpRequestor = requestorMock.Object
            };

            // act
            await client.RequestAccessToken("123");

            // assert
            requestorMock.Verify(req => req.GetString(HttpMethod.Get, It.IsAny<string>()), Times.Once);
            Assert.AreEqual("access_token", client.AccessToken);
        }        
        
        [TestMethod]
        public async Task GetFoldersList()
        {
            #region JSON
            // arrange
            const string output = @"             {
    ""result"": {
        ""items"": [
            {
                ""category"": ""folders"",
                ""mount_point"": ""C:/Users/Steffan/Documents"",
                ""name"": ""Documents"",
                ""deleted"": false,
                ""mirrored"": true,
                ""origin_device"": ""SANULTRA"",
                ""mtime"": 1386619684000,
                ""origin_device_id"": ""3267088014"",
                ""path"": ""/WGzxb5WRRWiOH1SxDlgQag"",
                ""type"": 1,
                ""sync_type"": ""backup""
            },
            {
                ""category"": ""folders"",
                ""mount_point"": ""I:"",
                ""name"": ""Bitcasa Infinite Drive"",
                ""deleted"": false,
                ""mirrored"": false,
                ""origin_device"": null,
                ""mtime"": 1386773228000,
                ""origin_device_id"": null,
                ""path"": ""/1f8YpTAnS7-leSDcQseowQ"",
                ""type"": 1,
                ""sync_type"": ""infinite drive""
            },
            {
                ""category"": ""folders"",
                ""mount_point"": ""C:/Users/san/Documents/Projects"",
                ""name"": ""Projects"",
                ""deleted"": false,
                ""mirrored"": true,
                ""origin_device"": ""SANX1"",
                ""mtime"": 1386078574000,
                ""origin_device_id"": ""2187488650"",
                ""path"": ""/JYK42zg3QFq9Gkfp4J6X-g"",
                ""type"": 1,
                ""sync_type"": ""backup""
            }
        ]
    },
    ""error"": null
}
";
            #endregion

            var requestorMock = new Mock<IHttpRequestor>();
            var tcs = new TaskCompletionSource<string>();
            tcs.SetResult(output);
            requestorMock.Setup(req => req.GetString(HttpMethod.Get, It.IsAny<string>())).Returns(tcs.Task);
            var client = new BitcasaClient("", "")
            {
                HttpRequestor = requestorMock.Object
            };
            typeof(BitcasaClient).GetProperty("AccessToken").SetValue(client, "token", null);


            // act
            var result = await client.GetItemsInFolder(null);

            // assert
            requestorMock.Verify(req => req.GetString(HttpMethod.Get, It.IsAny<string>()), Times.AtLeastOnce);
            Assert.IsInstanceOfType(result, typeof(List<Item>));
            Assert.AreEqual(4, result.Count);

            var sut = result[1];
            Assert.IsInstanceOfType(sut, typeof(Folder));
            Assert.AreEqual(Category.Folders, sut.Category);
            Assert.AreEqual(ItemType.Folder, sut.Type);
            Assert.AreEqual(SyncType.Backup, sut.SyncType);
            Assert.AreEqual("Documents", sut.Name);
            Assert.AreEqual("/WGzxb5WRRWiOH1SxDlgQag", sut.Path);
            Assert.AreEqual(new DateTime(2013, 12, 9, 21, 8, 4), sut.ModifiedTime);
            Assert.AreEqual(true, sut.Mirrored);
            Assert.AreEqual("C:/Users/Steffan/Documents", sut.MountPoint);
            Assert.AreEqual(false, sut.Deleted);
            Assert.AreEqual("SANULTRA", sut.OriginDevice);
            Assert.AreEqual("3267088014", sut.OriginDeviceId);

            sut = result[2];
            Assert.IsInstanceOfType(sut, typeof(Folder));
            Assert.AreEqual(Category.Folders, sut.Category);
            Assert.AreEqual(ItemType.Folder, sut.Type);
            Assert.AreEqual(SyncType.InfiniteDrive, sut.SyncType);
            Assert.AreEqual("Bitcasa Infinite Drive", sut.Name);
            Assert.AreEqual("/1f8YpTAnS7-leSDcQseowQ", sut.Path);
            Assert.AreEqual(new DateTime(2013, 12, 11, 15, 47, 8), sut.ModifiedTime);
            Assert.AreEqual(false, sut.Mirrored);
            Assert.AreEqual("I:", sut.MountPoint);
            Assert.AreEqual(false, sut.Deleted);
            Assert.IsNull(sut.OriginDevice);
            Assert.IsNull(sut.OriginDeviceId);
        }
    }
}
