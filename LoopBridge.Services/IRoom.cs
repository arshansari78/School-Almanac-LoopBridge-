using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Services
{
    public interface IRoom
    {
        public Task<RRoomVm> GetById(int Id);
        public Task<RRoomListVm> GetBySchool(int SchoolId);
        public Task<ApiResultVm> Add(RoomVm model);
        public Task<ApiResultVm> Update(RoomVm model);
        public Task<ApiResultVm> Delete(int Id);



    }
}
