using Assessment.Domain.Dtos;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Application.Interfaces
{
    public interface IFileManager
    {
        Task<BaseResponse<ImageUploadResult>> UploadFileAsync(IFormFile file);

        Task<DeletionResult> DeleteFileAsync(string publicId);
    }
}
