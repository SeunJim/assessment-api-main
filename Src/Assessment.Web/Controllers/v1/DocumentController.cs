using Assessment.Application.Helpers;
using Assessment.Application.Interfaces;
using Assessment.Domain.Dtos;
using Assessment.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TwPay.Payment.Web.Controllers;

namespace Assessment.Web.Controllers.v1
{
    public class DocumentController:ApiControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("add-document")]
        [SwaggerOperation(Summary = "Add Document")]
        [ProducesResponseType(typeof(BaseResponse<DocumentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<DocumentResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDocument([FromForm] DocumentRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var result = await _documentService.AddDocument(request);
                return Ok(result);
            }
            return BadRequest(new BaseResponse { Status = false, Message = ModelState.ModelStateError() });
        }
        [HttpPost("delete-document")]
        [SwaggerOperation(Summary = "Delete Document")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDocument([FromBody] DocumentRequestIdDto request)
        {
            if (ModelState.IsValid)
            {
                var result = await _documentService.DeleteDocument(request.documentId);
                return Ok(result);
            }
            return BadRequest(new BaseResponse { Status = false, Message = ModelState.ModelStateError() });
        }
        [HttpGet("fetch-document")]
        [SwaggerOperation(Summary = "Fetch Document")]
        [ProducesResponseType(typeof(BaseResponse<PagedList<Document>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<PagedList<Document>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FetchDocument([FromQuery] FilterRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _documentService.FetchDocuments(request);
                if (result.Data != null)
                {
                    Response.AddPaginationHeader(result.Data.CurrentPage, result.Data.PageSize, result.Data.TotalCount, result.Data.TotalPages);
                }
                return Ok(result);
            }
            return BadRequest(new BaseResponse { Status = false, Message = ModelState.ModelStateError() });
        }
        [HttpGet("fetch-trashed-document")]
        [SwaggerOperation(Summary = "Fetch Trash")]
        [ProducesResponseType(typeof(BaseResponse<PagedList<Document>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<PagedList<Document>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FetchTrash([FromQuery] FilterRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _documentService.FetchTrash(request);
                if (result.Data != null)
                {
                    Response.AddPaginationHeader(result.Data.CurrentPage, result.Data.PageSize, result.Data.TotalCount, result.Data.TotalPages);
                }
                return Ok(result);
            }
            return BadRequest(new BaseResponse { Status = false, Message = ModelState.ModelStateError() });
        }
        [HttpGet("single-document")]
        [SwaggerOperation(Summary = "Single Document")]
        [ProducesResponseType(typeof(BaseResponse<Document>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<Document>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SingleDocument([FromQuery] DocumentRequestIdDto request)
        {
            if (ModelState.IsValid)
            {
                var result = await _documentService.SingleDocument(request.documentId);
                return Ok(result);
            }
            return BadRequest(new BaseResponse { Status = false, Message = ModelState.ModelStateError() });
        }
    }
}
