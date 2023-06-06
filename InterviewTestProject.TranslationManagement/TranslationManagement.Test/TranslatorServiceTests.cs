using Moq;
using TranslationManagement.Application.Repositories;
using TranslationManagement.Application.Result;
using TranslationManagement.Application.Services;
using TranslationManagement.Domain.Models;

namespace TranslationManagement.Test
{
    public class TranslatorServiceTests
    {
        private Mock<ITranslatorRepository> _translatorRepositoryMock;
        private TranslatorService _translatorService;

        [SetUp]
        public void Setup()
        {
            _translatorRepositoryMock = new Mock<ITranslatorRepository>();
            _translatorService = new TranslatorService(_translatorRepositoryMock.Object);
        }

        [Test]
        public async Task GetTranslators_ReturnsTranslators()
        {
            // Arrange
            var expectedTranslators = new List<TranslatorModel>
            {
                new TranslatorModel { Id = 1, Name = "Translator", Status = Domain.Enums.TranslatorStatus.Applicant, HourlyRate = "1", CreditCardNumber = "5000123412341234"}
            };
            _translatorRepositoryMock.Setup(repo => repo.GetTranslators()).ReturnsAsync(expectedTranslators);

            // Act
            var result = await _translatorService.GetTranslators();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Value, Is.EqualTo(expectedTranslators));
        }

        [Test]
        public async Task GetTranslator_ExistingId_ReturnsTranslator()
        {
            // Arrange
            var translatorId = 1;
            var expectedTranslator = new TranslatorModel { Id = 1, Name = "Translator", Status = Domain.Enums.TranslatorStatus.Applicant, HourlyRate = "1", CreditCardNumber = "5000123412341234" };
            _translatorRepositoryMock.Setup(repo => repo.GetTranslator(translatorId)).ReturnsAsync(expectedTranslator);

            // Act
            var result = await _translatorService.GetTranslator(translatorId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Value, Is.EqualTo(expectedTranslator));
        }

        [Test]
        public async Task GetTranslator_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var translatorId = 1;
            _translatorRepositoryMock.Setup(repo => repo.GetTranslator(translatorId)).ReturnsAsync((TranslatorModel?)null);

            // Act
            var result = await _translatorService.GetTranslator(translatorId);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.ErrorCode, Is.EqualTo(ResultErrorCode.NotFound));
        }
    }
}