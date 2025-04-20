using Data_Layer.Utilities;

namespace Service_Layer.IEmail
{
    public interface IEmailService
    {
        OperationResult<string> GenerateLink(string userId, params string[] routeValues);
    }
}
