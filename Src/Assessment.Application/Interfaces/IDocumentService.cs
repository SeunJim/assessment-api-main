using Assessment.Application.Helpers;
using Assessment.Domain.Dtos;
using Assessment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Application.Interfaces
{
    public interface IDocumentService
    {
        Task<BaseResponse<DocumentResponseDto>> AddDocument(DocumentRequestDto document);
        Task<BaseResponse> DeleteDocument(string documentId);
        Task<BaseResponse<PagedList<Domain.Entities.Document>>> FetchDocuments(FilterRequest request);
        Task<BaseResponse<PagedList<Domain.Entities.Document>>> FetchTrash(FilterRequest request);
        Task<BaseResponse<Document>> SingleDocument(string documentId);
    }
}
