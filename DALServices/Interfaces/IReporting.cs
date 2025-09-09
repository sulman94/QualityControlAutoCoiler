using Entities.ViewModels;
using Services.Models;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IReporting
    {
        Task<GenericServiceResponse<ReportingViewModel>> GenerateBiltyReportBytes(long BiltyId);
        Task<GenericServiceResponse<ReportingViewModel>> GenerateVoucherReportBytes(VoucherViewModel model);

    }
}
