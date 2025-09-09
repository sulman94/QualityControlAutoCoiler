using Entities.ViewModels;
using Logger;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Helper;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectX.Controllers
{
    public class SizeCategoryController : BaseController
    {
        private readonly ISizeCategory _SizeCategorys;
        private readonly ILoggerManager _logger;

        public SizeCategoryController(ISizeCategory SizeCategorys, ILoggerManager logger)
        {
            _SizeCategorys = SizeCategorys;
            _logger = logger;
        }
        [CheckSessionAndUserPermission]
        public IActionResult Index()
        {
            return View();
        }
        [CheckSessionAndUserPermission]
        public async Task<JsonResult> SaveSizeCategoryDetails(SizeCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = this.GetUserId;
                model.CreatedDate = DateTime.Now;
                GenericServiceResponse<SizeCategory> serviceResponse = new GenericServiceResponse<SizeCategory>();
                serviceResponse = await _SizeCategorys.AddSizeCategory(model);
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

        [CheckSessionAndUserPermission]
        public async Task<JsonResult> UpdateSizeCategoryDetails(SizeCategory model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = this.GetUserId;
                model.UpdatedDate = DateTime.Now;
                GenericServiceResponse<SizeCategory> serviceResponse = new GenericServiceResponse<SizeCategory>();
                serviceResponse = await _SizeCategorys.UpdateSizeCategory(model);
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

        [CheckSessionAndUserPermission]
        public async Task<JsonResult> ViewSizeCategoryDetails(SizeCategory model)
        {
            if (ModelState.IsValid)
            {
                GenericServiceResponse<SizeCategory> serviceResponse = new GenericServiceResponse<SizeCategory>();
                serviceResponse = await _SizeCategorys.GetSizeCategoryDetailsById(model.Id);
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

        public async Task<JsonResult> GetAllSizeCategoryDetails()
        {
            GenericServiceResponse<List<SizeCategoryViewModel>> serviceResponse = new GenericServiceResponse<List<SizeCategoryViewModel>>();
            serviceResponse = await _SizeCategorys.GetAllSizeCategoryDetails();
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
        public async Task<JsonResult> UpdateSizeCategoryStatus(SizeCategory model)
        {
            if (ModelState.IsValid)
            {
                GenericServiceResponse<SizeCategory> serviceResponse = new GenericServiceResponse<SizeCategory>();
                serviceResponse = await _SizeCategorys.UpdateSizeCategoryStatus(model);
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

    }
}
