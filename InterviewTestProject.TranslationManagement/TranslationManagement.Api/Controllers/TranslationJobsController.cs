using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.ActionFilters;
using TranslationManagement.Api.DTOs;
using TranslationManagement.Application.Result;
using TranslationManagement.Application.Services;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationJobsController : ControllerBase
    {
        private readonly ITranslationJobService _translationJobService;
        private readonly ILogger<TranslatorsController> _logger;

        public TranslationJobsController(ITranslationJobService translationJobService, ILogger<TranslatorsController> logger)
        {
            _translationJobService = translationJobService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<TranslationJobModel>> Get()
        {
            var jobs = await _translationJobService.GetJobs();
            return Ok(jobs.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TranslationJobModel>> Get(int id)
        {
            var job = await _translationJobService.GetJob(id);

            if (job.ErrorCode == ResultErrorCode.NotFound)
            {
                return NotFound();
            }
            return Ok(job.Value);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<ActionResult<TranslationJobModel>> Create(CreateTranslationJobRequestDTO jobDto)
        {
            var job = new TranslationJobModel
            {
                CustomerName = jobDto.CustomerName,
                OriginalContent = jobDto.OriginalContent
            };

            var result = await _translationJobService.CreateJob(job);
            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
        }

        [HttpPost("file")]
        [ValidateModelState]
        public async Task<ActionResult<TranslationJobModel>> CreateWithFile(IFormFile file, string customer)
        {
            var result = await _translationJobService.CreateJobWithFile(file, customer);
            
            if (result.ErrorCode == ResultErrorCode.BadRequest)
            {
                return BadRequest("Invalid data.");
            }
            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}/status")]
        [ValidateModelState]
        public async Task<ActionResult<TranslationJobModel>> UpdateStatus(int id, int translatorId, string newStatus)
        {
            _logger.LogInformation("Job status update request received: " + newStatus + " for job " + id.ToString() + " by translator " + translatorId);

            var result = await _translationJobService.UpdateJobStatus(id, translatorId, newStatus);

            if (result.ErrorCode == ResultErrorCode.NotFound)
            {
                return NotFound();
            }
            else if (result.ErrorCode == ResultErrorCode.BadRequest)
            {
                return BadRequest("Invalid status change.");
            }
            return Ok(result.Value);
        }
    }
}