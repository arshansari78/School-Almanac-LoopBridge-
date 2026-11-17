
using LoopBridge.Common;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.AspNetCore.Mvc;

namespace LoopBridge.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacher repo;
        public TeacherController(ITeacher _repo)
        {
            repo = _repo;
        }
        [AuthorizeRole(CRole.School)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int PageNo, int PageSize)
        {
            return Ok(await repo.GetAll(PageNo, PageSize));
        }
        [AuthorizeRole(CRole.School,CRole.Teacher ,CRole.ClassTeacher)]
        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await repo.GetById(Id));
        }
        [AuthorizeRole(CRole.School,CRole.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Add(TeacherVm model)
        {
            return Ok(await repo.Add(model));
        }
        [AuthorizeRole(CRole.School,CRole.Teacher)]
        [HttpPost]
        public async Task<IActionResult> Update(TeacherVm model)
        {
            return Ok(await repo.Update(model));
        }
        
    }
}
