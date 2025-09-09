using Entities.ViewModels;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Entities.ViewModels.MenuItems;

namespace Services.Interfaces
{
    public interface IUserAccessService
    {
        #region Role-based Permisssion
        Task<List<PermissionTemplateDetails>> GetAllFunctionalitiesAsync();
        Task<GenericServiceResponse<bool>> SavePermissionTemplate(PermissionTemplateViewModel model);
        Task<GenericServiceResponse<PermissionTemplateViewModel>> GetPermissionTemplateById(int TemplateId);
        Task<GenericServiceResponse<List<PermissionTemplate>>> GetAllPermissionTemplate();
        Task<GenericServiceResponse<bool>> UpdatePermissionTemplate(PermissionTemplateViewModel model);
        Task<List<UserPermissionsModel>> GetUserPermissionsByRoleIdAsync(int RoleId);
        Task<List<Items>> GetMenu(int RoleId);
        #endregion

    }
}
