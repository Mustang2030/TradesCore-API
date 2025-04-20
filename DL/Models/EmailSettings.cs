using Microsoft.Extensions.Configuration;

namespace Data_Layer.Models
{
    public class EmailSettings(IConfiguration config)
    {
        public string SmtpServer { get; set; } = config["EmailSettings:SmtpServer"]!;
        public int SmtpPort { get; set; } = config.GetValue<int>("EmailSettings:SmtpPort");
        public string SenderName { get; set; } = config["EmailSettings:SenderName"]!;
        public string SenderEmail { get; set; } = config["EmailSettings:SenderEmail"]!;
        public string Username { get; set; } = config["EmailSettings:Username"]!;
        public string Password { get; set; } = config["EmailSettings:Password"]!;
    }
}
