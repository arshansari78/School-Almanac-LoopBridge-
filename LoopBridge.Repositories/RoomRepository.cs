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
    public class RoomRepository : IRoom
    {
        private readonly ApplicationDbContext context;
        public RoomRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<RRoomVm> GetById(int Id)
        {
            RRoomVm result = new RRoomVm();
            try
            {
                var res = await context.Rooms.FindAsync(Id);
                if (res != null)
                {
                    RoomVm room = new RoomVm()
                    {
                        Id = res.Id,
                        SchoolId = res.SchoolId,
                        RoomName = res.RoomName,
                        Section = res.Section,
                        ClassTeacher = res.ClassTeacher,
                    };
                    result.Data = room;
                    result.Result = new ApiResultVm { Succeeded = true , Message = "Success"};
                }
               else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Class not found" };

                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<RRoomListVm> GetBySchool(int SchoolId)
        {
            RRoomListVm result = new RRoomListVm();
            try
            {
                var res = await context.Rooms
                    .Where(x => x.SchoolId == SchoolId)
                    .ToListAsync();

                if (res != null)
                {
                    List<RoomVm> roomVms = new List<RoomVm>();
                    foreach (var item in res)
                    {
                        RoomVm vm = new RoomVm
                        {
                            Id = item.Id,
                            SchoolId = item.SchoolId,
                            RoomName = item.RoomName,
                            Section = item.Section,
                            ClassTeacher = item.ClassTeacher,
                        };
                        roomVms.Add(vm);
                    }
                    result.Data = roomVms;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };                   
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "No classes found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<ApiResultVm> Add(RoomVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                Room room = new Room
                {
                    Id = model.Id,
                    SchoolId = model.SchoolId,
                    RoomName = model.RoomName,
                    Section = model.Section,
                    ClassTeacher = model.ClassTeacher,
                };
                context.Rooms.Add(room);
                var res = await context.SaveChangesAsync();
                if (res > 0) result = new ApiResultVm { Succeeded = true, Message = "Room Added Successfully" };
                else result = new ApiResultVm { Succeeded = false, Message = "Something Wrong Room is not Added" };

            }
            catch (Exception ex)
            {
                var detail = ex.InnerException?.Message ?? ex.Message;
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }

        public async Task<ApiResultVm> Delete(int Id)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Rooms.FindAsync(Id);
                if (res == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Invalid Room to delete" };
                    return result;
                }
                context.Rooms.Remove(res);
                var res2 = await context.SaveChangesAsync();
                if (res2 > 0)
                {
                    result = new ApiResultVm { Succeeded = true, Message = "Room deleted successfully" };
                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Something Wrong Room is not deleted" };
                }
                return result;
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                return result;
            }
        }      
            
        public async Task<ApiResultVm> Update(RoomVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Rooms.FindAsync(model.Id);
                if (res == null)
                    return new ApiResultVm { Succeeded = false, Message = "Invalid Room to Update" };
                res.Id = model.Id;
                res.SchoolId = model.SchoolId;
                res.RoomName = model.RoomName;
                res.ClassTeacher = model.ClassTeacher;
                res.Section = model.Section;


                context.Rooms.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Room Updated Succsessfully" };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
    }
}
