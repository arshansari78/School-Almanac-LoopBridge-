using LoopBridge.Common;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoopBridge.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent repo;
        public StudentController(IStudent _repo)
        {
            repo = _repo;
        }
        [HttpGet]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher)]
        public async Task<IActionResult> GetbyId(int Id)
        {
            return Ok (await repo.GetById(Id));
        }
        [HttpGet]
        [AuthorizeRole(CRole.School)]


        public async Task<IActionResult> GetbySchool(int Id, int PageNo, int PageSize)

        {
            return Ok(await repo.GetBySchool(Id, PageNo, PageSize));
        }
        [HttpGet]
        [AuthorizeRole(CRole.School)]


        public async Task<IActionResult> GetByRoom(int Id, int PageNo, int PageSize)

        {
            return Ok(await repo.GetByRoom(Id, PageNo, PageSize));
        }
        [HttpGet]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher)]
        public async Task<IActionResult> GetByParent(string PhoneNo)
        {

            return Ok(await repo.GetByParent( PhoneNo));

            

        }
        [HttpPost]
        [AuthorizeRole(CRole.School, CRole.Administrator, CRole.ClassTeacher)]
        
        public async Task<IActionResult> Add(StudentVm model)
        {
            return Ok(await repo.Add(model));
        }
        [HttpPost]
        [AuthorizeRole(CRole.School, CRole.ClassTeacher)]
        public async Task<IActionResult> Update(StudentVm model)
        {
            return Ok(await repo.Update(model));
        }
    }
}
