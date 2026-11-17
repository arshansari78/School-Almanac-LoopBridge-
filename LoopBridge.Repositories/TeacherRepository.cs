using LoopBridge.Common;
using LoopBridge.Enums;
using LoopBridge.Models;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using X.PagedList.Extensions;

namespace LoopBridge.Repositories
{
    public class TeacherRepository : ITeacher
    {
        private readonly ApplicationDbContext context;
        public TeacherRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ApiResultVm> Add(TeacherVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                Teacher teacher = new Teacher()
                { 
                    Id = model.Id,
                    ProfilePhoto = model.ProfilePhoto,
                    Name = model.Name,
                    Gender = model.Gender,
                    PhoneNo = model.PhoneNo,
                    Email = model.Email,
                    Role = model.Role,
                    SchoolId = model.SchoolId,
                    PasswordHash = CommonLib.SetHashedPass(model.PasswordHash),
                    Status = Enums.EStatus.Deactive

                }
                ;
                context.Teachers.Add(teacher);
                var res = await context.SaveChangesAsync();
                if (res > 0) result = new ApiResultVm { Succeeded = true, Message = "Success" };
                else result = new ApiResultVm { Succeeded = false, Message = "Server Error" };

            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.InnerException?.Message ?? ex.Message };
            }
            return result;
        }
        public async Task<RTeacherListVm> GetAll(int PageNo, int PageSize)
        {
            {
                RTeacherListVm result = new RTeacherListVm();
                try
                {
                    var res = context.Teachers.ToPagedList(PageNo, PageSize);
                    if (res != null)
                    {
                        List<TeacherVm> teacherVms = new List<TeacherVm>();

                        foreach (var item in res)
                        {
                            TeacherVm vm = new TeacherVm
                            {
                                Id = item.Id,
                                ProfilePhoto = item.ProfilePhoto,
                                Name = item.Name,
                                Gender = item.Gender,
                                PhoneNo = item.PhoneNo,
                                Email = item.Email,
                                Role = item.Role,

                            };
                            teacherVms.Add(vm);
                        }
                        result.Data = teacherVms;
                        result.Result = new ApiResultVm { Message = "success", Succeeded = true };
                    }
                    else
                    {
                        result.Result = new ApiResultVm { Message = "Success", Succeeded = false };
                    }

                }
                catch (Exception ex)
                {
                    result.Result = new ApiResultVm { Message = ex.Message, Succeeded = false };
                }
                return result;
            }
        }
        public async Task<RTeacherVm> GetById(int Id)
        {
            RTeacherVm result = new RTeacherVm();
            try
            {
                var res = await context.Teachers.FindAsync(Id);
                if (res != null)
                {
                    TeacherVm teacher = new TeacherVm()
                    {
                        Id = res.Id,
                        ProfilePhoto = res.ProfilePhoto,
                        Name = res.Name,
                        Gender = res.Gender,
                        PhoneNo = res.PhoneNo,
                        Email = res.Email,
                        Role = res.Role,


                    };
                    result.Data = teacher;

                    result.Result = new ApiResultVm { Succeeded = true, Message = "success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Server Error" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<ApiResultVm> Update(TeacherVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Teachers.FindAsync(model.Id);
                if (res == null)
                    return new ApiResultVm { Succeeded = false, Message = "Invalid Item" };

                res.Id = model.Id;
                res.ProfilePhoto = model.ProfilePhoto;
                res.Name = model.Name;
                res.Gender = model.Gender;
                res.PhoneNo = model.PhoneNo;
                res.Email = model.Email;
                res.Role = model.Role;




                context.Teachers.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Updated Successfully! Updated Count:" + res2 };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = "Invalid Item" };
            }
            return result;
        }
        public async Task<ApiResultVm> Deactive(int Id)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Teachers.FirstOrDefaultAsync(e => e.Status == EStatus.Active && e.Id == Id);
                if (res == null)
                    return new ApiResultVm { Succeeded = false, Message = "Invalid Item" };
                res.Status = EStatus.Deactive;
                context.Teachers.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Updated Successfully! Updated Count:" + res2 };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = "Invalid Item" };
            }
            return result;
        }
    }
}
