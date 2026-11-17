using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Services
{
    public interface IClassTeacher
    {
        public Task<RClassTeacherVm> GetById(int Id);
        public Task<RClassTeacherListVm> GetBySchool(int SchoolId);
        public Task<RClassTeacherListVm> GetByTeacher(int TeacherId);
        public Task<ApiResultVm> Add(ClassTeacherVm model);
        public Task<ApiResultVm> Update(ClassTeacherVm model);
        public Task<ApiResultVm> Delete(int Id);
    }
}
