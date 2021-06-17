using Azure.Storage.Blobs;
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
        public async Task<string> SaveFile(IFormFile file, string name)
        {
            string container = name;
            if (file != null)
            {
                BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, container);
                blobContainerClient.CreateIfNotExists();
                blobContainerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
               await blobClient.UploadAsync(file.OpenReadStream());

                

                return blobClient.Uri.ToString();
            }

            return "File Path incorrect ziomus";
        }

        string _connectionString;

        public AzureBlobService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            _connectionString = configuration["appSettings:storageConnectionString"];
        }

       //public string BlobCreateTest(string filePath)
       //{
       //    string container = "ddd";
       //    if (System.IO.File.Exists(filePath))
       //    {
       //        BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, container);
       //        blobContainerClient.CreateIfNotExists();
       //        blobContainerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
       //
       //        BlobClient blobClient = blobContainerClient.GetBlobClient(Path.GetFileName(filePath));
       //
       //
       //        blobClient.Upload(filePath);
       //
       //        return blobClient.Uri.ToString();
       //    }
       //
       //    return "File Path incorrect ziomus";
       //}
       //
       //public string BlobCreateTest(IFormFile file)
       //{
       //    string container = "ddd";
       //    if (file!=null)
       //    {
       //        BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, container);
       //        blobContainerClient.CreateIfNotExists();
       //        blobContainerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
       //
       //        BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
       //        blobClient.Upload(file.OpenReadStream());
       //
       //       // blobClient.Upload(filePath);
       //       
       //        return blobClient.Uri.ToString();
       //    }
       //
       //    return "File Path incorrect ziomus";
       //}

        private bool CheckIfFileExist(string fileName)
        {
            return true;
        }

        public async Task<string> DeleteFile(string url, string ownerId)
        {
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            //var container = new BlobContainerClient(_connectionString, ownerId);
            

            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ownerId);
            //var blobList = blobContainerClient.GetBlobs();
            var blobToDelete = blobContainerClient.GetBlobClient(Path.GetFileName(url));

            if (blobToDelete != null)
            {
                blobContainerClient.DeleteBlob(blobToDelete.Name);
                return "Blob DELETED";
            } else
            {
                throw new NotImplementedException();
            }

            
            
        }
    }
}
