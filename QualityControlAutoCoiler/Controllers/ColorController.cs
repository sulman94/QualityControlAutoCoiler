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
    public class ColorController : BaseController
    {
        private readonly IColors _Colors;
        private readonly ILoggerManager _logger;

        public ColorController(IColors Colors, ILoggerManager logger)
        {
            _Colors = Colors;
            _logger = logger;
        }
        [CheckSessionAndUserPermission]
        public IActionResult Index()
        {
            return View();
        }
        [CheckSessionAndUserPermission]
        public async Task<JsonResult> SaveColorDetails(Color model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = this.GetUserId;
                model.CreatedDate = DateTime.Now;
                GenericServiceResponse<Color> serviceResponse = new GenericServiceResponse<Color>();
                serviceResponse = await _Colors.AddColor(model);
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
        public async Task<JsonResult> UpdateColorDetails(Color model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = this.GetUserId;
                model.UpdatedDate = DateTime.Now;
                GenericServiceResponse<Color> serviceResponse = new GenericServiceResponse<Color>();
                serviceResponse = await _Colors.UpdateColor(model);
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
        public async Task<JsonResult> ViewColorDetails(Color model)
        {
            if (ModelState.IsValid)
            {
                GenericServiceResponse<Color> serviceResponse = new GenericServiceResponse<Color>();
                serviceResponse = await _Colors.GetColorDetailsById(model.Id);
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

        public async Task<JsonResult> GetAllColorDetails()
        {
            GenericServiceResponse<List<ColorViewModel>> serviceResponse = new GenericServiceResponse<List<ColorViewModel>>();
            serviceResponse = await _Colors.GetAllColorDetails();
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
        public async Task<JsonResult> UpdateColorStatus(Color model)
        {
            if (ModelState.IsValid)
            {
                GenericServiceResponse<Color> serviceResponse = new GenericServiceResponse<Color>();
                serviceResponse = await _Colors.UpdateColorStatus(model);
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
