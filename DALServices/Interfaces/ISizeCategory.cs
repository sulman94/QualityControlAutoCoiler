using Entities.ViewModels;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISizeCategory
    {
        Task<GenericServiceResponse<List<DropdownModel>>> SizeCategoryDropdownCall(bool isLoading = true);
        Task<GenericServiceResponse<SizeCategory>> AddSizeCategory(SizeCategory model);
        Task<GenericServiceResponse<SizeCategory>> UpdateSizeCategory(SizeCategory model);
        Task<GenericServiceResponse<SizeCategory>> UpdateSizeCategoryStatus(SizeCategory model);
        Task<GenericServiceResponse<SizeCategory>> GetSizeCategoryDetailsById(int SizeCategoryId);
        Task<GenericServiceResponse<List<SizeCategoryViewModel>>> GetAllSizeCategoryDetails();
    }
}