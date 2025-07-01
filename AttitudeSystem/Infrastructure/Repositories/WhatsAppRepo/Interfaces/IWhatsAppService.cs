namespace AttitudeSystem.Infrastructure.Repositories.WhatsAppRepo.Interfaces
{
    public interface IWhatsAppService
    {
        Task SendMessage(string phoneNumber, string message);
    }
}
