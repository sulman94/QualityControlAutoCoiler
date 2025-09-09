using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entities.ViewModels.MenuItems;

namespace Services.Services
{
    public class UserAccessService : IUserAccessService
    {
        private readonly IConfiguration _config;
        private readonly QualityControlAutoCoilerContext context;
        public UserAccessService(QualityControlAutoCoilerContext dbcontext, IConfiguration config)
        {
            context = dbcontext;
            _config = config;
        }

        #region Role-based Permisssion
        public async Task<List<PermissionTemplateDetails>> GetAllFunctionalitiesAsync()
        {
            List<PermissionTemplateDetails> response = new List<PermissionTemplateDetails>();

            response = await (from fun in context.ApplicationFunctionalities
                              join module in context.ApplicationModules on fun.ApplicationModuleId equals module.Id
                              where module.IsActive == true && fun.IsActive == true
                              select new PermissionTemplateDetails
                              {
                                  ModuleId = module.Id,
                                  ControllerName = module.ControllerName,
                                  ModuleDisplayName = module.DisplayName,
                                  FunctionalityId = fun.Id,
                                  FunctionalityName = fun.FunctionalityName,
                                  IsAllow = false
                              }).ToListAsync();
            return response;
        }
        public async Task<GenericServiceResponse<bool>> SavePermissionTemplate(PermissionTemplateViewModel model)
        {
            GenericServiceResponse<bool> serviceResponse = new GenericServiceResponse<bool>();
            var dbContextTransaction = context.Database.BeginTransaction();
            try
            {
                List<PermissionTemplateDetail> templateDetails = new List<PermissionTemplateDetail>();
                PermissionTemplate permissionTemplate = new PermissionTemplate
                {
                    Id = model.Id,
                    TemplateName = model.TemplateName,
                    IsActive = model.IsActive,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now
                };
                context.PermissionTemplates.Add(permissionTemplate);
                await context.SaveChangesAsync();
                model.Id = permissionTemplate.Id;
                foreach (var item in model.permissionTemplates)
                {
                    PermissionTemplateDetail templateDetail = new PermissionTemplateDetail
                    {
                        TemplateId = model.Id,
                        FunctionalityId = item.FunctionalityId,
                        IsAllow = item.IsAllow
                    };
                    templateDetails.Add(templateDetail);
                }
                context.PermissionTemplateDetails.AddRange(templateDetails);
                await context.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                serviceResponse = new GenericServiceResponse<bool>() { Status = true, message = "SavePermissionTemplate Success", Data = true };
            }
            catch (Exception ex)
            {
                serviceResponse = new GenericServiceResponse<bool>() { Status = false, message = ex.Message, Data = false };
            }
            return serviceResponse;
        }
        public async Task<GenericServiceResponse<PermissionTemplateViewModel>> GetPermissionTemplateById(int TemplateId)
        {
            PermissionTemplateViewModel viewModel = new PermissionTemplateViewModel();
            try
            {

                var templateDetail = await context.PermissionTemplates.Where(x => x.Id == TemplateId).FirstOrDefaultAsync();
                if (templateDetail != null)
                {
                    var activePermissions = await context.PermissionTemplateDetails.Where(x => x.TemplateId == TemplateId).Select(z => z.FunctionalityId).ToListAsync();
                    var response = await (from fun in context.ApplicationFunctionalities
                                          join form in context.ApplicationModules on fun.ApplicationModuleId equals form.Id
                                          where form.IsActive == true && fun.IsActive == true
                                          select new PermissionTemplateDetails
                                          {
                                              ModuleId = form.Id,
                                              ControllerName = form.ControllerName,
                                              ModuleDisplayName = form.DisplayName,
                                              FunctionalityId = fun.Id,
                                              FunctionalityName = fun.FunctionalityName,
                                              IsAllow = false
                                          }).ToListAsync();

                    foreach (var item in response.Where(t => activePermissions.Contains(t.FunctionalityId)))
                    {
                        item.IsAllow = true;
                    }
                    viewModel.Id = templateDetail.Id;
                    viewModel.TemplateName = templateDetail.TemplateName;
                    viewModel.IsActive = templateDetail.IsActive;
                    viewModel.permissionTemplates = response;
                    return new GenericServiceResponse<PermissionTemplateViewModel>() { Status = true, message = "GetPermissionTemplateById success", Data = viewModel };
                }
                else
                {
                    return new GenericServiceResponse<PermissionTemplateViewModel>() { Status = false, message = "No Template Found", Data = null };
                }
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<PermissionTemplateViewModel>() { Status = false, message = ex.Message, Data = null };
            }
        }
        public async Task<GenericServiceResponse<List<PermissionTemplate>>> GetAllPermissionTemplate()
        {
            var result = await context.PermissionTemplates.Select(x => new PermissionTemplate { Id = x.Id, TemplateName = x.TemplateName }).ToListAsync();
            return new GenericServiceResponse<List<PermissionTemplate>>() { Status = true, message = "Get All Templates", Data = result };
        }
        public async Task<GenericServiceResponse<bool>> UpdatePermissionTemplate(PermissionTemplateViewModel model)
        {
            GenericServiceResponse<bool> serviceResponse = new GenericServiceResponse<bool>();
            var dbContextTransaction = context.Database.BeginTransaction();
            try
            {
                List<PermissionTemplateDetail> templateDetails = new List<PermissionTemplateDetail>();
                var updatePermissionTemplate = await context.PermissionTemplates.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (updatePermissionTemplate != null)
                {
                    updatePermissionTemplate.TemplateName = model.TemplateName;
                    updatePermissionTemplate.IsActive = model.IsActive;
                    updatePermissionTemplate.UpdatedBy = model.UpdatedBy;
                    updatePermissionTemplate.UpdatedDate = DateTime.Now;
                    await context.SaveChangesAsync();

                    context.PermissionTemplateDetails.RemoveRange(context.PermissionTemplateDetails.Where(x => x.TemplateId == model.Id));
                    await context.SaveChangesAsync();
                    foreach (var item in model.permissionTemplates)
                    {
                        PermissionTemplateDetail templateDetail = new PermissionTemplateDetail
                        {
                            TemplateId = model.Id,
                            FunctionalityId = item.FunctionalityId,
                            IsAllow = item.IsAllow
                        };
                        templateDetails.Add(templateDetail);
                    }
                    context.PermissionTemplateDetails.AddRange(templateDetails);
                    await context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                    serviceResponse = new GenericServiceResponse<bool>() { Status = true, message = "Success", Data = true };
                }
                else
                {
                    serviceResponse = new GenericServiceResponse<bool>() { Status = false, message = "Failed to update template", Data = false };
                }

            }
            catch (Exception ex)
            {
                serviceResponse = new GenericServiceResponse<bool>() { Status = false, message = ex.Message, Data = false };
            }
            return serviceResponse;
        }
        public async Task<List<UserPermissionsModel>> GetUserPermissionsByRoleIdAsync(int RoleId)
        {
            List<UserPermissionsModel> permissions = new List<UserPermissionsModel>();
            permissions = await (from ua in context.PermissionTemplateDetails
                                 join af in context.ApplicationFunctionalities on ua.FunctionalityId equals af.Id into ps
                                 from af in ps.DefaultIfEmpty()
                                 where ua.TemplateId == RoleId && af.ApplicationModule.IsActive
                                 select new UserPermissionsModel
                                 {
                                     ModuleId = af.ApplicationModuleId,
                                     FunctionalityName = af.FunctionalityName,
                                     AllowAccess = ua.IsAllow,
                                     ActionMethodName = af.ActionMethodName,
                                     ControllerName = af.ApplicationModule.ControllerName,
                                     ModuleName = af.ApplicationModule.DisplayName
                                 }).ToListAsync();
            return permissions;
        }
        public async Task<List<Items>> GetMenu(int RoleId)
        {
            List<Items> items = new List<Items>();
            items = await (from AppMod in context.ApplicationModules
                           join Appfunc in context.ApplicationFunctionalities on AppMod.Id equals Appfunc.ApplicationModuleId
                           join PTD in context.PermissionTemplateDetails on Appfunc.Id equals PTD.FunctionalityId
                           where Appfunc.IsMenuItem == true && PTD.TemplateId == RoleId && AppMod.IsActive && Appfunc.IsActive
                           orderby AppMod.DisplayOrder ascending
                           group new { AppMod, Appfunc } by new { AppMod.MenuHeader, AppMod.IconCode } into g
                           select new Items
                           {
                               ItemName = g.Key.MenuHeader,
                               Icon = g.Key.IconCode,
                               SubMenuItems = g.Select(x => new SubMenuItems
                               {
                                   submenuItem = x.Appfunc.FunctionalityName,
                                   NavigationLink = "/" + x.AppMod.ControllerName + "/" + x.Appfunc.ActionMethodName
                               }).ToList(),
                           }).ToListAsync();

            return items;
        }



        #endregion

    }
}
