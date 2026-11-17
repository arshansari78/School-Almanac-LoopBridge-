using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Services
{
    public interface IDiary
    {
        public Task<RDiaryListVm> GetBySchool(int SchoolId);
        public Task<RDiaryListVm> GetByRoom(int RoomId);
        public Task<RDiaryListVm> GetByStudent(int StudentId);
        public Task<ApiResultVm> Add(DiaryVm model);
        public Task<ApiResultVm> Delete(int Id);
    }
}
