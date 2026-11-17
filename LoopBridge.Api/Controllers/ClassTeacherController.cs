using LoopBridge.Common;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.AspNetCore.Mvc;

namespace LoopBridge.Api.Controllers

{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassTeacherController : ControllerBase
    {
        private readonly IClassTeacher Repo;
        public ClassTeacherController(IClassTeacher _Repo)
        {
            Repo = _Repo;
        }
        [HttpGet]
        [AuthorizeRole(CRole.School,CRole.Administrator)]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await Repo.GetById(Id));
        }
        [HttpGet]
        [AuthorizeRole(CRole.Administrator)]
        public async Task<IActionResult> GetBySchool(int schoolId)
        {
            return Ok(await Repo.GetBySchool(schoolId));
        }
        [HttpGet]
        [AuthorizeRole( CRole.School, CRole.Administrator)]
        public async Task<IActionResult> GetByTeacher(int TeacherId)
        {
            return Ok(await Repo.GetByTeacher(TeacherId));
        }
        [HttpPost]
        [AuthorizeRole(CRole.School,CRole.Administrator)]

        public async Task<IActionResult> Add(ClassTeacherVm model)
        {
            return Ok(await Repo.Add( model));
        }
        [HttpPost]
        [AuthorizeRole(CRole.School, CRole.Administrator)]
        public async Task<IActionResult> Update(ClassTeacherVm model)
        {
            return Ok(await Repo.Update(model));
        }
        [HttpPost]
        [AuthorizeRole(CRole.School, CRole.Administrator)]
        public async Task<IActionResult> Delete(int Id)
        {
            return Ok(await Repo.Delete(Id));
        }




    }
}
