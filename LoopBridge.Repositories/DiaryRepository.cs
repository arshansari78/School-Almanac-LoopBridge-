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
    public class DiaryRepository : IDiary
    {
        private readonly ApplicationDbContext context;
        public DiaryRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResultVm> Add(DiaryVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                Diary diary = new Diary
                {
                    RoomId = model.RoomId,
                    StudentId = model.StudentId,
                    TeacherId = model.TeacherId,
                    Detail = model.Detail,
                    Title = model.Title
                };
                context.Diarys.Add(diary);
                var res = await context.SaveChangesAsync();
                if (res > 0) result = new ApiResultVm { Succeeded = true, Message = "Added Successfully" };
                else result = new ApiResultVm { Succeeded = false, Message = "Something Wrong Not Added" };
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<ApiResultVm> Delete(int Id)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Diarys.FindAsync(Id);
                if (res == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = " Diary not found " };
                    return result;
                }
                context.Diarys.Remove(res);
                var res2 = await context.SaveChangesAsync();
                if (res2 > 0)
                {
                    result = new ApiResultVm { Succeeded = true, Message = "Diary deleted Successfully" };
                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Failed to delete diary" };
                }
                return result;

            }
            catch (Exception ex)
            {
                {
                    result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                    return result;

                }
            }
        }

        public async Task<RDiaryListVm> GetByRoom(int Id)
        {
            RDiaryListVm result = new RDiaryListVm();
            try
            {

                var res = await context.Diarys
                    .Where(x => x.RoomId == Id)
                    .Include(x => x.Room)
                    .Include(x => x.Teacher)
                    .Include(x => x.Student)
                    .OrderByDescending(e => e.Id)
                    .ToListAsync();
                if (res != null)

                {
                    List<DiaryVm> diaryVms = new List<DiaryVm>();
                    foreach (var item in res)
                    {
                        DiaryVm vm = new DiaryVm
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Detail = item.Detail,
                            RoomId = item.RoomId,
                            TeacherId = item.TeacherId,
                            StudentId = item.StudentId,
                        };
                        diaryVms.Add(vm);
                    }
                    result.Data = diaryVms;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Room not found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;

        }

        public async Task<RDiaryListVm> GetBySchool(int SchoolId)
        {
            RDiaryListVm result = new RDiaryListVm();
            {
                try
                {
                    var res = await context.Diarys
                        .Where(x => x.Teacher.SchoolId == SchoolId)
                        .Include(x => x.Teacher)
                        .Include(x => x.Student)
                        .Include(x => x.Room)
                        .OrderByDescending(e => e.Id)
                        .ToListAsync();
                    if (res != null)
                    {
                        List<DiaryVm> diaryVms = new List<DiaryVm>();
                        foreach (var item in res)
                        {
                            DiaryVm vm = new DiaryVm
                            {
                                Id = item.Id,
                                Title = item.Title,
                                Detail = item.Detail,
                                RoomId = item.RoomId,
                                TeacherId = item.TeacherId,
                                StudentId = item.StudentId,
                            };
                            diaryVms.Add(vm);
                        }
                        result.Data = diaryVms;
                        result.Result = new ApiResultVm { Succeeded = true, Message = "success" };
                    }
                    else
                    {
                        result.Result = new ApiResultVm { Succeeded = false, Message = "Diary Not Found" };
                    }
                }
                catch (Exception ex)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                }
            }
            ;
            return result;
        }

        public async Task<RDiaryListVm> GetByStudent(int StudentId)
        {


            RDiaryListVm result = new RDiaryListVm();
            try
            {
                var res = await context.Diarys
                    .Where(e => e.StudentId == StudentId)
                    .Include(e => e.Student)
                    .Include(e => e.Room)
                    .Include(e => e.Teacher)
                    .OrderByDescending(e => e.Id)
                    .ToListAsync();
                if (res != null)
                {
                    List<DiaryVm> diaryVms = new List<DiaryVm>();
                    foreach (var item in res)
                    {
                        DiaryVm vm = new DiaryVm
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Detail = item.Detail,
                            RoomId = item.RoomId,
                            TeacherId = item.TeacherId,
                            StudentId = item.StudentId,

                        };
                        diaryVms.Add(vm);
                    }
                    result.Data = diaryVms;
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

    }
}
