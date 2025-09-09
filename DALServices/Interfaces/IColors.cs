using Entities.ViewModels;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IColors
    {
        Task<GenericServiceResponse<List<DropdownModel>>> ColorDropdownCall(bool isLoading = true);
        Task<GenericServiceResponse<Color>> AddColor(Color model);
        Task<GenericServiceResponse<Color>> UpdateColor(Color model);
        Task<GenericServiceResponse<Color>> UpdateColorStatus(Color model);
        Task<GenericServiceResponse<Color>> GetColorDetailsById(int ColorId);
        Task<GenericServiceResponse<List<ColorViewModel>>> GetAllColorDetails();
    }
}