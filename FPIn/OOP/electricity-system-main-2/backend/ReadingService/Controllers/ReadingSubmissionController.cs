using Microsoft.AspNetCore.Mvc;
using ReadingService.DTOs.Readings;

namespace ReadingService.Controllers;

[ApiController]
[Route("api/reading-submissions")]
public class ReadingSubmissionsController : ControllerBase
{
    [HttpPost]
    public ActionResult<ReadingSubmissionResponse> CreateReadingSubmission(
        CreateReadingSubmissionRequest request)
    {
        var submissionId = Guid.NewGuid();

        var response = new ReadingSubmissionResponse
        {
            SubmissionId = submissionId,
            UserId = request.UserId,
            ContractId = request.ContractId,
            Period = request.Period,
            Status = "Submitted",
            SubmittedAt = DateTime.UtcNow,
            Readings = request.Readings.Select(reading => new MeterReadingResponse
            {
                MeterReadingId = Guid.NewGuid(),
                MeterId = reading.MeterId,
                DayValue = reading.DayValue,
                NightValue = reading.NightValue,
                PhotoUrl = reading.PhotoUrl
            }).ToList()
        };

        return CreatedAtAction(nameof(GetReadingSubmissionById), new { submissionId }, response);
    }

    [HttpGet("{submissionId:guid}")]
    public ActionResult<ReadingSubmissionResponse> GetReadingSubmissionById(Guid submissionId)
    {
        var response = new ReadingSubmissionResponse
        {
            SubmissionId = submissionId,
            UserId = Guid.NewGuid(),
            ContractId = Guid.NewGuid(),
            Period = "2026-06",
            Status = "Submitted",
            SubmittedAt = DateTime.UtcNow,
            Readings = new List<MeterReadingResponse>
            {
                new MeterReadingResponse
                {
                    MeterReadingId = Guid.NewGuid(),
                    MeterId = Guid.NewGuid(),
                    DayValue = 1350.75m,
                    NightValue = 920.10m,
                    PhotoUrl = "/files/readings/meter-photo.jpg"
                }
            }
        };

        return Ok(response);
    }

    [HttpGet("/api/contracts/{contractId:guid}/reading-submissions")]
    public ActionResult<List<ReadingSubmissionResponse>> GetReadingSubmissionsByContractId(
        Guid contractId)
    {
        var response = new List<ReadingSubmissionResponse>
        {
            new ReadingSubmissionResponse
            {
                SubmissionId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ContractId = contractId,
                Period = "2026-06",
                Status = "Submitted",
                SubmittedAt = DateTime.UtcNow,
                Readings = new List<MeterReadingResponse>
                {
                    new MeterReadingResponse
                    {
                        MeterReadingId = Guid.NewGuid(),
                        MeterId = Guid.NewGuid(),
                        DayValue = 1350.75m,
                        NightValue = 920.10m,
                        PhotoUrl = "/files/readings/meter-photo.jpg"
                    }
                }
            }
        };

        return Ok(response);
    }

    [HttpPatch("{submissionId:guid}/status")]
    public ActionResult<ReadingSubmissionResponse> UpdateReadingSubmissionStatus(
        Guid submissionId,
        UpdateReadingSubmissionStatusRequest request)
    {
        var response = new ReadingSubmissionResponse
        {
            SubmissionId = submissionId,
            UserId = Guid.NewGuid(),
            ContractId = Guid.NewGuid(),
            Period = "2026-06",
            Status = request.Status,
            SubmittedAt = DateTime.UtcNow,
            Readings = new List<MeterReadingResponse>()
        };

        return Ok(response);
    }
}