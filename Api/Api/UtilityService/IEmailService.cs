using Api.Models;
namespace AngularAuthAPI.UtilityService;

public interface IEmailService
{
    void SendEmail(EmailModel email);
}
