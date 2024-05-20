# Repository has moved

Please go to **https://github.com/microsoft/MK.IO**

# MK.IO Client Library

A client library for [MediaKind MK.IO](https://mk.io).

[Link to MK.IO Nuget package](https://www.nuget.org/packages/MK.IO).

## Usage (C#, .NET)

### MK.IO API Token

You need the MK.IO API token `mkiotoken` to connect to the API.

To do so,

1. Open a web browser and log into https://mk.io (sign in with Microsoft SSO).
1. Once you are logged in, open a second tab on the same browser and open this link in the new tab: https://api.mk.io/auth/token/

This should provide you with your user_id and token. Note that this token is valid for 1 year.

Another way to get the token is to use [Fiddler](https://www.telerik.com/fiddler) when you connect to the MK.IO portal with your browser.
It is displayed in the header as `x-mkio-token`. For example, you should see it on the second REST call to https://api.mk.io/api/ams/mkiosubscriptionname/stats/.

For more information, please read this [article](https://support.mediakind.com/portal/en/kb/articles/how-to-use-mkio-apis-step-by-step).

### Supported operations

In the current version, operations are supported for :

- Assets
- Streaming endpoints
- Streaming locators
- Storage accounts
- Content key policies
- Transforms, including with CVQ presets
- Jobs
- Live events
- Live outputs
- Asset filters
- Account filters
- Streaming policies

### Sample code

```csharp
using MK.IO;
using MK.IO.Models;

// **********************
// MK.IO Client creation
// **********************

var client = new MKIOClient("mkiosubscriptionname", "mkiotoken");

// get user profile info
var profile = client.Account.GetUserProfile();

// Get subscription stats
var stats = client.Account.GetSubscriptionStats();

// *****************
// asset operations
// *****************

// list assets
var mkioAssets = client.Assets.List();

// list assets with pages, 10 assets per page, sorted by creation date
var mkioAssetsResult = client.Assets.ListAsPage("properties/created desc", null, null, null, 10);
while (true)
{
    // do stuff here using mkioAssetsResult.Results

    if (mkioAssetsResult.NextPageLink == null) break;
    mkioAssetsResult = client.Assets.ListAsPageNext(mkioAssetsResult.NextPageLink);
}

// get asset
var mkasset = client.Assets.Get("myassetname");

// create asset
var newAssetName = MKIOClient.GenerateUniqueName("asset");
var newasset = client.Assets.CreateOrUpdate(newAssetName, newAssetName, "storagename", "description of my asset");

// create asset and use labels to tag it
newAssetName = MKIOClient.GenerateUniqueName("asset");
newasset = client.Assets.CreateOrUpdate(
    newAssetName,
    newAssetName,
    "storagename",
    "description of asset using labels",
    AssetContainerDeletionPolicyType.Retain,
    null,
    new Dictionary<string, string>() { { "typeAsset", "source" } }
    );

// list assets using labels filtering
var sourceEncodedAssets = client.Assets.List(label: new List<string> { "typeAsset=source" });

// delete asset
client.Assets.Delete(newsasset.Name);

// get streaming locators for asset
var locatorsAsset = client.Assets.ListStreamingLocators("copy-1b510ee166");

// Get tracks and directory of an asset
var tracksAndDir = client.Assets.ListTracksAndDirListing("copy-ef2058b692");


// ******************************
// Streaming endpoint operations
// ******************************

// get streaming endpoint
var mkse = client.StreamingEndpoints.Get("streamingendpoint1");

// list streaming endpoints
var mkses = client.StreamingEndpoints.List();

// create streaming endpoint
var newSe = client.StreamingEndpoints.Create("streamingendpoint2", "francecentral", new StreamingEndpointProperties
            {
                Description = "my description",
                ScaleUnits = 0,
                CdnEnabled = false,
                Sku = new StreamingEndpointsCurrentSku
                {
                    Name = StreamingEndpointSkuType.Standard
                }
            });

// start, stop, delete streaming endpoint
client.StreamingEndpoints.Start("streamingendpoint1");
client.StreamingEndpoints.Stop("streamingendpoint1");
client.StreamingEndpoints.Delete("streamingendpoint1");
```

Additional samples are available :

- [live operations](https://github.com/xpouyat/MK.IO/blob/master/SampleLiveOperations.md) 
- [storage operations](https://github.com/xpouyat/MK.IO/blob/master/SampleStorageOperations.md)
- [transform and job operations](https://github.com/xpouyat/MK.IO/blob/master/SampleTransformAndJobOperations.md)
- [account filter and asset filter operations](https://github.com/xpouyat/MK.IO/blob/master/SampleFilterOperations.md)
- [content key policy and streaming locator operations](https://github.com/xpouyat/MK.IO/blob/master/SampleContentKeyPolicyOperations.md)


Async operations are also supported. For example :

```csharp

// *****************
// asset operations
// *****************

// Retrieve assets with pages for better performances, sorted by names, with a batch of 10 assets in each page
var mkioAssetsResult = await client.Assets.ListAsPageAsync("name desc", null, null, null, 10);
while (true)
{
    // do stuff here using mkioAssetsResult.Results

    if (mkioAssetsResult.NextPageLink == null) break;
    mkioAssetsResult = await client.Assets.ListAsPageNextAsync(mkioAssetsResult.NextPageLink);
}


// ******************************
// Streaming endpoint operations
// ******************************

// get streaming endpoint
var mkse = await client.StreamingEndpoints.GetAsync("streamingendpoint1");

// list streaming endpoints
var mkses = await client.StreamingEndpoints.ListAsync();

// create streaming endpoint
var newSe = await client.StreamingEndpoints.CreateAsync("streamingendpoint2", "francecentral", new StreamingEndpointProperties
            {
                Description = "my description",
                ScaleUnits = 0,
                CdnEnabled = false,
                Sku = new StreamingEndpointsCurrentSku
                {
                    Name = StreamingEndpointSkuType.Standard
                }
            });

// start, stop, delete streaming endpoint
await client.StreamingEndpoints.StartAsync("streamingendpoint1");
await client.StreamingEndpoints.StopAsync("streamingendpoint1");
await client.StreamingEndpoints.DeleteAsync("streamingendpoint2");

```
