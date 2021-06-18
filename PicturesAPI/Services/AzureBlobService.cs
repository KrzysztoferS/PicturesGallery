using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PicturesAPI.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PicturesAPI.Services
{
    public class AzureBlobService : IFileSaver
    { 
        
        string _connectionString;
        
        public AzureBlobService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            _connectionString = configuration["appSettings:storageConnectionString"];
        }

        public async Task<string> SaveFile(IFormFile file, string name)
        {
            string container = name;
            if (file != null)
            {
                BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, container);
                blobContainerClient.CreateIfNotExists();
                blobContainerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                
                BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
                bool exists=await blobClient.ExistsAsync();
                if (!exists)
                {
                    await blobClient.UploadAsync(file.OpenReadStream());

                    return blobClient.Uri.ToString();
                }
                else return "Error";
            }

            return "Error";
        }

        public async Task<bool> DeleteFile(string url, string ownerId)
        {
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ownerId);
            
            var blobToDelete = blobContainerClient.GetBlobClient(Path.GetFileName(url));

            if (blobToDelete != null)
            {
                await blobContainerClient.DeleteBlobIfExistsAsync(blobToDelete.Name);
                return true;
            } else
            {
                return false;
            }

            
            
        }

        public async Task<bool> DeleteContainer(string ownerId)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            Pageable<BlobContainerItem> containers=blobServiceClient.GetBlobContainers();

            foreach(var container in containers)
            {
                if (container.Name == ownerId)
                {
                   Response response= blobServiceClient.DeleteBlobContainer(container.Name);
                   return true;
                }
            }

            return false;
        }
    }
}
