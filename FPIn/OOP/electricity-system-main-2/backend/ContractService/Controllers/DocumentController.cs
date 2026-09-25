using ContractService.DTOs.Common;
using ContractService.DTOs.Documents;
using ContractService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractService.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController : ControllerBase
{
    private readonly DocumentsService _documentsService;

    public DocumentsController(DocumentsService documentsService)
    {
        _documentsService = documentsService;
    }

    [HttpPost]
    public async Task<ActionResult<DocumentResponse>> CreateDocument(CreateDocumentRequest request)
    {
        try
        {
            var result = await _documentsService.CreateDocumentAsync(request);

            return Created(string.Empty, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                ErrorCode = "DOCUMENT_VALIDATION_ERROR",
                Message = ex.Message
            });
        }
    }

    [HttpGet("/api/contracts/{contractId:guid}/documents")]
    public async Task<ActionResult<List<DocumentResponse>>> GetDocumentsByContractId(Guid contractId)
    {
        var result = await _documentsService.GetDocumentsByContractIdAsync(contractId);

        return Ok(result);
    }
}