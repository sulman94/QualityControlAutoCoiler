using Entities.ViewModels;
using Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectX.Helper;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectX.Controllers
{
    public class AutoCoilerController : BaseController
    {
        private readonly IAutoCoiler _autoCoiler;
        private readonly IMachines _machines;
        private readonly ISizeCategory _sizeCategory;
        private readonly IAdmin _admin;
        private readonly IColors _colors;
        private readonly ILoggerManager _logger;

        public AutoCoilerController(IAutoCoiler autoCoiler, ILoggerManager logger, IMachines machines, ISizeCategory sizeCategory, IColors colors, IAdmin admin)
        {
            _autoCoiler = autoCoiler;
            _logger = logger;
            _admin = admin;
            _machines = machines;
            _colors = colors;
            _sizeCategory = sizeCategory;
        }
        [CheckSessionAndUserPermission]
        public async Task<IActionResult> Index()
        {
            await SetBiltyDropdowns();
            return View();
        }
        [CheckSessionAndUserPermission]
        public async Task<JsonResult> SaveProductionLogs(ProductionLog model)
        {
            if (ModelState.IsValid)
            {
            model.CreatedBy = this.GetUserId;
                model.CreatedDate = DateTime.Now;
                GenericServiceResponse<ProductionLog> serviceResponse = new GenericServiceResponse<ProductionLog>();
                serviceResponse = await _autoCoiler.AddAutoCoilerEntry(model);
                if (serviceResponse.Status)
                {
                    return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
                }
                else
                {
                    return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
                }
                return null;
            }
            else
            {
                return new JsonResult(new { success = false, message = "Model Invalid", Data = "Model Invalid" });
            }
        }

        //[CheckSessionAndUserPermission]
        //public async Task<JsonResult> UpdateSizeCategoryDetails(SizeCategory model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.UpdatedBy = this.GetUserId;
        //        model.UpdatedDate = DateTime.Now;
        //        GenericServiceResponse<SizeCategory> serviceResponse = new GenericServiceResponse<SizeCategory>();
        //        serviceResponse = await _autoCoiler.UpdateSizeCategory(model);
        //        if (serviceResponse.Status)
        //        {
        //            return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });

        //        }
        //        else
        //        {
        //            return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
        //        }
        //    }
        //    else
        //    {
        //        return new JsonResult(new { success = false, message = "Model Invalid", Data = "Model Invalid" });
        //    }
        //}

        //[CheckSessionAndUserPermission]
        //public async Task<JsonResult> ViewSizeCategoryDetails(SizeCategory model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        GenericServiceResponse<SizeCategory> serviceResponse = new GenericServiceResponse<SizeCategory>();
        //        serviceResponse = await _autoCoiler.GetSizeCategoryDetailsById(model.Id);
        //        if (serviceResponse.Status)
        //        {
        //            return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });

        //        }
        //        else
        //        {
        //            return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
        //        }
        //    }
        //    else
        //    {
        //        return new JsonResult(new { success = false, message = "Model Invalid", Data = "Model Invalid" });
        //    }
        //}
        
        //[CheckSessionExpiry]
        //public async Task<JsonResult> GetAllSizeCategoryDetails()
        //{
        //    GenericServiceResponse<List<SizeCategoryViewModel>> serviceResponse = new GenericServiceResponse<List<SizeCategoryViewModel>>();
        //    serviceResponse = await _autoCoiler.GetAllSizeCategoryDetails();
        //    if (serviceResponse.Status)
        //    {
        //        return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });

        //    }
        //    else
        //    {
        //        return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
        //    }
        //}
        //[CheckSessionAndUserPermission]
        //public async Task<JsonResult> UpdateSizeCategoryStatus(SizeCategory model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        GenericServiceResponse<SizeCategory> serviceResponse = new GenericServiceResponse<SizeCategory>();
        //        serviceResponse = await _autoCoiler.UpdateSizeCategoryStatus(model);
        //        if (serviceResponse.Status)
        //        {
        //            return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
        //        }
        //        else
        //        {
        //            return new JsonResult(new { success = serviceResponse.Status, serviceResponse.message, serviceResponse.Data });
        //        }
        //    }
        //    else
        //    {
        //        return new JsonResult(new { success = false, message = "Model Invalid", Data = "Model Invalid" });
        //    }
        //}
        private async Task SetBiltyDropdowns()
        {
            var machines = await _machines.MachineDropdownCall();
            var machineslist = new SelectList(machines.Data, "Id", "Value");

            var sizes = await _sizeCategory.SizeCategoryDropdownCall();
            var sizeslist = new SelectList(sizes.Data, "Id", "Value");

            var colors = await _colors.ColorDropdownCall();
            var colorslist = new SelectList(colors.Data, "Id", "Value");
            
            var operators = await _admin.UsersDropdownCall();
            var operatorslist = new SelectList(operators.Data, "Id", "Value");


            ViewBag.allMachines = machineslist;
            ViewBag.allColors = colorslist;
            ViewBag.allSizes = sizeslist;
            ViewBag.allOperators = operatorslist;
        }
    }
}
