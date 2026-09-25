using ContractService.Data;
using ContractService.DTOs.Documents;
using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Services;

public class DocumentsService
{
    private readonly ContractDbContext _context;

    public DocumentsService(ContractDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentResponse> CreateDocumentAsync(CreateDocumentRequest request)
    {
        if (request.UserId == Guid.Empty)
        {
            throw new InvalidOperationException("Не указан пользователь.");
        }

        if (string.IsNullOrWhiteSpace(request.ContractNumber))
        {
            throw new InvalidOperationException("Номер договора обязателен.");
        }

        if (string.IsNullOrWhiteSpace(request.DocumentType))
        {
            throw new InvalidOperationException("Тип документа обязателен.");
        }

        if (string.IsNullOrWhiteSpace(request.FilePath))
        {
            throw new InvalidOperationException("Путь к файлу обязателен.");
        }

        if (request.ContractId != null)
        {
            var contract = await _context.Contracts
                .FirstOrDefaultAsync(x => x.Id == request.ContractId);

            if (contract == null)
            {
                throw new InvalidOperationException("Договор не найден.");
            }
        }

        var document = new Document
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ContractId = request.ContractId,
            ContractNumber = request.ContractNumber,
            DocumentType = request.DocumentType,
            FilePath = request.FilePath,
            VerificationStatus = "Pending",
            UploadedAt = DateTime.UtcNow
        };

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        var response = new DocumentResponse
        {
            DocumentId = document.Id,
            UserId = document.UserId,
            ContractId = document.ContractId,
            ContractNumber = document.ContractNumber,
            DocumentType = document.DocumentType,
            FilePath = document.FilePath,
            VerificationStatus = document.VerificationStatus,
            UploadedAt = document.UploadedAt
        };

        return response;
    }

    public async Task<List<DocumentResponse>> GetDocumentsByContractIdAsync(Guid contractId)
    {
        var documents = await _context.Documents.Where(x => x.ContractId == contractId).ToListAsync();

        var result = new List<DocumentResponse>();

        foreach (var document in documents)
        {
            var response = new DocumentResponse
            {
                DocumentId = document.Id,
                UserId = document.UserId,
                ContractId = document.ContractId,
                ContractNumber = document.ContractNumber,
                DocumentType = document.DocumentType,
                FilePath = document.FilePath,
                VerificationStatus = document.VerificationStatus,
                UploadedAt = document.UploadedAt
            };

            result.Add(response);
        }

        return result;
    }
}