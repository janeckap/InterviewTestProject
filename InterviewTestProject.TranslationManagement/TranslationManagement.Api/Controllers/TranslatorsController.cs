using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.ActionFilters;
using TranslationManagement.Api.DTOs;
using TranslationManagement.Application.Result;
using TranslationManagement.Application.Services;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslatorsController : ControllerBase
    {
        private readonly ITranslatorService _translatorService;
        private readonly ILogger<TranslatorsController> _logger;

        public TranslatorsController(ITranslatorService translatorService, ILogger<TranslatorsController> logger)
        {
            _translatorService = translatorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TranslatorModel>>> Get()
        {
            var translators = await _translatorService.GetTranslators();
            return Ok(translators.Value);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<TranslatorModel>>> GetByName(string name)
        {
            var translators = await _translatorService.GetTranslatorsByName(name);
            return Ok(translators.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TranslatorModel>> Get(int id)
        {
            var translator = await _translatorService.GetTranslator(id);

            if (translator.ErrorCode == ResultErrorCode.NotFound)
            {
                return NotFound();
            }
            return Ok(translator.Value);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<ActionResult<TranslatorModel>> Create(CreateTranslatorRequestDTO requestDTO)
        {
            var translator = new TranslatorModel
            {
                Name = requestDTO.Name,
                HourlyRate = requestDTO.HourlyRate,
                Status = Enum.Parse<TranslatorStatus>(requestDTO.Status),
                CreditCardNumber = requestDTO.CreditCardNumber
            };

            var result = await _translatorService.AddTranslator(translator);
            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}/status")]
        [ValidateModelState]
        public async Task<ActionResult> UpdateStatus(int id, string newStatus)
        {
            _logger.LogInformation("User status update request: " + newStatus + " for user " + id.ToString());

            var translator = await _translatorService.UpdateTranslatorStatus(id, newStatus);

            if (translator.ErrorCode == ResultErrorCode.NotFound)
            {
                return NotFound();
            }
            else if (translator.ErrorCode == ResultErrorCode.BadRequest)
            {
                return BadRequest("Invalid status change.");
            }
            return Ok(translator.Value);
        }
    }
}