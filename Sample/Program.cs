﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.Configuration;
using MK.IO;
using MK.IO.Models;
using System.Security.Cryptography;

namespace Sample
{
    internal class Program
    {
        static void Main()
        {
            // MainAsync().Wait();
            // or, if you want to avoid exceptions being wrapped into AggregateException:
            MainAsync().GetAwaiter().GetResult();
        }


        static async Task MainAsync()
        {
            Console.WriteLine("Sample that operates MK/IO.");


            /* you need to add an appsettings.json file with the following content:
             {
                "MKIOSubscriptionName": "yourMKIOsubscriptionname",
                "MKIOToken": "yourMKIOtoken"
             }
            */


            // Build a config object, using env vars and JSON providers.
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            Console.WriteLine($"Using '{config["MKIOSubscriptionName"]}' MK/IO subscription.");


            // **********************
            // MK/IO Client creation
            // **********************

            var client = new MKIOClient(config["MKIOSubscriptionName"], config["MKIOToken"]);

            var profile = client.GetUserInfo();

            // Get subscription stats
            var stats = client.GetStats();

            var lista = client.Assets.ListAsync();

            // **********************
            // live event operations
            // **********************

            var les = client.LiveEvents.List();

            // client.LiveEvents.Delete("liveevent4");

            var le = client.LiveEvents.Create("liveevent4", "francecentral", new LiveEventProperties
            {
                Input = new LiveEventInput { StreamingProtocol = "RTMP" },
                StreamOptions = new List<string> { "Default" },
                Encoding = new LiveEventEncoding { EncodingType = "PassthroughBasic" }
            });


            // *****************
            // asset operations
            // *****************

            // list assets
            //var mkioAssets = client.Assets.List("name desc", 4);


            var mkioAssetsResult = client.Assets.ListAsPage("name desc", 4);
            do
            {
                mkioAssetsResult = client.Assets.ListAsPageNext(mkioAssetsResult.NextPageLink);
            } while (mkioAssetsResult.NextPageLink != null);


            var specc = client.Assets.ListTracksAndDirListing("copy-ef2058b692-copy");

            // get streaming locators for asset
            var locatorsAsset = client.Assets.ListStreamingLocators("copy-1b510ee166-copy-d32391984a");

            // get asset
            var mkasset = client.Assets.Get("copy-152b839997");

            // create asset
            // var newasset = client.Assets.CreateOrUpdate("copy-ef2058b692-copy", "asset-2346d605-b4d6-4958-a80b-b4943b602ea8", "amsxpfrstorage", "description of asset copy");

            // delete asset
            //client.Assets.Delete("asset-33adc1873f");

            // *********************
            // transform operations
            // *********************

            /*
            var tranform = client.CreateTransform("mytesttranf", new TransformProperties
            {
                Description = "desc",
                Outputs = new List<TransformOutput>() {
                    new TransformOutput {
                        Preset = new BuiltInStandardEncoderPreset(EncoderNamedPreset.H264SingleBitrate720P),
                        RelativePriority = "Normal" } }
            });
            */


            // ***************
            // job operations
            // ***************

            var jobs = client.Jobs.ListAll();

            //var job = client.Jobs.Get("simple", "testjob1");

            /*
            var outputAsset = client.Assets.CreateOrUpdate("outputasset-012", "asset-outputasset-012", config["StorageName"], "output asset for job");

            client.Jobs.Create("simple", "testjob2", new JobProperties
            {
                Description = "My job",
                Priority = "Normal",
                Input = new JobInputAsset(
                    "copy-ef2058b692-copy",
                    new List<string> {
                        "switch_1920x1080_AACAudio_3677.mp4"
                    }),
                Outputs = new List<JobOutputAsset>()
                {
                    new JobOutputAsset()
                    {
                        AssetName="outputasset-012"
                    }
                }
            }
            );
            */

            var outputAsset = client.Assets.CreateOrUpdate("outputasset-014", "asset-outputasset-014", config["StorageName"], "output asset for job");

            client.Jobs.Create("simple", "testjob3", new JobProperties
            {
                Description = "My job",
                Priority = "Normal",
                Input = new JobInputHttp(
                    null,
                    new List<string> {
                        "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4"
                    }),
                Outputs = new List<JobOutputAsset>()
                {
                    new JobOutputAsset()
                    {
                        AssetName="outputasset-014"
                    }
                }
            }
            );

            client.Jobs.Cancel("simple", "testjob2");
            //client.Jobs.Delete("simple", "testjob1");

            // ******************************
            // content key policy operations
            // ******************************

            try
            {
                // ck = await MKIOClient.GetContentKeyPolicyAsync("testpolcreate");
            }

            catch
            {

            }

            // var cks = MKIOClient.ListContentKeyPolicies();

            try
            {
                await client.DeleteContentKeyPolicyAsync("testpolcreate");
            }

            catch
            {

            }

            /*
            var key = GenerateSymKeyAsBase64();

            var newpol = client.CreateContentKeyPolicy(
                "testpolcreate",
                new ContentKeyPolicy("My description", new List<ContentKeyPolicyOption>()
                {
                    new ContentKeyPolicyOption(
                        "option1",
                        new ContentKeyPolicyConfigurationWidevine("{}"),
                        new ContentKeyPolicyTokenRestriction(
                            "issuer",
                            "audience",
                            "Jwt",
                            new ContentKeyPolicySymmetricTokenKey(key)
                            )
                        )
                })
                );
            */

            // *******************
            // storage operations
            // *******************

            // Creation

            /*
            var storage = client.CreateStorageAccount(new StorageRequestSchema
            {
                Spec = new StorageSchema
                {
                    Name = config["StorageName"],
                    Location = config["StorageRegion"],
                    Description = "my description",
                    AzureStorageConfiguration = new BlobStorageAzureProperties
                    {
                        Url = config["StorageSAS"]
                    }
                }
            }
            );
            */

            // List
            var storages = client.ListStorageAccounts();

            // Get
            var storage2 = client.GetStorageAccount((Guid)storages.First().Metadata.Id);


            var creds = client.ListStorageAccountCredentials((Guid)storages.First().Metadata.Id);

            var cred = client.GetStorageAccountCredential((Guid)storages.First().Metadata.Id, (Guid)creds.First().Metadata.Id);


            // Delete
            // client.DeleteStorageAccount(storages.First().Metadata.Id);




            // ******************************
            // Streaming endpoint operations
            // ******************************

            // get streaming endpoint
            var mkse = client.GetStreamingEndpoint("xpouyatse1");

            // list streaming endpoints
            var mkses = client.ListStreamingEndpoints();

            // create streaming endpoint

            /*
            var newSe = client.CreateStreamingEndpoint("streamingendpoint2", "francecentral", new StreamingEndpointProperties
            {
                Description = "my description",
                ScaleUnits = 0,
                CdnEnabled = false,
                Sku = new StreamingEndpointsCurrentSku
                {
                    Name = "Standard",
                    Capacity = 600
                }
            });
            */

            // start, stop, delete streaming endpoint
            //client.StartStreamingEndpoint("streamingendpoint1");
            //client.StopStreamingEndpoint("streamingendpoint1");
            //client.DeleteStreamingEndpoint("streamingendpoint2");


            // ******************************
            // Streaming locator operations
            // ******************************

            var mklocators = client.ListStreamingLocators();

            //var mklocator1 = client.GetStreamingLocator("locator-25452");

            string uniqueness = Guid.NewGuid().ToString()[..13];
            string locatorName = $"locator-{uniqueness}";
            //var mklocator2 = MKIOClient.CreateStreamingLocator(locatorName, new StreamingLocator("copy-9ec48d1bf3-mig", "Predefined_ClearStreamingOnly"));
            var mklocator2 = client.CreateStreamingLocator(
                locatorName,
                new StreamingLocatorProperties
                {
                    AssetName = "copy-ef2058b692-copy",
                    StreamingPolicyName = "Predefined_ClearStreamingOnly"
                });

            var pathsl = client.ListUrlPathsStreamingLocator(mklocator2.Name);

            // client.DeleteStreamingLocator("locator-25452");

        }


        private static string GenerateSymKeyAsBase64()
        {
            byte[] TokenSigningKey = new byte[40];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(TokenSigningKey);
            return Convert.ToBase64String(TokenSigningKey);
        }
    }
}