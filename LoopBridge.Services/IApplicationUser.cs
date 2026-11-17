

using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Params;
using LoopBridge.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Services
{
    public interface IApplicationUser
    {
        public Task<RApplicationUserListVm> GetAll();
        public Task<ApiResultVm> Add(ApplicationUserVm model);
        public Task<RLoginVm> AuthenticateAdmin(PloginVm model);
        public Task<RLoginVm> AuthenticateSchool(PloginVm model);
        public Task<RLoginVm> AuthenticateTeacher(PloginVm model);
        public Task<RLoginVm> AuthenticateParent(PloginVm model);
        public Task<ApiResultVm> ChangePasswordAdmin(PChangePassword model);
        public Task<ApiResultVm> ChangePasswordSchool(PChangePassword model);
        public Task<ApiResultVm> ChangePasswordTeacher(PChangePassword model);
    }
}
