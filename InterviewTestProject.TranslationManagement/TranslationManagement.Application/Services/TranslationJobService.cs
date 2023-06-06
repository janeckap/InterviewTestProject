using Microsoft.AspNetCore.Http;
using TranslationManagement.Application.Repositories;
using TranslationManagement.Application.Result;
using TranslationManagement.Application.Services.FileProcessing;
using TranslationManagement.Application.Services.UnreliableNotification;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Application.Services
{
    public class TranslationJobService : ITranslationJobService
    {
        private const decimal _pricePerCharacter = 0.01m;

        private readonly ITranslationJobRepository _translationJobRepository;
        private readonly IFileProcessorFactory _fileProcessorFactory;
        private readonly IUnreliableNotificationWrapper _unreliableNotificationWrapper;

        public TranslationJobService(ITranslationJobRepository translationJobRepository,
            IFileProcessorFactory fileProcessorFactory,
            IUnreliableNotificationWrapper unreliableNotificationWrapper)
        {
            _translationJobRepository = translationJobRepository;
            _fileProcessorFactory = fileProcessorFactory;
            _unreliableNotificationWrapper = unreliableNotificationWrapper;
        }

        public async Task<ApiResult<IEnumerable<TranslationJobModel>>> GetJobs()
        {
            var result = await _translationJobRepository.GetJobs();
            return ApiResult<IEnumerable<TranslationJobModel>>.SuccessResult(result);
        }

        public async Task<ApiResult<TranslationJobModel>> GetJob(int id)
        {
            var result = await _translationJobRepository.GetJob(id);
            if (result == null)
            {
                return ApiResult<TranslationJobModel>.NotFoundResult();
            }
            else
            {
                return ApiResult<TranslationJobModel>.SuccessResult(result);
            }
        }

        public async Task<ApiResult<TranslationJobModel>> CreateJob(TranslationJobModel job)
        {
            job.Status = JobStatus.New;
            job.Price = job.OriginalContent.Length * _pricePerCharacter;
            var result = await _translationJobRepository.AddJob(job);
            _unreliableNotificationWrapper.SendNotification("Job created: " + job.Id);
            return ApiResult<TranslationJobModel>.SuccessResult(result);
        }

        public async Task<ApiResult<TranslationJobModel>> CreateJobWithFile(IFormFile file, string customer)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            IFileProcessor fileProcessor;
            try
            {
                fileProcessor = _fileProcessorFactory.GetFileProcessor(fileExtension);
            }
            catch (NotSupportedException)
            {
                return ApiResult<TranslationJobModel>.BadRequestResult();
            }

            string streamContent, streamCustomer;
            using (var stream = file.OpenReadStream())
            {
                (streamContent, streamCustomer) = await fileProcessor.ReadContentAsync(stream);
            }

            var newJob = new TranslationJobModel()
            {
                OriginalContent = streamContent,
                TranslatedContent = "",
                CustomerName = string.IsNullOrEmpty(streamCustomer) ? customer : streamCustomer
            };

            return await CreateJob(newJob);
        }

        public async Task<ApiResult<TranslationJobModel>> UpdateJobStatus(int jobId, int translatorId, string newStatus)
        {
            var job = await _translationJobRepository.GetJob(jobId);

            if (job == null)
            {
                return ApiResult<TranslationJobModel>.NotFoundResult();
            }

            if (!Enum.TryParse(typeof(JobStatus), newStatus, true, out var status))
            {
                return ApiResult<TranslationJobModel>.BadRequestResult();
            }

            bool isInvalidStatusChange = (job.Status == JobStatus.New && (JobStatus) status == JobStatus.Completed) ||
                                         job.Status == JobStatus.Completed || (JobStatus) status == JobStatus.New;

            if (isInvalidStatusChange)
            {
                return ApiResult<TranslationJobModel>.BadRequestResult();
            }

            var result = await _translationJobRepository.UpdateJobStatus(jobId, (JobStatus) status);

            return ApiResult<TranslationJobModel>.SuccessResult(result);
        }
    }
}
