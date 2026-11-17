using LoopBridge.Common;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LoopBridge.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoom repo;
        public RoomController(IRoom _repo)
        {
            repo = _repo;
        }
        [HttpGet]
        [AuthorizeRole(CRole.School, CRole.Administrator)]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await repo.GetById(Id));
        }
        [HttpGet]
        [AuthorizeRole(CRole.School, CRole.Administrator)]
        public async Task<IActionResult> GetBySchool(int SchoolId)
        {
            return Ok(await repo.GetBySchool(SchoolId));
        }
        [HttpPost]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher,CRole.Administrator)]
        public async Task<IActionResult> Add(RoomVm model)
        {
            return Ok(await repo.Add(model));
        }
        [HttpPost]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher, CRole.Administrator)]
        public async Task<IActionResult> Update(RoomVm model)
        {
            return Ok(await repo.Update(model));
        }
        
        [HttpPost]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher, CRole.Administrator)]
        public async Task<IActionResult> Delete(int Id)
        {
            return Ok(await repo.Delete(Id));
        }
    }
}
