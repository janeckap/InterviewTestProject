using Moq;
using TranslationManagement.Application.Repositories;
using TranslationManagement.Application.Services.FileProcessing;
using TranslationManagement.Application.Services.UnreliableNotification;
using TranslationManagement.Application.Services;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Domain.Models;
using TranslationManagement.Application.Result;

namespace TranslationManagement.Test
{
    public class TranslationJobServiceTest
    {
        private Mock<ITranslationJobRepository> _mockTranslationJobRepository;
        private Mock<IFileProcessorFactory> _mockFileProcessorFactory;
        private Mock<IUnreliableNotificationWrapper> _mockUnreliableNotificationWrapper;
        private TranslationJobService _translationJobService;

        [SetUp]
        public void Setup()
        {
            _mockTranslationJobRepository = new Mock<ITranslationJobRepository>();
            _mockFileProcessorFactory = new Mock<IFileProcessorFactory>();
            _mockUnreliableNotificationWrapper = new Mock<IUnreliableNotificationWrapper>();

            _translationJobService = new TranslationJobService(
                _mockTranslationJobRepository.Object,
                _mockFileProcessorFactory.Object,
                _mockUnreliableNotificationWrapper.Object);
        }

        [Test]
        public async Task GetJob_WithNonExistingId_ReturnsNotFoundApiResult()
        {
            // Arrange
            int jobId = 1;
            _mockTranslationJobRepository.Setup(repo => repo.GetJob(jobId)).ReturnsAsync((TranslationJobModel?)null);

            // Act
            var result = await _translationJobService.GetJob(jobId);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.ErrorCode, Is.EqualTo(ResultErrorCode.NotFound));
        }

        [Test]
        public async Task UpdateJobStatus_WithNonExistingJob_ReturnsNotFoundApiResult()
        {
            // Arrange
            int jobId = 1;
            int translatorId = 2;
            string newStatus = "Completed";
            _mockTranslationJobRepository.Setup(repo => repo.GetJob(jobId)).ReturnsAsync((TranslationJobModel?)null);

            // Act
            var result = await _translationJobService.UpdateJobStatus(jobId, translatorId, newStatus);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.ErrorCode, Is.EqualTo(ResultErrorCode.NotFound));
        }

        [Test]
        public async Task UpdateJobStatus_WithInvalidStatus_ReturnsBadRequestApiResult()
        {
            // Arrange
            int jobId = 1;
            int translatorId = 2;
            string newStatus = "InvalidStatus";
            var job = new TranslationJobModel { Status = JobStatus.New };
            _mockTranslationJobRepository.Setup(repo => repo.GetJob(jobId)).ReturnsAsync(job);

            // Act
            var result = await _translationJobService.UpdateJobStatus(jobId, translatorId, newStatus);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.ErrorCode, Is.EqualTo(ResultErrorCode.BadRequest));
        }

    }
}