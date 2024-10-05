using ApiDevBP.Models;

namespace ApiDevBP.Services
{
    public interface IUserService
    {
        Task<bool> SaveUserAsync(UserModel user);
        Task<List<UserModel>> GetUsersAsync();
        Task<bool> UpdateUserAsync(int id, UserModel user);
        Task<bool> DeleteUserAsync(int id);
    }
}
