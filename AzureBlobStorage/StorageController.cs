using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AzureBlobStorage
{
    public class StorageController
    {
        string _connectionString; 

        public StorageController()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json") ;

            IConfiguration configuration = builder.Build();

            _connectionString = configuration["appSettings:storageConnectionString"];
        }

        public string BlobCreateTest(string filePath)
        {
            string container = "ss";
            if (System.IO.File.Exists(filePath))
            {
                BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, container);
                blobContainerClient.CreateIfNotExists();
                blobContainerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                BlobClient blobClient = blobContainerClient.GetBlobClient(Path.GetFileName(filePath));

                blobClient.Upload(filePath);

                return blobClient.Uri.ToString();
            }

            return "DUPA";
        }
    }
}
