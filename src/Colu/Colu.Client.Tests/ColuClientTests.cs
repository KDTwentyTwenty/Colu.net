﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Colu;

namespace ColuClient.Tests
{
    [TestClass]
    public class ColuClientTests
    {
        private const String MAINNET_HOST = "http://bitcoinaa3.cloudapp.net:8081";
        private const String TESTNET_HOST = "http://autarky.cloudapp.net:8081";
        private const String HOME_HOST = "http://101.165.103.43:8081";

        [TestMethod]
        public async Task Should_Get_Private_Seed()
        {
            using (Client client = new Client(HOME_HOST))
            {
                var response = await client.GetPrivateSeed();

                Assert.IsFalse(String.IsNullOrEmpty(response.Result));
            }
        }

        [TestMethod]
        public async Task Should_Get_HD_Address()
        {
            using (IAddressClient client = new Client(MAINNET_HOST))
            {
                String id = Guid.NewGuid().ToString();
                var response = await client.GetAddressAsync(id);

                Assert.IsFalse(String.IsNullOrEmpty(response.Address));
                Assert.AreEqual(id, response.Id);
            }
        }

        [TestMethod]
        public async Task Should_Get_Asset_Holders()
        {
            const String BITPOKER_ASSET_ID = "Ua9V5JgADia5zJdSnSTDDenKhPuTVc6RbeNmsJ";

            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.GetStakeHolders.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };
                request.Params.AssetId = BITPOKER_ASSET_ID;
                request.Params.NumberConfirmations = 1;

                var acutal = await client.GetStakeHoldersAsync(request);
                Assert.IsNotNull(acutal);
                Assert.IsNotNull(acutal.Result);
                Assert.AreEqual(BITPOKER_ASSET_ID, acutal.Result.AssetId);
                Assert.IsTrue(acutal.Result.Holders.Count > 0);
            }
        }

        [TestMethod]
        public async Task Should_Get_Asset_Holders_2()
        {
            const String BITPOKER_ASSET_ID = "Ua9V5JgADia5zJdSnSTDDenKhPuTVc6RbeNmsJ";

            using (Client client = new Client(TESTNET_HOST))
            {
                var acutal = await client.GetStakeHoldersAsync(BITPOKER_ASSET_ID, 1);
                Assert.IsNotNull(acutal);
                Assert.IsNotNull(acutal.Result);
                Assert.AreEqual(BITPOKER_ASSET_ID, acutal.Result.AssetId);
                Assert.IsTrue(acutal.Result.Holders.Count > 0);
            }
        }

        [TestMethod]
        public async Task Should_Issue_Asset()
        {
            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.IssueAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.Param.Amount = 1000;
                request.Param.Divisibility = 0;
                request.Param.Reissueable = false;
                //request.Param.IssueAddress = "1MbLLmDNc3UmZcvDb2Qv128aTdtPHYiP5N";
                request.Param.IssueAddress = "mftCzxjSGWRRXh5QDKaTpsCXWmGNEtHX3S";

                //IS TO REQUIRED?
                //request.Param.IssueAddress = "mkNCMkfqKJaR5Ex1gjk9rKFqePk95kDVaC";
                //request.to.Add(new Models.To() { PhoneNumber = "61407928417", Amount = 1 });

                //request.Params.AssetId = "Ua9V5JgADia5zJdSnSTDDenKhPuTVc6RbeNmsJ";
                //request.Params.numConfirmations = "0";

                var acutal = await client.IssueAsync(request);
                Assert.IsNotNull(acutal);
            }
        }

        [TestMethod]
        public async Task Should_Issue_Asset_With_Metadata()
        {
            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.IssueAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.Param.Amount = 100;
                request.Param.Divisibility = 0;
                request.Param.Reissueable = false;
                request.Param.IssueAddress = "1DNjKtYCjrJJgQCkzYqSfrcd8ahzBZXPzR";

                request.Param.MetaData.AssetName = "General Fisheries Permit";
                request.Param.MetaData.Issuer = "Queensland Government";

                var acutal = await client.IssueAsync(request);
                Assert.IsNotNull(acutal);
            }
        }

        [TestMethod]
        public async Task Should_Issue_Asset_With_Metadata_And_Image()
        {
            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.IssueAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.Param.Amount = 10;
                request.Param.Divisibility = 0;
                request.Param.Reissueable = false;
                request.Param.IssueAddress = "192Qdmz576DugRecfECXJrpHojuBguzip6"; //"1DNjKtYCjrJJgQCkzYqSfrcd8ahzBZXPzR";

                request.Param.MetaData.AssetName = "General Fisheries Permit";
                request.Param.MetaData.Issuer = "Queensland Government";

                var iconUrl = new Colu.Models.IssueAsset.Url()
                {
                    Name = "icon",
                    MimeType = "image/png",
                    url = "https://blockchainpermits.azurewebsites.net/images/Fishing-Licence2.png"
                };
                request.Param.MetaData.Urls.Add(iconUrl);

                var acutal = await client.IssueAsync(request);
                Assert.IsNotNull(acutal);
            }
        }

        [TestMethod]
        public async Task Should_Issue_Asset_With_Metadata_And_Verification()
        {
            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.IssueAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.Param.Amount = 1;
                request.Param.Divisibility = 0;
                request.Param.Reissueable = false;
                request.Param.IssueAddress = "mjD34XbQWnccNrfPs4G1pEJZr8mjKhS8A2";

                request.Param.MetaData.AssetName = "Test Bitpoker";
                request.Param.MetaData.Verification = new Colu.Models.IssueAsset.Verification();
                request.Param.MetaData.Verification.Domain = new Colu.Models.IssueAsset.Domain() { url = "https://www.bitpoker.io/assets.txt" };

                var acutal = await client.IssueAsync(request);
                Assert.IsNotNull(acutal);
            }
        }

        [TestMethod]
        public async Task Should_Issue_And_Transfer_Asset()
        {
            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.IssueAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.Param.Amount = 10;
                request.Param.Divisibility = 0;
                request.Param.Reissueable = false;
                //request.Param.IssueAddress = "1MbLLmDNc3UmZcvDb2Qv128aTdtPHYiP5N";
                request.Param.IssueAddress = "mftCzxjSGWRRXh5QDKaTpsCXWmGNEtHX3S";

                //IS TO REQUIRED?
                //request.Param.IssueAddress = "mkNCMkfqKJaR5Ex1gjk9rKFqePk95kDVaC";
                //mjgNvJhN6g8rkANZZKqpPQDNtrMux5LvT9 random bitaddress address
                request.Transfer.Add(new Colu.Models.To() { address = "mjgNvJhN6g8rkANZZKqpPQDNtrMux5LvT9", Amount = 1 });

                //request.Params.AssetId = "Ua9V5JgADia5zJdSnSTDDenKhPuTVc6RbeNmsJ";
                //request.Params.numConfirmations = "0";

                var acutal = await client.IssueAsync(request);
                Assert.IsNotNull(acutal);
            }
        }

        [TestMethod]
        public async Task Should_Send_Asset()
        {
            const String TESTNET_ASSET_ID = "La7xWL4k6mr5h5Yi8p3YmN3oxaKPn7x8Ub3YUG";
            const String TEST_NET_ADDRESS = "mftCzxjSGWRRXh5QDKaTpsCXWmGNEtHX3S";

            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.SendAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.param.from.Add(TEST_NET_ADDRESS);

                request.param.to.Add(new Colu.Models.To() { address= "mkK8GmN4q5TnPEZkJmY6LVa5i5kimxwNXB", Amount = 1, AssetId = TESTNET_ASSET_ID });
                var acutal = await client.SendAssetAsync(request);
                Assert.IsNotNull(acutal);
                Assert.IsNotNull(acutal.Result);
                Assert.IsNotNull(acutal.Result.TxId);
            }
        }

        [TestMethod]
        public async Task Should_Send_Asset_via_Phone()
        {
            //fishing permit
            const String ASSET_ID = "LaAXAraoJfPYRovBtR4DctaLsxiHEcAuBwMWGb";

            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.SendAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.param.from.Add("1DNjKtYCjrJJgQCkzYqSfrcd8ahzBZXPzR");
                request.param.to.Add(new Colu.Models.To() { PhoneNumber = "61407928417", Amount = 1, AssetId = ASSET_ID });

                var acutal = await client.SendAssetAsync(request);
                Assert.IsNotNull(acutal);
                Assert.IsNotNull(acutal.Result);
                Assert.IsNotNull(acutal.Result.TxId);
            }
        }

        [TestMethod, TestCategory("Testnet")]
        public async Task Should_Send_Asset_via_Autarky()
        {
            const String ASSET_ID = "Ua5QZpRpQLg5YsHY4h9QCCbbo6Z5HJBx6CUJvn";
            const String FROM_ADDRESS = "mkoffnrkGEMKvswwWXR2LWyXFan68B1UGv";
            const String TO_ADDRESS = "mvBea3GT2B7iUxESsDioTVcmP492bNzUmf";

            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.SendAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.param.from.Add(FROM_ADDRESS);

                request.param.to.Add(new Colu.Models.To() { address = TO_ADDRESS, Amount = 100, AssetId = ASSET_ID });
                var acutal = await client.SendAssetAsync(request);
                Assert.IsNotNull(acutal);
                Assert.IsNotNull(acutal.Result.TxId);
            }
        }

        [TestMethod, TestCategory("Testnet")]
        public async Task Should_Issue_Autarky_Asset()
        {
            using (Client client = new Client(TESTNET_HOST))
            {
                var request = new Colu.Models.IssueAsset.Request()
                {
                    Id = Guid.NewGuid().ToString()
                };

                request.Param.Amount = 1000;
                request.Param.Divisibility = 2;
                request.Param.Reissueable = true;
                request.Param.IssueAddress = "mzVuTFivD1bxTNWmYoY5NSB9VY4k1zhKDs";

                var acutal = await client.IssueAsync(request);
                Assert.IsNotNull(acutal);
            }
        }

        [TestMethod]
        public async Task Should_Get_Address_Info()
        {
            using (Client client = new Client(MAINNET_HOST))
            {
                //mkXE4k1JqY3fYQ6TJJVGyRze9dM7dE53PD
                //1DNjKtYCjrJJgQCkzYqSfrcd8ahzBZXPzR - Permits
                //mkK8GmN4q5TnPEZkJmY6LVa5i5kimxwNXB - Autarky
                var acutal = await client.GetAddressInfoAsync("1DNjKtYCjrJJgQCkzYqSfrcd8ahzBZXPzR");
                Assert.IsNotNull(acutal);
            }
        }

        [TestMethod]
        public async Task Should_Get_Asset_Data()
        {
            using (Client client = new Client(MAINNET_HOST))
            {
                var acutal = await client.GetAssetDataAsync("La61G4yCFETSbja4BC3QfKXmM3GUVu3eGHh3bn");
                Assert.IsNotNull(acutal);
            }
        }

        [TestMethod]
        public async Task Should_Get_Assets()
        {
            using (Client client = new Client("http://bitcoinaa3.cloudapp.net:8081"))
            {
                var acutal = await client.GetAssetsAsync();
                Assert.IsNotNull(acutal);
            }
        }
    }
}