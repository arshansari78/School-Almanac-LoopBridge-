using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Services
{
    public interface IStudent
    {
        public Task<RStudentVm> GetById(int Id);
        public Task<RStudentListVm> GetBySchool(int Id, int PageNo, int PageSize);
        public Task<RStudentListVm> GetByRoom(int Id, int PageNo, int PageSize);
        public Task<RStudentListVm> GetByParent(string PhoneNo);
        public Task<ApiResultVm> Add(StudentVm model);
        public Task<ApiResultVm> Update(StudentVm model);
        public Task<ApiResultVm> Deactive(int Id);
    }
}
