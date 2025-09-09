using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectX.Helper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectX.Controllers
{
    public class PermissionController : BaseController
    {
        private readonly IUserAccessService _userAccessService;
        public PermissionController(IUserAccessService userAccessService)
        {
            _userAccessService = userAccessService;
        }
        [CheckSessionAndUserPermission]
        public async Task<IActionResult> CreateRoles()
        {
            PermissionTemplateViewModel Template = new PermissionTemplateViewModel();
            var result = await _userAccessService.GetAllFunctionalitiesAsync();
            Template.permissionTemplates = result;
            return View(Template);
        }
        [CheckSessionAndUserPermission]
        public async Task<IActionResult> ManageRoles(int TempId)
        {
            if (TempId != 0)
            {
                var result = await _userAccessService.GetPermissionTemplateById(TempId);
                var response = await _userAccessService.GetAllPermissionTemplate();
                var Templates = new SelectList(response.Data, "Id", "TemplateName", TempId);
                ViewBag.Templates = Templates;
                if (result.Status)
                {
                    return View(result.Data);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                var response = await _userAccessService.GetAllPermissionTemplate();
                var Templates = new SelectList(response.Data, "Id", "TemplateName");
                ViewBag.Templates = Templates;
                return View();
            }
        }
        [CheckSessionExpiry]
        public async Task<JsonResult> SavePermissionTemplate(PermissionTemplateViewModel permissionTemplate)
        {
            permissionTemplate.CreatedBy = this.GetUserId;
            permissionTemplate.IsActive = true;
            var serviceResponse = await _userAccessService.SavePermissionTemplate(permissionTemplate);
            return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
        }
        [CheckSessionAndUserPermission]
        public async Task<JsonResult> UpdatePermissionTemplate(PermissionTemplateViewModel permissionTemplate)
        {
                permissionTemplate.CreatedBy = this.GetUserId;
                var serviceResponse = await _userAccessService.UpdatePermissionTemplate(permissionTemplate);
                int RoleId = Convert.ToInt32(HttpContext.Session.GetString("RoleId"));
                List<UserPermissionsModel> permission = await _userAccessService.GetUserPermissionsByRoleIdAsync(RoleId);
                var serializedpermissions = JsonSerializer.Serialize(permission);
                HttpContext.Session.SetString("UserPermissions", serializedpermissions);
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
            
        }
    }
}
