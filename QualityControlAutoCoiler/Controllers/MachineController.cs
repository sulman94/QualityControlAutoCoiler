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
    public class MachineController : BaseController
    {
        private readonly IMachines _machines;
        private readonly ILoggerManager _logger;

        public MachineController(IMachines machines, ILoggerManager logger)
        {
            _machines = machines;
            _logger = logger;
        }
        [CheckSessionAndUserPermission]
        public IActionResult Index()
        {
            return View();
        }
        [CheckSessionAndUserPermission]
        public async Task<JsonResult> SaveMachineDetails(Machine model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = this.GetUserId;
                model.CreatedDate = DateTime.Now;
                GenericServiceResponse<Machine> serviceResponse = new GenericServiceResponse<Machine>();
                serviceResponse = await _machines.AddMachine(model);
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
        public async Task<JsonResult> UpdateMachineDetails(Machine model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedBy = this.GetUserId;
                model.UpdatedDate = DateTime.Now;
                GenericServiceResponse<Machine> serviceResponse = new GenericServiceResponse<Machine>();
                serviceResponse = await _machines.UpdateMachine(model);
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
        public async Task<JsonResult> ViewMachineDetails(Machine model)
        {
            if (ModelState.IsValid)
            {
                GenericServiceResponse<Machine> serviceResponse = new GenericServiceResponse<Machine>();
                serviceResponse = await _machines.GetMachineDetailsById(model.Id);
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

        public async Task<JsonResult> GetAllMachineDetails()
        {
            GenericServiceResponse<List<MachineViewModel>> serviceResponse = new GenericServiceResponse<List<MachineViewModel>>();
            serviceResponse = await _machines.GetAllMachineDetails();
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
        public async Task<JsonResult> UpdateMachineStatus(Machine model)
        {
            if (ModelState.IsValid)
            {
                GenericServiceResponse<Machine> serviceResponse = new GenericServiceResponse<Machine>();
                serviceResponse = await _machines.UpdateMachineStatus(model);
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
