using Yms.Contracts.CommonServices;

namespace Yms.Services.Common.Abstractions
{
    public interface IUserService
    {
        UserInfoDto Authenticate(string username, string password);
    }
}
