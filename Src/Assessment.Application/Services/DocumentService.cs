using Assessment.Application.Helpers;
using Assessment.Application.Interfaces;
using Assessment.Domain.Dtos;
using Assessment.Domain.Entities;
using Assessment.Infrastructure.Persistence;
using AutoMapper;
using Azure.Core;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IFileManager _fileManager;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public DocumentService(IFileManager fileManager, AppDbContext appDbContext, IMapper mapper)
        {
            _fileManager = fileManager;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<BaseResponse<DocumentResponseDto>> AddDocument(DocumentRequestDto document)
        {
            var result = await _fileManager.UploadFileAsync(document.File);
            if (result.Status && result.Data.Error == null)
            {
                var payload = new Domain.Entities.Document
                {
                    FileName = document.FileName,
                    FileType = document.FileType,
                    PublicId = result.Data.PublicId,
                    Location = result.Data.SecureUri.AbsoluteUri
                };
                _appDbContext.Documents.Add(payload);
                await _appDbContext.SaveChangesAsync();
                return new BaseResponse<DocumentResponseDto>(true, "Fetch Succesful", new DocumentResponseDto
                {
                    FileName = result.Data.OriginalFilename,
                    Location = result.Data.SecureUri.AbsoluteUri
                });
            }
            return new BaseResponse<DocumentResponseDto>(false, "An Error Occured when trying to Add Document");
        }
        public async Task<BaseResponse> DeleteDocument(string documentId)
        {
            var documentResult = _appDbContext.Documents.FirstOrDefault(x => x.Id == documentId);
            if (documentResult == null)
            {
                return new BaseResponse(false, "Document Id doesn't exist");
            }
            documentResult.IsDeleted = true;
            _appDbContext.Documents.Update(documentResult);
            await _appDbContext.SaveChangesAsync();
            return new BaseResponse(true, "Document Deleted");
        }
        public async Task<BaseResponse<PagedList<Domain.Entities.Document>>> FetchDocuments(FilterRequest request)
        {
            var documentResult = _appDbContext.Documents.Where(x => x.IsDeleted == false).OrderByDescending(x=>x.CreatedAt);
            if (!string.IsNullOrEmpty(request.FileName))
            {
                documentResult.Where(x=>x.FileName==request.FileName);
            }
            var pagedList = await PagedList<Domain.Entities.Document>.CreateAsync(documentResult, request.PageNumber, request.PageSize, 0, 0);
            return new BaseResponse<PagedList<Domain.Entities.Document>>(true, "Fetch Successful", pagedList);
        }

        public async Task<BaseResponse<PagedList<Domain.Entities.Document>>> FetchTrash(FilterRequest request)
        {
            var documentResult = _appDbContext.Documents.Where(x => x.IsDeleted);
            var pagedList = await PagedList<Domain.Entities.Document>.CreateAsync(documentResult, request.PageNumber, request.PageSize, 0, 0);
            return new BaseResponse<PagedList<Domain.Entities.Document>>(true, "Fetch Successful", pagedList);
        }

        public async Task<BaseResponse<Domain.Entities.Document>> SingleDocument(string documentId)
        {
            var documentResult = _appDbContext.Documents.FirstOrDefault(x => x.Id == documentId && x.IsDeleted == false);
            if (documentResult == null)
            {
                return new BaseResponse<Domain.Entities.Document>(false, "Document Id doesn't exist");
            }
            return new BaseResponse<Domain.Entities.Document>(false, "Document Deleted", documentResult);
        }
    }
}
