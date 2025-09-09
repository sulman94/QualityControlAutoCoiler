using Entities.ViewModels;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMachines
    {
        Task<GenericServiceResponse<List<DropdownModel>>> MachineDropdownCall(bool isLoading = true);
        Task<GenericServiceResponse<Machine>> AddMachine(Machine model);
        Task<GenericServiceResponse<Machine>> UpdateMachine(Machine model);
        Task<GenericServiceResponse<Machine>> UpdateMachineStatus(Machine model);
        Task<GenericServiceResponse<Machine>> GetMachineDetailsById(int MachineId);
        Task<GenericServiceResponse<List<MachineViewModel>>> GetAllMachineDetails();
    }
}