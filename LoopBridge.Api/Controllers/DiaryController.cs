using LoopBridge.Common;
using LoopBridge.Models;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.AspNetCore.Mvc;

namespace LoopBridge.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly IDiary repo;
        public DiaryController(IDiary _repo)
        {
            repo = _repo;
        }       
        [HttpGet]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher, CRole.Teacher)]
        public async Task<IActionResult> GetBySchool(int SchoolId)
        {
            return Ok(await repo.GetBySchool(SchoolId));
        }
        [HttpGet]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher, CRole.Teacher)]
        public async Task<IActionResult> GetByRoom(int RoomId)
        {
            return Ok(await repo.GetByRoom(RoomId));
        }
        [HttpGet]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher, CRole.Teacher)]
        public async Task<IActionResult> GetByStudent(int StudentId)
        {
            return Ok(await repo.GetByStudent(StudentId));
        }
        [HttpPost]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher, CRole.Teacher)]
        public async Task<IActionResult> Add(DiaryVm model)
        {
            return Ok(await repo.Add(model));
        }
        [HttpDelete]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher, CRole.Teacher)]
        public async Task<IActionResult> Delete(int Id)
        {
            return Ok(await repo.Delete(Id));
        }
    }
}
