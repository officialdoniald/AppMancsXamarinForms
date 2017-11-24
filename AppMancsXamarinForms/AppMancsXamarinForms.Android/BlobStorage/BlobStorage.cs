﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppMancsXamarinForms.BLL.IBlobStorage;
using System.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AppMancsXamarinForms.Droid.BlobStorage.BlobStorage))]
namespace AppMancsXamarinForms.Droid.BlobStorage
{
    public class BlobStorage : AppMancsXamarinForms.BLL.IBlobStorage.IBlobStorage
    {
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=officialdoniald;AccountKey=xKdU5Ijg9oU6HBS2zi60get5qeLLrUPYvwVjdTxcoRhBCTVvssSZn1jKzXyhyEklCRL/Gx6CsttTM6uxnBqKtg==;EndpointSuffix=core.windows.net";

        CloudBlobClient cloudBlobClient;
        CloudBlobContainer invmeContainer;

        async Task<List<Uri>> IBlobStorage.GetAllBlobUrisAsync()
        {
            CloudBlobClient cloudBlobClient = CloudStorageAccount
               .Parse(connectionString)
               .CreateCloudBlobClient();

            invmeContainer = cloudBlobClient.GetContainerReference("appmancs");

            var containerToken = new BlobContinuationToken();
            var allBlobs = await invmeContainer.ListBlobsSegmentedAsync(containerToken).ConfigureAwait(false);

            var uris = allBlobs.Results.Select(b => b.Uri).ToList();

            return uris;
        }

        async Task<string> IBlobStorage.UploadFileAsync(string filepath, Stream file)
        {
            CloudBlobClient cloudBlobClient = CloudStorageAccount
               .Parse(connectionString)
               .CreateCloudBlobClient();

            invmeContainer = cloudBlobClient.GetContainerReference("appmancs");

            var uniqueBlobName = Guid.NewGuid().ToString();
            uniqueBlobName += Path.GetExtension(filepath);

            var blobRef = invmeContainer.GetBlockBlobReference(uniqueBlobName);

            await blobRef.UploadFromStreamAsync(file).ConfigureAwait(false);

            return uniqueBlobName;
        }
    }
}