using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AutoCoilerServices : IAutoCoiler
    {
        private readonly QualityControlAutoCoilerContext _context;
        public AutoCoilerServices(QualityControlAutoCoilerContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<GenericServiceResponse<ProductionLog>> AddAutoCoilerEntry(ProductionLog model)
        {
            try
            {
                _context.ProductionLogs.Add(model);
                await _context.SaveChangesAsync();
                return new GenericServiceResponse<ProductionLog>() { Status = true, message = "ProductionLogs has been created Successfully.", Data = model };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<ProductionLog>() { Status = false, message = ex.Message, Data = model };
            }
        }

        public async Task<GenericServiceResponse<List<ProductionLogsViewModel>>> GetAllProductionLogs()
        {
            try
            {
                var result = await (from o in _context.ProductionLogs
                                    join u in _context.Users on o.CreatedBy equals u.Id
                                    join c in _context.Colors on o.ColorId equals c.Id
                                    join m in _context.Machines on o.MachineId equals m.Id
                                    join s in _context.SizeCategories on o.SizeId equals s.Id
                                    select new ProductionLogsViewModel
                                    {
                                        Id = o.Id,
                                        SizeId = o.SizeId,
                                        ApprovedBy = o.ApprovedBy,
                                        CreatedDate = o.CreatedDate,
                                        ApprovedDate = o.ApprovedDate,
                                        Bp = o.Bp,
                                        ColorId = o.ColorId,
                                        DrumNumber = o.DrumNumber,
                                        DrumWiseScrap = o.DrumWiseScrap,
                                        GoodCoils = o.GoodCoils,
                                        LengthMentioned = o.LengthMentioned,
                                        LengthRecovered = o.LengthRecovered,
                                        MachineId = o.MachineId,
                                        Np = o.Np,
                                        Reason = o.Reason,
                                        Remarks = o.Remarks,
                                        ShortLengthCoils = o.ShortLengthCoils,
                                        ShortLengthMeters = o.ShortLengthMeters,
                                        CreatedBy = u.FirstName + " " + u.LastName,
                                        CreatedById = o.CreatedBy,
                                        Size = s.Size,
                                        Color = c.ColorName + " | " + c.ColorNameInUrdu,
                                        MachineName = m.Name + " | " + m.NameInUrdu
                                    }).ToListAsync();
                return new GenericServiceResponse<List<ProductionLogsViewModel>>() { Status = true, message = "All Product Logs", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<List<ProductionLogsViewModel>>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<SizeCategory>> GetSizeCategoryDetailsById(int SizeCategoryId)
        {
            try
            {
                var result = await _context.SizeCategories.Where(x => x.Id == SizeCategoryId).FirstOrDefaultAsync();
                return new GenericServiceResponse<SizeCategory>() { Status = true, message = "GetSizeCategoryDetailsById success", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<SizeCategory>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<List<DropdownModel>>> SizeCategoryDropdownCall(bool isLoading = true)
        {
            try
            {
                var result = await _context.SizeCategories.Where(x => x.IsActive == true).Select(x => new DropdownModel { Id = x.Id, Value = x.Size }).ToListAsync();
                return new GenericServiceResponse<List<DropdownModel>>() { Status = true, message = "All Termianls", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<List<DropdownModel>>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<SizeCategory>> UpdateSizeCategory(SizeCategory model)
        {
            try
            {
                _context.SizeCategories.Update(model);
                await _context.SaveChangesAsync();
                return new GenericServiceResponse<SizeCategory>() { Status = true, message = "SizeCategorys has been updated Successfully.", Data = model };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<SizeCategory>() { Status = false, message = ex.Message, Data = model };
            }
        }

        public async Task<GenericServiceResponse<SizeCategory>> UpdateSizeCategoryStatus(SizeCategory model)
        {
            try
            {
                SizeCategory SizeCategory = await _context.SizeCategories.FindAsync(model.Id);
                if (SizeCategory != null)
                {
                    SizeCategory.IsActive = model.IsActive;
                    await _context.SaveChangesAsync();
                    return new GenericServiceResponse<SizeCategory>() { Status = true, message = "SizeCategory has been updated Successfully.", Data = model };
                }
                else
                {
                    return new GenericServiceResponse<SizeCategory>() { Status = false, message = "No SizeCategory on given id found", Data = model };
                }
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<SizeCategory>() { Status = false, message = ex.Message, Data = model };
            }
        }
    }
}