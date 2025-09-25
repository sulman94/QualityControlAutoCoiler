using Entities.ViewModels;
using Services.Models;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAutoCoiler
    {
        //Task<GenericServiceResponse<List<DropdownModel>>> SizeCategoryDropdownCall(bool isLoading = true);
        Task<GenericServiceResponse<ProductionLog>> AddAutoCoilerEntry(ProductionLog model);
        //Task<GenericServiceResponse<SizeCategory>> UpdateSizeCategory(SizeCategory model);
        //Task<GenericServiceResponse<SizeCategory>> UpdateSizeCategoryStatus(SizeCategory model);
        //Task<GenericServiceResponse<SizeCategory>> GetSizeCategoryDetailsById(int SizeCategoryId);
        //Task<GenericServiceResponse<List<SizeCategoryViewModel>>> GetAllSizeCategoryDetails();
    }
}