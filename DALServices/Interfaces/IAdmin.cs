using Entities.ViewModels;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAdmin
    {
        Task<GenericServiceResponse<AppUser>> CreateUser(AppUser user);
        Task<GenericServiceResponse<AppUser>> UpdateUser(AppUser user);
        Task<GenericServiceResponse<AppUser>> GetUserDetailById(long userId);
        Task<GenericServiceResponse<List<AppUser>>> GetAllUsers();
        Task<GenericServiceResponse<bool>> UpdateUserStatus(long userId);
        Task<GenericServiceResponse<bool>> ResetPassword(long userId);



    }
}
