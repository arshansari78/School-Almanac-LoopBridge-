using LoopBridge.Models;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Repositories
{
    public class ClassTeacherRepository : IClassTeacher
    {
        private readonly ApplicationDbContext context;
        public ClassTeacherRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        
        public async Task<ApiResultVm> Add(ClassTeacherVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                ClassTeacher classTeacher = new ClassTeacher
                {
            
                    RoomId = model.RoomId,
                    TeacherId = model.TeacherId,
                };
                context.ClassTeachers.Add(classTeacher);
                var res = await context.SaveChangesAsync();
                if (res > 0) result = new ApiResultVm { Succeeded = true, Message = "ClassTeacher Added Successfully" };
                else result = new ApiResultVm { Succeeded = false, Message = "Something Wrong ClassTeacher is not Added"};
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        
        public async Task<ApiResultVm> Delete(int Id)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.ClassTeachers.FindAsync(Id);
                if (res == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "ClassTeacher not Deleted" };
                    return result;
                }
                context.ClassTeachers.Remove(res);
                var res2 = await context.SaveChangesAsync();
                if (res2 > 0)
                {
                    result = new ApiResultVm { Succeeded = true, Message = "ClassTeacher Deleted Successfully" };
                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Somthing Wrong ClassTeacher is not Deleted" };
                }
                return result;
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }

        public async Task<RClassTeacherVm> GetById(int Id)
        {
            RClassTeacherVm result = new RClassTeacherVm();
            try
            {
                var res = await context.ClassTeachers.FindAsync(Id);
                if (res != null)
                {
                    ClassTeacherVm classTeacher = new ClassTeacherVm()
                    {
                        Id = res.Id,
                        
                        RoomId = res.RoomId,
                        TeacherId = res.TeacherId,
                    };
                    result.Data = classTeacher;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
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

        public async Task<RClassTeacherListVm> GetBySchool(int SchoolId)
        {
            RClassTeacherListVm result = new RClassTeacherListVm();
            try
            {
                var res = await context.ClassTeachers
                    .Where(x => x.Teacher.SchoolId == SchoolId)
                    .Include(x => x.Teacher)
                    .Include (x => x.Room)
                    .ToListAsync();
                if (res != null)
                {
                    List<ClassTeacherVm> classTeacherVms = new List<ClassTeacherVm>();
                    foreach (var item in res)
                    {
                        ClassTeacherVm vm = new ClassTeacherVm
                        {
                            Id = item.Id,
                            RoomId = item.RoomId,
                            TeacherId = item.TeacherId,
                        };
                        classTeacherVms.Add(vm);
                    }
                    result.Data = classTeacherVms;
                    result.Result = new ApiResultVm {  Succeeded = true, Message = "success"};
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "No Teacher Found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }

        public async Task<RClassTeacherListVm> GetByTeacher(int TeacherId)
        {
            RClassTeacherListVm result = new RClassTeacherListVm();
            try
            {
                var res = await context.ClassTeachers
                    .Where(x => x.TeacherId == TeacherId)
                    .Include(x => x.Teacher)
                    .Include(x => x.Room)
                    .ToListAsync();
                if (res != null)
                {
                    List<ClassTeacherVm> classTeacherVms = new List<ClassTeacherVm>();
                    foreach (var item in res)
                    {
                        ClassTeacherVm vm = new ClassTeacherVm
                        {
                            Id = item.Id,
                            RoomId = item.RoomId,
                            TeacherId = item.TeacherId,
                        };
                        classTeacherVms.Add(vm);
                    }
                    result.Data = classTeacherVms;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "No Teacher Found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }

        public async Task<ApiResultVm> Update(ClassTeacherVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.ClassTeachers.FindAsync(model.Id);
                if (res == null)
                    return new ApiResultVm { Succeeded = false, Message = "Invaild Item" };
                res.Id = model.Id;
                res.RoomId = model.RoomId;
                res.TeacherId = model.TeacherId;

                context.ClassTeachers.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Successfully Updated " };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
    }
}
