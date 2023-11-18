using AhorroDigital.Common.Response;
using System.Net.Mail;

namespace AhorroDigital.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);

    }
}
