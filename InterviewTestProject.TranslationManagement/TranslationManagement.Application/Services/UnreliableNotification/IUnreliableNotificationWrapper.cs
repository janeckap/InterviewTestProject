namespace TranslationManagement.Application.Services.UnreliableNotification
{
    public interface IUnreliableNotificationWrapper
    {
        Task SendNotification(string notification, int maxRetries = 10);
    }
}
