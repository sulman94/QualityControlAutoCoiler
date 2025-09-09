using Entities.ViewModels;
using Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectX.Helper;
using Services.Interfaces;
using Services.Models;
using Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectX.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdmin _admin;
        private readonly ILoggerManager _logger;
        private readonly IUserAccessService _userAccessService;
        public AdminController(IAdmin admin, ILoggerManager logger, IUserAccessService userAccessService)
        {
            _logger = logger;
            _admin = admin;
            _userAccessService = userAccessService;

        }
        [CheckSessionAndUserPermission]

        public async Task<IActionResult> CreateUser()
        {
            _logger.LogInformation("CreateUser Method Starts");
            var response = await _userAccessService.GetAllPermissionTemplate();
            var Templates = new SelectList(response.Data, "Id", "TemplateName");
            ViewBag.Templates = Templates;
            return View();
        }
        [CheckSessionAndUserPermission]

        public async Task<IActionResult> EditUser(long Id)
        {
            GenericServiceResponse<AppUser> serviceResponse = new GenericServiceResponse<AppUser>();
            if (Id != 0)
            {

                serviceResponse = await _admin.GetUserDetailById(Id);
                if (serviceResponse.Status)
                {
                    var response = await _userAccessService.GetAllPermissionTemplate();
                    var Templates = new SelectList(response.Data, "Id", "TemplateName", serviceResponse.Data.RoleTemplateID);
                    ViewBag.Templates = Templates;
                    return View(serviceResponse.Data);
                }
                else
                {
                    return BadRequest(serviceResponse.message);
                }
            }
            return BadRequest("user ID must be valid for Edit.");
        }
        [CheckSessionAndUserPermission]

        public async Task<IActionResult> ViewUser(long Id)
        {
            GenericServiceResponse<AppUser> serviceResponse = new GenericServiceResponse<AppUser>();
            if (Id != 0)
            {
                serviceResponse = await _admin.GetUserDetailById(Id);
                if (serviceResponse.Status)
                {
                    var response = await _userAccessService.GetAllPermissionTemplate();
                    var Templates = new SelectList(response.Data, "Id", "TemplateName", serviceResponse.Data.RoleTemplateID);
                    ViewBag.Templates = Templates;
                    return View(serviceResponse.Data);
                }
                else
                {
                    return BadRequest(serviceResponse.message);
                }
            }
            return BadRequest("user ID must be valid for Edit.");
        }
        [CheckSessionExpiry]

        public async Task<JsonResult> SaveUser(AppUser user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedBy = this.GetUserId;
                GenericServiceResponse<AppUser> serviceResponse = new GenericServiceResponse<AppUser>();
                serviceResponse = await _admin.CreateUser(user);
                if (serviceResponse.Status)
                {
                    return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });

                }
                else
                {
                    return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
                }
            }
            else
            {
                return new JsonResult(new { success = false, message = "Model Invalid", Data = "Model Invalid" });
            }
        }
        [CheckSessionExpiry]

        public async Task<JsonResult> UpdateUser(AppUser user)
        {
            user.UpdatedBy = this.GetUserId;
            GenericServiceResponse<AppUser> serviceResponse = new GenericServiceResponse<AppUser>();
            serviceResponse = await _admin.UpdateUser(user);
            if (serviceResponse.Status)
            {
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });

            }
            else
            {
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
            }
        }
        [CheckSessionAndUserPermission]

        public IActionResult AllUsers()
        {
            return View();
        }
        [CheckSessionExpiry]
        public async Task<JsonResult> GetAllUsers()
        {
            GenericServiceResponse<List<AppUser>> serviceResponse = new GenericServiceResponse<List<AppUser>>();
            serviceResponse = await _admin.GetAllUsers();
            if (serviceResponse.Status)
            {
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
            }
            else
            {
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
            }
        }
        [CheckSessionAndUserPermission]

        public async Task<JsonResult> UpdateUserStatus(long userId)
        {
            var serviceResponse = await _admin.UpdateUserStatus(userId);
            if (serviceResponse.Status)
            {
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });

            }
            else
            {
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
            }
        }
        [CheckSessionAndUserPermission]

        public async Task<JsonResult> ResetPassword(long userId)
        {
            var serviceResponse = await _admin.ResetPassword(userId);
            if (serviceResponse.Status)
            {
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });

            }
            else
            {
                return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
            }
        }


    }
}
