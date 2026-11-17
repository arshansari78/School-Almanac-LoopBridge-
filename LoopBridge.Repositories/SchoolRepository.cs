using LoopBridge.Common;
using LoopBridge.Enums;
using LoopBridge.Models;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Result;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using X.PagedList.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace LoopBridge.Repositories
{
    public class SchoolRepository : ISchool
    {
        private readonly ApplicationDbContext context;

        public SchoolRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ApiResultVm> Add(SchoolVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                School school = new School
                {

                    SchoolLogo = model.SchoolLogo,
                    SchoolName = model.SchoolName,
                    BoardType = model.BoardType,
                    Email = model.Email,
                    PhoneNo = model.PhoneNo,
                    Address = model.Address,
                    RegistrationCertificate = model.RegistrationCertificate,
                    PasswordHash = CommonLib.SetHashedPass(model.PasswordHash),
                    Status = Enums.EStatus.Deactive


                };
                context.Schools.Add(school);
                var res = await context.SaveChangesAsync();
                if (res > 0) result = new ApiResultVm { Succeeded = true, Message = "Success" };
                else result = new ApiResultVm { Succeeded = false, Message = "Server Error" };

            }
            catch (Exception ex)

            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }


            return result;
        }

        public async Task<ApiResultVm> Delete(int Id)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Schools.FindAsync(Id);
                if (res == null)
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Invalid item to delete" };
                    return result;
                }
                context.Schools.Remove(res);
                var res2 = await context.SaveChangesAsync();
                if (res2 > 0)
                {
                    result = new ApiResultVm { Succeeded = true, Message = "Deleted Successfully" };
                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "Server Error" };
                }
                return result;
            }
            catch (Exception ex)
            {
                {
                    result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                    return result;
                }

            }
        }

        public async Task<RSchoolListVm> GetAll(int PageNo, int PageSize)
        {
            RSchoolListVm result = new RSchoolListVm();
            try
            {
                var res = context.Schools.ToPagedList(PageNo, PageSize);
                if (res != null)
                {
                    List<SchoolVm> Vms = new List<SchoolVm>();

                    foreach (var item in res)
                    {
                        SchoolVm vm = new SchoolVm
                        {
                            Id = item.Id,
                            SchoolLogo = item.SchoolLogo,
                            SchoolName = item.SchoolName,
                            BoardType = item.BoardType,
                            Email = item.Email,
                            PhoneNo = item.PhoneNo,
                            Address = item.Address,
                            RegistrationCertificate = item.RegistrationCertificate,


                        };
                        Vms.Add(vm);
                    }

                    result.Data = Vms;
                    result.Result = new ApiResultVm { Message = "success", Succeeded = true };
                }
                else
                {
                    result.Result = new ApiResultVm { Message = "Success", Succeeded = false };
                }

            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Message = ex.Message, Succeeded = false };
            }
            return result;
        }

        public async Task<RSchoolVm> GetById(int Id)
        {
            RSchoolVm result = new RSchoolVm();
            try
            {
                var res = await context.Schools.FindAsync(Id);
                if (res != null)
                {
                    SchoolVm school = new SchoolVm()
                    {
                        Id = res.Id,
                        SchoolLogo = res.SchoolLogo,
                        SchoolName = res.SchoolName,
                        BoardType = res.BoardType,
                        Email = res.Email,
                        PhoneNo = res.PhoneNo,
                        Address = res.Address,
                        RegistrationCertificate = res.RegistrationCertificate,


                    };
                    result.Data = school;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Server Error" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }

        public async Task<ApiResultVm> Update(SchoolVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Schools.FindAsync(model.Id);
                if (res == null)
                    return new ApiResultVm { Succeeded = false, Message = "Invalid Item" };

                res.Id = model.Id;

                res.SchoolLogo = model.SchoolLogo;

                res.SchoolName = model.SchoolName;

                res.BoardType = model.BoardType;

                res.Email = model.Email;

                res.PhoneNo = model.PhoneNo;

                res.Address = model.Address;

                res.RegistrationCertificate = model.RegistrationCertificate;




 
                context.Schools.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Updated Successfully! Updated Count:" + res2 };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }


        public async Task<ApiResultVm> ChangeStatus(int Id)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var res = await context.Schools.FindAsync(Id);
                if (res == null)
                {
                    return new ApiResultVm { Succeeded = false, Message = "Invalid School" };
                }
                if (res.Status == Enums.EStatus.Active) { res.Status = Enums.EStatus.Deactive; }
                else { res.Status = Enums.EStatus.Active; }
                context.Schools.Update(res);
                var res2 = await context.SaveChangesAsync();
                result = new ApiResultVm { Succeeded = true, Message = "Status Changed Successfully! Updated Count: " + res2 };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }

            return result;
        }
    }
}


