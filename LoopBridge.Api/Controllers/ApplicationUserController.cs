using LoopBridge.Common;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Params;
using LoopBridge.ViewModels.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoopBridge.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUser repo;
        public ApplicationUserController(IApplicationUser _repo)
        {
            repo = _repo;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAdmin(PloginVm model)       
        {
            return Ok(await repo.AuthenticateAdmin(model));
        }
        [HttpGet]
        [AuthorizeRole(CRole.Administrator)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await repo.GetAll());
        }
        
        [HttpPost]
        [AuthorizeRole(CRole.Administrator)]
        public async Task<IActionResult> Add(ApplicationUserVm model)
        {
            return Ok(await repo.Add(model));
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateSchool(PloginVm model)
        {
            return Ok(await repo.AuthenticateSchool(model));
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateTeacher(PloginVm model)
        {
            return Ok(await repo.AuthenticateTeacher(model));
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateParent(PloginVm model)
        {
            return Ok(await repo.AuthenticateParent(model));
        }
        [HttpPost]
        [AuthorizeRole(CRole.Administrator)]
        public async Task<IActionResult> ChangePasswordAdmin(PChangePassword model)
        {
            return Ok(await repo.ChangePasswordAdmin(model));
        }
        [HttpPost]
        [AuthorizeRole(CRole.School)]
        public async Task<IActionResult> ChangePasswordSchool(PChangePassword model)
        {
            return Ok(await repo.ChangePasswordSchool(model));
        } 
        [HttpPost]
        [AuthorizeRole(CRole.Teacher)]
        public async Task<IActionResult> ChangePasswordTeacher(PChangePassword model)
        {
            return Ok(await repo.ChangePasswordTeacher(model));
        }
    }
}
