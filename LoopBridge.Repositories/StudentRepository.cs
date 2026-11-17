using LoopBridge.Enums;
using LoopBridge.Models;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList.Extensions;

namespace LoopBridge.Repositories
{
    public class StudentRepository : IStudent
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<ApiResultVm> Add(StudentVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                Student student = new Student
                {
                    Name = model.Name,
                    ParentName = model.ParentName,
                    Email = model.Email,
                    PhoneNo = model.PhoneNo,
                    ProfilePhoto = model.ProfilePhoto,
                    RoomId=model.RoomId,
                    Status=model.Status
                };
                context.Students.Add(student);
                var res = await context.SaveChangesAsync();
                if (res > 0) result = new ApiResultVm { Message = "Success", Succeeded = true };
                else result = new ApiResultVm { Succeeded = false, Message = "Server Error" };
            }
            catch (Exception ex)
            {
                var detail = ex.InnerException?.Message ?? ex.Message;
                result = new ApiResultVm { Succeeded = false, Message = detail };
            }
            return result;
        }

        public async Task<RStudentVm> GetById(int Id)
        {
            RStudentVm result = new RStudentVm();
            try
            {

                var res = await context.Students.FindAsync(Id);
                if (result != null)

                {
                    StudentVm student = new StudentVm()

                    {
                        Id = res.Id,
                        RoomId = res.RoomId,
                        Status = res.Status,
                        Name = res.Name,
                        ParentName = res.ParentName,
                        Email = res.Email,
                        PhoneNo = res.PhoneNo,
                        ProfilePhoto = res.ProfilePhoto,
                    };
                    result.Data =student;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Student not found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
            
        }

        public async Task<RStudentListVm> GetByParent(string PhoneNo)
        {
            RStudentListVm result = new RStudentListVm();
            try
            {

                var res = await context.Students.Where(e=> e.PhoneNo == PhoneNo).ToListAsync();
                if (res != null)

                {
                    List<StudentVm> studentVms = new List<StudentVm>();
                    foreach (var item in res)

                    {
                        StudentVm vm = new StudentVm

                        {
                            Id = item.Id,
                            RoomId = item.RoomId,
                            Name = item.Name,
                            Email = item.Email,
                            PhoneNo = item.PhoneNo,
                            ParentName = item.ParentName,
                            ProfilePhoto = item.ProfilePhoto,
                            Status = item.Status,
                        };
                       studentVms.Add(vm);
                    };
                    result.Data = studentVms;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "parent not found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;

        }

        public async Task<RStudentListVm> GetByRoom(int Id, int PageNo, int PageSize)
        {
            RStudentListVm result = new RStudentListVm();
            try
            {

                var res = context.Students
                   .Where(x => x.Id == Id)
                   .ToPagedList(PageNo, PageSize);
                if (res != null)

                {

                    List<StudentVm> studentVms = new List<StudentVm>();
                    foreach (var item in res) 

                    {
                        StudentVm vm = new StudentVm
                        {
                            Id = item.Id,
                            Email = item.Email,
                            Name = item.Name,
                            ParentName = item.ParentName,
                            RoomId = item.RoomId,
                            PhoneNo = item.PhoneNo,
                            Status = item.Status,
                            ProfilePhoto = item.ProfilePhoto,
                        };
                        studentVms .Add(vm);

                    };
                    result.Data = studentVms;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Student not found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;

        }
        public async Task<RStudentListVm> GetBySchool(int Id, int PageNo, int PageSize)
        {
            RStudentListVm result = new RStudentListVm();
            try
            {

                var res =  context.Students
                    .Where(x => x.Id == Id)
                    .ToPagedList(PageNo, PageSize);
                    
                if (res != null)

                {

                    List<StudentVm> studentVms = new List<StudentVm>();
                    foreach (var item in res)

                    {
                        StudentVm vm = new StudentVm

                        {
                            Id = item.Id,
                            Email = item.Email,
                            Name = item.Name,
                            ParentName = item.ParentName,
                            RoomId = item.RoomId,
                            PhoneNo = item.PhoneNo,
                            Status = item.Status,
                            ProfilePhoto = item.ProfilePhoto,
                        };
                        studentVms.Add(vm);
                    }
                    ;
                    result.Data = studentVms;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "School not found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;

        }

        public async Task<ApiResultVm> Update(StudentVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Students.FindAsync(model.Id);
                if (res == null) return new ApiResultVm { Succeeded = false, Message = "Invalid Item" };
                res.Status = model.Status;
                res.RoomId = model.RoomId;
                res.ParentName = model.ParentName;
                res.PhoneNo = model.PhoneNo;
                res.Name = model.Name;
                res.ProfilePhoto = model.ProfilePhoto;
                res.Email = model.Email;

                context.Students.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Update Successfully! Updated Count:" + res2 };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }

            return result;
        }
        public async Task<ApiResultVm> Deactive(int Id)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Students.FirstOrDefaultAsync(e => e.Status == EStatus.Active && e.Id == Id);
                if (res == null)
                    return new ApiResultVm { Succeeded = false, Message = "Invalid Item" };
                res.Status = EStatus.Deactive;
                context.Students.Update(res);
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

