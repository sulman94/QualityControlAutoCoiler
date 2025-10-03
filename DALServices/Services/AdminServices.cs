using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AdminServices : IAdmin
    {
        private readonly IConfiguration _config;
        private readonly UserManager<QualityControlAutoCoilerUser> _userManager;
        private readonly QualityControlAutoCoilerContext _context;
        public AdminServices(QualityControlAutoCoilerContext dbcontext, UserManager<QualityControlAutoCoilerUser> userManager, IConfiguration config)
        {
            _config = config;
            _userManager = userManager;
            _context = dbcontext;
        }
        public async Task<GenericServiceResponse<List<DropdownLongModel>>> UsersDropdownCall(bool isLoading = true)
        {
            try
            {
                var result = await _context.Users.Where(x => x.Status == 1).Select(x => new DropdownLongModel { Id = x.Id, Value = x.FirstName }).ToListAsync();
                return new GenericServiceResponse<List<DropdownLongModel>>() { Status = true, message = "All Users", Data = result };
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<List<DropdownLongModel>>() { Status = false, message = ex.Message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<AppUser>> CreateUser(AppUser user)
        {
            QualityControlAutoCoilerUser xUser = new QualityControlAutoCoilerUser
            {
                ProfileImage = user.ProfileImage,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Status = 1,
                CreatedDate = DateTime.Now,
                RoleTemplateID = user.RoleTemplateID,
                CreatedBy = user.CreatedBy,
                EmailConfirmed = true,
                NormalizedUserName = user.UserName?.ToUpper(),
                PhoneNumber = user.MobileNo,
                AccountNumber = user.AccountNumber,
                AccountTitle = user.AccountTitle,
                BankName = user.BankName,
                CNIC = user.CNIC,
                DOB = user.DOB,
                Domicile = user.Domicile,
                FatherName= user.FatherName,
                FullName = user.FullName,
                JoiningDate = user.JoiningDate,
                LeavingDate = user.LeavingDate,
                LockoutEnabled = true,
                PermanentAddress = user.PermanentAddress,
                Qualification = user.Qualification,
                ReferenceName = user.ReferenceName,
                ResidentialAddress = user.ResidentialAddress,
                Remarks = user.Remarks,
                WorkExperience = user.WorkExperience
            };
            var result = await _userManager.CreateAsync(xUser, user.Password);
            if (result.Succeeded)
            {
                user.UserId = xUser.Id;
                return new GenericServiceResponse<AppUser>() { Status = true, message = "User Created Successfully", Data = user };
            }
            else
            {
                string message = "";
                foreach (var error in result.Errors)
                {
                    message += error.Description;
                    message += Environment.NewLine;
                }
                return new GenericServiceResponse<AppUser>() { Status = false, message = message, Data = null };
            }
        }

        public async Task<GenericServiceResponse<AppUser>> UpdateUser(AppUser user)
        {
            var xUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == user.UserId);
            if (xUser != null)
            {
                xUser.RoleTemplateID = user.RoleTemplateID;
                xUser.ProfileImage = user.ProfileImage;
                xUser.FirstName = user.FirstName;
                xUser.LastName = user.LastName;
                xUser.Email = user.Email;
                xUser.UserName = user.UserName;
                xUser.UpdatedBy = user.UpdatedBy;
                xUser.UpdatedDate = DateTime.Now;
                var result = await _userManager.UpdateAsync(xUser);
                if (result.Succeeded)
                {
                    return new GenericServiceResponse<AppUser>() { Status = true, message = "User Updated Successfully", Data = user };
                }
                else
                {
                    string message = "";

                    foreach (var error in result.Errors)
                    {
                        message += error.Description;
                        message += Environment.NewLine;
                    }
                    return new GenericServiceResponse<AppUser>() { Status = false, message = message, Data = null };
                }
            }
            else
            {
                return new GenericServiceResponse<AppUser>() { Status = false, message = "User does not exists", Data = null };
            }
        }

        public async Task<GenericServiceResponse<List<AppUser>>> GetAllUsers()
        {
            GenericServiceResponse<List<AppUser>> serviceResponse = new GenericServiceResponse<List<AppUser>>();
            List<AppUser> appUsersviewModel = new List<AppUser>();
            List<QualityControlAutoCoilerUser> xUsers = new List<QualityControlAutoCoilerUser>();
            xUsers = await _userManager.Users.ToListAsync();
            foreach (var user in xUsers)
            {
                appUsersviewModel.Add(new AppUser
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Status = user.Status,
                    ProfileImage = user.ProfileImage,
                    CreatedBy = user.CreatedBy,
                    CreatedDate = user.CreatedDate,
                    UpdatedBy = user.UpdatedBy,
                    UpdatedDate = user.UpdatedDate,
                });
            }
            if (appUsersviewModel.Count > 1)
                return new GenericServiceResponse<List<AppUser>>() { Status = true, message = "User list fetched succesfully", Data = appUsersviewModel };
            else
                return new GenericServiceResponse<List<AppUser>>() { Status = false, message = "Error while getting userlist", Data = appUsersviewModel };
        }
        public async Task<GenericServiceResponse<AppUser>> GetUserDetailById(long userId)
        {
            var xUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (xUser != null)
            {
                AppUser user = new AppUser
                {
                    UserId = xUser.Id,
                    Email = xUser.Email,
                    FirstName = xUser.FirstName,
                    LastName = xUser.LastName,
                    UserName = xUser.UserName,
                    ProfileImage = xUser.ProfileImage,
                    Status = xUser.Status,
                    RoleTemplateID = xUser.RoleTemplateID,
                };
                return new GenericServiceResponse<AppUser>() { Status = true, message = "User details found successfuly", Data = user };

            }
            else
            {
                return new GenericServiceResponse<AppUser>() { Status = false, message = "User does not exists", Data = null };
            }
        }

        public async Task<GenericServiceResponse<bool>> UpdateUserStatus(long userId)
        {
            try
            {
                var xUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (xUser != null)
                {
                    xUser.Status = xUser.Status == 1 ? 0 : 1;
                    _context.Users.Update(xUser);
                    await _context.SaveChangesAsync();
                    return new GenericServiceResponse<bool>() { Status = true, message = "User status updated successfully", Data = true };

                }
                else
                {
                    return new GenericServiceResponse<bool>() { Status = false, message = "User does not exists", Data = false };
                }
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<bool>() { Status = false, message = ex.Message, Data = false };
            }
        }

        public async Task<GenericServiceResponse<bool>> ResetPassword(long userId)
        {
            try
            {
                var xUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                if (xUser != null)
                {
                    await _userManager.RemovePasswordAsync(xUser);
                    await _userManager.AddPasswordAsync(xUser, _config["AppSetting:DefaultPass"].ToString());
                    return new GenericServiceResponse<bool>() { Status = true, message = "User password reset successfully", Data = true };
                }
                else
                {
                    return new GenericServiceResponse<bool>() { Status = false, message = "User does not exists", Data = false };
                }
            }
            catch (Exception ex)
            {
                return new GenericServiceResponse<bool>() { Status = false, message = ex.Message, Data = false };
            }
        }
    }
}
