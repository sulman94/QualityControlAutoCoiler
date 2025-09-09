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
    public class SizeCategoryServices : ISizeCategory
    {
        private readonly QualityControlAutoCoilerContext _context;
        public SizeCategoryServices(QualityControlAutoCoilerContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<GenericServiceResponse<SizeCategory>> AddSizeCategory(SizeCategory model)
        {
            try
            {
                _context.SizeCategories.Add(model);
                await _context.SaveChangesAsync();
                return new GenericServiceResponse<SizeCategory>() { Status = true, message = "SizeCategory has been created Successfully.", Data = model };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<SizeCategory>() { Status = false, message = ex.Message, Data = model };
            }
        }

        public async Task<GenericServiceResponse<List<SizeCategoryViewModel>>> GetAllSizeCategoryDetails()
        {
            try
            {
                var result = await (from o in _context.SizeCategories
                                    join u in _context.Users on o.CreatedBy equals u.Id
                                    select new SizeCategoryViewModel
                                    {
                                        Id = o.Id,
                                        Size = o.Size,
                                        IsActive = o.IsActive,
                                        CreatedDate = o.CreatedDate,
                                        CreatedBy = u.FirstName + " " + u.LastName,
                                        CreatedById = o.CreatedBy
                                    }).ToListAsync();
                return new GenericServiceResponse<List<SizeCategoryViewModel>>() { Status = true, message = "All SizeCategorys", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<List<SizeCategoryViewModel>>() { Status = false, message = ex.Message, Data = null };
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