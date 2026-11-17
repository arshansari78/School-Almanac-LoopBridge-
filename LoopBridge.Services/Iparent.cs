using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Services
{
    public interface IParent
    {
        public Task<RParentListVm> GetStudentByParent(string PhoneNo);

        public Task<RParentVm> GetById(int Id);

        public Task<RParentListVm> GetAll(int PageNo, int PageSize);

        public Task<ApiResultVm> Add(ParentVm model);

        public Task<ApiResultVm> Update(ParentVm model);
       
    }
}
