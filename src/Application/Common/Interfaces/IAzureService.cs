using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Models;

namespace apollo.Application.Common.Interfaces
{
    public interface IAzureService
    {
        Task<AzureBlobResult> UploadFile(IFormFile file);
        string ImageResize(Image img, int maxWidth, int maxHeight);
        Task<AzureBlobResult> UploadFileFromStream(Stream file, string filename, string filetype);
    }
}
