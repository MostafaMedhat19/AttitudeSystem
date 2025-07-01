

namespace AttitudeSystem.Infrastructure.Repositories.WhatsAppRepo.Implementation
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly IConfiguration _configuration;

        public WhatsAppService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessage(string phoneNumber, string message)
        {
            Console.WriteLine($"WhatsApp to {phoneNumber}: {message}");

            var accountSid = _configuration["Twilio:AccountSid"];
            var authToken = _configuration["Twilio:AuthToken"];
            var fromNumber = _configuration["Twilio:WhatsAppNumber"];

          
            TwilioClient.Init(accountSid, authToken);

         
            var msg = await MessageResource.CreateAsync(
                from: new PhoneNumber("whatsapp:" + fromNumber),
                to: new PhoneNumber("whatsapp:" + phoneNumber),
                body: message
            );

            Console.WriteLine($"Message sent. SID: {msg.Sid}");
        }
    }
}
