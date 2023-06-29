using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;

namespace apollo.Infrastructure.Services
{
    public class AzureService : IAzureService
    {
        private readonly IConfiguration _configuration;

        public AzureService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);

            return DateTime.Now.Ticks + "_" + fileNameWithoutExtension + ext;
        }

        public async Task<AzureBlobResult> UploadFile(IFormFile file)
        {
            var storageConnectionString = _configuration.GetConnectionString("BlobAccessKey");

            var blobContainerName = "ws-web-files";

            var blobURL = "https://kmcstorage1.blob.core.windows.net";

            BlobContainerClient container = new BlobContainerClient(storageConnectionString, blobContainerName);

            if (!container.Exists())
            {
                container.Create();
            }

            try
            {
                if (file == null)
                {
                    throw new GenericException("Could not upload file.");
                }
                else
                {
                    var fileName = GetRandomBlobName(file.FileName);
                    BlobClient blob = container.GetBlobClient(fileName);
                    var stream = file.OpenReadStream();
                    await blob.UploadAsync(stream, new BlobHttpHeaders
                    {
                        ContentType = file.ContentType
                    },
                    conditions: null);

                    return new AzureBlobResult
                    {
                        Succeeded = true,
                        FilePath = $"{blobURL}/{blobContainerName}/{fileName}",
                        FileType = file.ContentType,
                        FileSize = file.Length,
                        Errors = new List<string>().ToArray()
                    }; 
                }
            }
            catch (Exception err)
            {
                throw new GenericException("Could not upload file. Error Message: " + err.InnerException.Message.ToString());
            }
        }


        public async Task<AzureBlobResult> UploadFileFromStream(Stream file, string filename, string fileType)
        {
            var storageConnectionString = _configuration.GetConnectionString("BlobAccessKey");

            var blobContainerName = "ws-web-files";

            var blobURL = "https://kmcstorage1.blob.core.windows.net";

            BlobContainerClient container = new BlobContainerClient(storageConnectionString, blobContainerName);

            if (!container.Exists())
            {
                container.Create();
            }

            try
            {
                BlobClient blob = container.GetBlobClient(filename);

                await blob.UploadAsync(file, new BlobHttpHeaders
                {
                    ContentType = fileType
                }, conditions: null);

                return new AzureBlobResult
                {
                    Succeeded = true,
                    FilePath = $"{blobURL}/{blobContainerName}/{filename}",
                    FileType = fileType,
                    FileSize = 1000,
                    Errors = new List<string>().ToArray()
                };
            }catch (Exception err)
            {
                throw err;
            }

            return null;
        }



        public string ImageResize(Image img, int maxWidth, int maxHeight)
        {
            if(img.Width > maxWidth || img.Height > maxHeight)
            {
                double widthRatio = (double)img.Width / (double)maxWidth;
                double heightRatio = (double)img.Height / (double)maxHeight;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);

                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }
    }
}
