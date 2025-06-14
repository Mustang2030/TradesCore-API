﻿using Data_Layer.Models;
using Data_Layer.Utilities;

namespace Service_Layer.IEmailService
{
    public interface IEmailSender
    {
        Task<OperationResult<EmailSettings>> SendEmailAsync(string toEmail, string subject, string message);
    }
}
