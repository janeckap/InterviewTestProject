using External.ThirdParty.Services;
using Microsoft.Extensions.Logging;

namespace TranslationManagement.Application.Services.UnreliableNotification
{
    public class UnreliableNotificationWrapper : IUnreliableNotificationWrapper
    {
        private readonly UnreliableNotificationService _unreliableNotificationService;
        private readonly ILogger<UnreliableNotificationWrapper> _logger;

        public UnreliableNotificationWrapper(UnreliableNotificationService unreliableNotificationService, ILogger<UnreliableNotificationWrapper> logger)
        {
            _unreliableNotificationService = unreliableNotificationService;
            _logger = logger;
        }

        public async Task SendNotification(string notification, int maxRetries = 10)
        {
            for (int retryCount = 0; retryCount < maxRetries; retryCount++)
            {
                try
                {
                    if (await _unreliableNotificationService.SendNotification(notification))
                    {
                        _logger.LogInformation("New job notification sent");
                        return;
                    }
                }
                catch (Exception)
                {
                    _logger.LogError("Error while trying to create a job notification.");
                }
            }

            _logger.LogError("Error while trying to create a job notification. Maximum retries reached. Aborting.");
        }
    }
}
