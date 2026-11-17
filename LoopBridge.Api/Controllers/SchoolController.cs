using LoopBridge.Common;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoopBridge.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SchoolController: ControllerBase
    {
        private readonly ISchool repo;
        public SchoolController(ISchool _repo)
        {
            repo = _repo;
        }
        [HttpGet]
        [AuthorizeRole(CRole.Administrator)]
        public async Task<IActionResult> GetAll(int PageNo, int PageSize)
        {
            return Ok(await repo.GetAll(PageNo, PageSize));
        }
        [HttpGet]
        [AuthorizeRole(CRole.Administrator)]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await repo.GetById(Id));
        }
        [HttpPost]
        [AllowAnonymous]
      
        public async Task<IActionResult> Add(SchoolVm model)
        {
            return Ok(await repo.Add(model));
        }
        [HttpPost]
        [AuthorizeRole(CRole.Administrator, CRole.School)]
        public async Task<IActionResult> Update(SchoolVm model)
        {
            return Ok(await repo.Update(model));
        }
        [HttpPost]
        [AuthorizeRole(CRole.Administrator)]
        public async Task<IActionResult> Delete(int Id)
        {
            return Ok(await repo.Delete(Id));
        }
        [HttpPost]
        [AuthorizeRole(CRole.Administrator)]
        public async Task<IActionResult> ChangeStatus(int Id)
        {
            return Ok(await repo.ChangeStatus(Id));
        }

    }
}
