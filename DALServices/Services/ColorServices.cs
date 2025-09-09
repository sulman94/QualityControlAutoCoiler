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
    public class ColorServices : IColors
    {
        private readonly QualityControlAutoCoilerContext _context;
        public ColorServices(QualityControlAutoCoilerContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<GenericServiceResponse<Color>> AddColor(Color model)
        {
            try
            {
                _context.Colors.Add(model);
                await _context.SaveChangesAsync();
                return new GenericServiceResponse<Color>() { Status = true, message = "Color has been created Successfully.", Data = model };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<Color>() { Status = false, message = ex.Message, Data = model };
            }
        }

        public async Task<GenericServiceResponse<List<ColorViewModel>>> GetAllColorDetails()
        {
            try
            {
                var result = await (from o in _context.Colors
                                    join u in _context.Users on o.CreatedBy equals u.Id
                                    select new ColorViewModel
                                    {
                                        Id = o.Id,
                                        ColorName = o.ColorName,
                                        IsActive = o.IsActive,
                                        ColorNameInUrdu = o.ColorNameInUrdu,
                                        CreatedDate = o.CreatedDate,
                                        CreatedBy = u.FirstName + " " + u.LastName,
                                        CreatedById = o.CreatedBy
                                    }).ToListAsync();
                return new GenericServiceResponse<List<ColorViewModel>>() { Status = true, message = "All Colors", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<List<ColorViewModel>>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<Color>> GetColorDetailsById(int ColorId)
        {
            try
            {
                var result = await _context.Colors.Where(x => x.Id == ColorId).FirstOrDefaultAsync();
                return new GenericServiceResponse<Color>() { Status = true, message = "GetColorDetailsById success", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<Color>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<List<DropdownModel>>> ColorDropdownCall(bool isLoading = true)
        {
            try
            {
                var result = await _context.Colors.Where(x => x.IsActive == true).Select(x => new DropdownModel { Id = x.Id, Value = x.ColorName }).ToListAsync();
                return new GenericServiceResponse<List<DropdownModel>>() { Status = true, message = "All Termianls", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<List<DropdownModel>>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<Color>> UpdateColor(Color model)
        {
            try
            {
                _context.Colors.Update(model);
                await _context.SaveChangesAsync();
                return new GenericServiceResponse<Color>() { Status = true, message = "Colors has been updated Successfully.", Data = model };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<Color>() { Status = false, message = ex.Message, Data = model };
            }
        }

        public async Task<GenericServiceResponse<Color>> UpdateColorStatus(Color model)
        {
            try
            {
                Color Color = await _context.Colors.FindAsync(model.Id);
                if (Color != null)
                {
                    Color.IsActive = model.IsActive;
                    await _context.SaveChangesAsync();
                    return new GenericServiceResponse<Color>() { Status = true, message = "Color has been updated Successfully.", Data = model };
                }
                else
                {
                    return new GenericServiceResponse<Color>() { Status = false, message = "No Color on given id found", Data = model };
                }
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<Color>() { Status = false, message = ex.Message, Data = model };
            }
        }
    }
}