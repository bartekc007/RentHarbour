namespace RentHarbor.AuthService.Services
{
    public interface IAuthorizationService
    {
        Task<string> GetUserIdFromTokenAsync(string jwtToken);
        Task<string> GetUserNameById(string id);
    }
}
