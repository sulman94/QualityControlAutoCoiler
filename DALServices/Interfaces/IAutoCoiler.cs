using Entities.ViewModels;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAutoCoiler
    {
        Task<GenericServiceResponse<ProductionLog>> AddAutoCoilerEntry(ProductionLog model);
        Task<GenericServiceResponse<List<ProductionLogsViewModel>>> GetAllProductionLogs();
    }
}