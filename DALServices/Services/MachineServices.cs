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
    public class MachineServices : IMachines
    {
        private readonly QualityControlAutoCoilerContext _context;
        public MachineServices(QualityControlAutoCoilerContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<GenericServiceResponse<Machine>> AddMachine(Machine model)
        {
            try
            {
                _context.Machines.Add(model);
                await _context.SaveChangesAsync();
                return new GenericServiceResponse<Machine>() { Status = true, message = "Machine has been created Successfully.", Data = model };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<Machine>() { Status = false, message = ex.Message, Data = model };
            }
        }

        public async Task<GenericServiceResponse<List<MachineViewModel>>> GetAllMachineDetails()
        {
            try
            {
                var result = await (from o in _context.Machines
                                    join u in _context.Users on o.CreatedBy equals u.Id
                                    select new MachineViewModel
                                    {
                                        Id = o.Id,
                                        Name = o.Name,
                                        IsActive = o.IsActive,
                                        NameInUrdu = o.NameInUrdu,
                                        CreatedDate = o.CreatedDate,
                                        CreatedBy = u.FirstName + " " + u.LastName,
                                        CreatedById = o.CreatedBy
                                    }).ToListAsync();
                return new GenericServiceResponse<List<MachineViewModel>>() { Status = true, message = "All Machines", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<List<MachineViewModel>>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<Machine>> GetMachineDetailsById(int MachineId)
        {
            try
            {
                var result = await _context.Machines.Where(x => x.Id == MachineId).FirstOrDefaultAsync();
                return new GenericServiceResponse<Machine>() { Status = true, message = "GetMachineDetailsById success", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<Machine>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<List<DropdownModel>>> MachineDropdownCall(bool isLoading = true)
        {
            try
            {
                var result = await _context.Machines.Where(x => x.IsActive == true).Select(x => new DropdownModel { Id = x.Id, Value = x.Name + " | " + x.NameInUrdu }).ToListAsync();
                return new GenericServiceResponse<List<DropdownModel>>() { Status = true, message = "All Machines", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<List<DropdownModel>>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<Machine>> UpdateMachine(Machine model)
        {
            try
            {
                _context.Machines.Update(model);
                await _context.SaveChangesAsync();
                return new GenericServiceResponse<Machine>() { Status = true, message = "Machines has been updated Successfully.", Data = model };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<Machine>() { Status = false, message = ex.Message, Data = model };
            }
        }

        public async Task<GenericServiceResponse<Machine>> UpdateMachineStatus(Machine model)
        {
            try
            {
                Machine Machine = await _context.Machines.FindAsync(model.Id);
                if (Machine != null)
                {
                    Machine.IsActive = model.IsActive;
                    await _context.SaveChangesAsync();
                    return new GenericServiceResponse<Machine>() { Status = true, message = "Machine has been updated Successfully.", Data = model };
                }
                else
                {
                    return new GenericServiceResponse<Machine>() { Status = false, message = "No Machine on given id found", Data = model };
                }
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<Machine>() { Status = false, message = ex.Message, Data = model };
            }
        }
    }
}