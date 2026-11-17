using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Services
{
    public interface ISchool
    {
        public Task<RSchoolListVm> GetAll(int PageNo, int PageSize);       
        public Task<RSchoolVm> GetById(int Id);
        public Task<ApiResultVm> Add(SchoolVm model);
        public Task<ApiResultVm> Update(SchoolVm model);
        public Task<ApiResultVm> Delete(int Id);
        public Task<ApiResultVm> ChangeStatus(int Id);
    }
}
