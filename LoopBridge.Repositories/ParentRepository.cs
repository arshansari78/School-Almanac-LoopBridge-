using LoopBridge.Common;
using LoopBridge.Models;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList.Extensions;

namespace LoopBridge.Repositories
{
    public class ParentRepository : IParent
    {
        private readonly ApplicationDbContext context;
        public int Id { get;private set; }

        public ParentRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResultVm> Add(ParentVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                Parent parent = new Parent
                {
                    Name = model.Name,
                    PhoneNo = model.PhoneNo,
                    Email = model.Email,
                    PasswordHash = CommonLib.SetHashedPass(model.PasswordHash)
                };
                context.Parents.Add(parent);
                var res = await context.SaveChangesAsync();
                if(res>0) result = new ApiResultVm { Message ="Success",Succeeded  = true};
                else result = new ApiResultVm {Succeeded = false ,Message = "Sever Error"};
            }
            catch (Exception ex)
            {
                result =new ApiResultVm { Message = ex.Message,Succeeded = false};
            }
            return result;
        }

        public  async Task<RParentListVm> GetAll(int PageNo, int PageSize)
        {
            RParentListVm result = new RParentListVm();
            try
            {
                var res = context.Parents.ToPagedList(PageNo, PageSize);
                if (res != null)
                {
                    List<ParentVm> vms = new List<ParentVm>();
                    foreach (var item in res)
                    {
                        ParentVm vm = new ParentVm()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Email = item.Email,
                            PhoneNo = item.PhoneNo,
                        };
                        vms.Add(vm);
                    }
                    result.Data = vms;
                    result.Result = new ApiResultVm { Message = "Success", Succeeded = true };
                }
                else
                {
                    result.Result = new ApiResultVm { Message = "failed", Succeeded = false };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Message =ex.Message, Succeeded = false };
            }
            return result;
        }

        public async Task<RParentVm> GetById(int Id)
        {
            RParentVm result = new RParentVm();
            try
            {
                var res = await context.Parents.FindAsync(Id);
                if (res != null)
                {
                    ParentVm parent = new ParentVm
                    {
                        Name = res.Name,
                        PhoneNo = res.PhoneNo,
                        Email = res.Email,
                    };
                    result.Data = parent;
                    result.Result = new ApiResultVm { Message = "Success", Succeeded = true };
                }
                else
                {
                    result.Result = new ApiResultVm { Message = "failed", Succeeded = false };
                }
            }

            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Message = ex.Message, Succeeded = false };
            }
            return result;
        }

        public async Task<RParentListVm> GetStudentByParent(string PhoneNo)
        {
            RParentListVm result = new RParentListVm();

            try
            {
                var data = await context.Students

                    
                    
                    .Where(x => x.PhoneNo == PhoneNo)
                    .Select(x => new ParentVm
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PhoneNo = x.PhoneNo,
                        Email = x.Email
                    }).ToListAsync();

                if ( data.Count > 0) {


                result.Data = data;

                result.Result = new ApiResultVm { Succeeded = true, Message = "Data Found Successfully" };
            }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "No Data Found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm
                {
                    Succeeded = false,
                    Message = ex.Message
                };
            }

            return result;
        } 

        public async Task<ApiResultVm> Update(ParentVm model)
        {
            ApiResultVm result = new ApiResultVm();
           try
            {
                var res = await context.Parents.FindAsync(model.Id);
                if (res == null) return new ApiResultVm { Succeeded = false, Message ="invalid item", };
                res.Name = model.Name;
                res.PhoneNo = model.PhoneNo;
                res.Email = model.Email;

                context.Parents.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Updated Successfully! Updated Count:" };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Message=ex.Message, Succeeded = false };
            }
            return result;
        }
    }
}
