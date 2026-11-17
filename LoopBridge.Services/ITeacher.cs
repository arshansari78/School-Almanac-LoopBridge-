using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Services
{
    public interface ITeacher
    {
        public Task<RTeacherListVm> GetAll(int PageNo, int PageSize);
        public Task<RTeacherVm> GetById(int Id);
        public Task<ApiResultVm> Add(TeacherVm model);
        public Task<ApiResultVm> Update(TeacherVm model);
        public Task<ApiResultVm> Deactive(int Id);


    }
}
