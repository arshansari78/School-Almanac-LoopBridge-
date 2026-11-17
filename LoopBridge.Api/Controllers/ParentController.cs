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
    public class ParentController : ControllerBase
    {
        private readonly IParent repo;
        public ParentController (IParent _repo)
        {
            repo = _repo;
        }
        [AuthorizeRole(CRole.Administrator, CRole.Parent)]
        [HttpGet]
        public async Task<IActionResult> GetStudentByParent(string PhoneNo)
        {
            return Ok(await repo.GetStudentByParent(PhoneNo));

        }
        [AuthorizeRole(CRole.Parent)]
        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            return Ok(await repo.GetById(Id));
        }
        [AuthorizeRole(CRole.Administrator)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int PageNo,int PageSize)
        {
            return Ok(await repo.GetAll(PageNo ,PageSize));
        }
        [AuthorizeRole(CRole.School,CRole.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Add(ParentVm model)
        {
            return Ok(await repo.Add(model));
        }
        [AuthorizeRole(CRole.Parent)]
        [HttpPost]
        public async Task<IActionResult> Update(ParentVm model)
        {
            return Ok(await repo.Update(model));
        }

    }
}