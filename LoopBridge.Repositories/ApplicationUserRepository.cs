using LoopBridge.Models;
using LoopBridge.Services;
using LoopBridge.ViewModels;
using LoopBridge.ViewModels.Common;
using LoopBridge.ViewModels.Params;
using LoopBridge.ViewModels.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using LoopBridge.Common;



namespace LoopBridge.Repositories
{
    
    public class ApplicationUserRepository : IApplicationUser
    {
        public readonly JwtConfigVm JwtConfig;
        private readonly UserManager<ApplicationUser> userManager;
        public readonly ApplicationDbContext context;

        public ApplicationUserRepository(IOptions<JwtConfigVm> options, UserManager<ApplicationUser> _userManager, ApplicationDbContext context)
        {
            JwtConfig = options.Value;
            userManager = _userManager;
            this.context = context;
        }
        public async Task<ApiResultVm> Add(ApplicationUserVm model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var hashed = new PasswordHasher<ApplicationUser>();  
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.UserName,
                    NormalizedUserName = model.UserName,
                    FullName = model.UserName,
                    Email = model.UserName,
                    PhoneNumber = model.UserName,
                    PasswordHash = hashed.HashPassword(null, model.Password)
                };
                var check = await userManager.CreateAsync(user);
                if (check.Succeeded) result = new ApiResultVm { Succeeded = true, Message = "User Created" };
                else result = new ApiResultVm { Succeeded = false, Message = "Something Error" };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<RLoginVm> AuthenticateAdmin(PloginVm model)
        {
            {
                RLoginVm result = new RLoginVm();
                try
                {
                    var user = await userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        var check = await userManager.CheckPasswordAsync(user, model.Password);
                        if (check)
                        {
                            var key = Encoding.ASCII.GetBytes(JwtConfig.Key);
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Audience = JwtConfig.Audience,
                                Issuer = JwtConfig.Issuer,
                                Expires = DateTime.UtcNow.AddMinutes(JwtConfig.ExpiresInMinutes),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                        new Claim(JwtRegisteredClaimNames.Sub, JwtConfig.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("id", user.Id),
                        new Claim("userName", user.UserName),
                        new Claim(ClaimTypes.Role, CRole.Administrator),
                    })
                            };
                            var newToken = tokenHandler.CreateToken(tokenDescriptor);
                            var token = tokenHandler.WriteToken(newToken);

                            result.Token = token;
                            result.UserName = user.UserName;
                            result.Result = new ApiResultVm { Succeeded = true, Message = "Authentication successful" };
                        }
                        else
                        {
                            result.Result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                        }
                    }
                    else
                    {
                        result.Result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                    }
                }
                catch (Exception ex)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                }
                return result;
            }
        }
        public Task<RLoginVm> AuthenticateParent(PloginVm model)
        {
            throw new NotImplementedException();
        }
        public async Task<RLoginVm> AuthenticateSchool(PloginVm model)
        {
            {
                RLoginVm result = new RLoginVm();
                try
                {
                    var user = await context.Schools.FirstOrDefaultAsync(e => e.Email == model.UserName.ToLower());
                    if (user != null)
                    {
                        var check = model.Password == CommonLib.GetRealPass(user.PasswordHash);
                        if (check)
                        {
                            var key = Encoding.ASCII.GetBytes(JwtConfig.Key);
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Audience = JwtConfig.Audience,
                                Issuer = JwtConfig.Issuer,
                                Expires = DateTime.Now.AddMinutes(JwtConfig.ExpiresInMinutes),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                                Subject = new ClaimsIdentity(new[]
                                {
                        new Claim(JwtRegisteredClaimNames.Sub, JwtConfig.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("id", user.Id.ToString()),
                        new Claim("userName", user.Email),
                        new Claim(ClaimTypes.Role, CRole.School),
                    })
                            };
                            var newToken = tokenHandler.CreateToken(tokenDescriptor);
                            var token = tokenHandler.WriteToken(newToken);

                            result.Token = token;
                            result.UserName = user.Email;
                            result.Result = new ApiResultVm { Succeeded = true, Message = "Authentication successful" };
                        }
                        else
                        {
                            result.Result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                        }
                    }
                    else
                    {
                        result.Result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                    }
                }
                catch (Exception ex)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                }
                return result;
            }
        }
        public async Task<RLoginVm> AuthenticateTeacher(PloginVm model)
        {
            {
                RLoginVm result = new RLoginVm();
                try
                {
                    var user = await context.Teachers.FirstOrDefaultAsync(e => e.Id.ToString() == model.UserName.ToLower());
                    if (user != null)
                    {
                        var check = model.Password == CommonLib.GetRealPass(user.PasswordHash);
                        if (check)
                        {
                            var key = Encoding.ASCII.GetBytes(JwtConfig.Key);
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Audience = JwtConfig.Audience,
                                Issuer = JwtConfig.Issuer,
                                Expires = DateTime.Now.AddMinutes(JwtConfig.ExpiresInMinutes),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                                Subject = new ClaimsIdentity(new[]
                                {
                        new Claim(JwtRegisteredClaimNames.Sub, JwtConfig.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("id", user.Id.ToString()),
                        new Claim("userName", user.Email),
                        new Claim(ClaimTypes.Role, CRole.Teacher),
                    })
                            };
                            var newToken = tokenHandler.CreateToken(tokenDescriptor);
                            var token = tokenHandler.WriteToken(newToken);

                            result.Token = token;
                            result.UserName = user.Email;
                            result.Result = new ApiResultVm { Succeeded = true, Message = "Authentication successful" };
                        }
                        else
                        {
                            result.Result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                        }
                    }
                    else
                    {
                        result.Result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                    }
                }
                catch (Exception ex)
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                }
                return result;
            }
        }
        public async Task<ApiResultVm> ChangePasswordAdmin(PChangePassword model)
        { 
            ApiResultVm result = new ApiResultVm();
            try

            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var check = await userManager.ChangePasswordAsync(user, model.OldPass, model.NewPass);
                    if (check.Succeeded)
                    {
                        result = new ApiResultVm { Succeeded = true, Message = "Password Changed Successfully" };
                    }
                    else
                    {
                        result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                    }
                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                }
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<ApiResultVm> ChangePasswordSchool(PChangePassword model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var user = await context.Schools.FirstOrDefaultAsync(e => e.Email.ToLower() == model.UserName.ToLower());
                if (user != null)
                {
                    var check = model.OldPass == CommonLib.GetRealPass(user.PasswordHash);
                    if (check)
                    {
                        user.PasswordHash = CommonLib.SetHashedPass(model.NewPass);
                        context.Schools.Update(user);
                        await context.SaveChangesAsync();
                        result = new ApiResultVm { Succeeded = true, Message = "Password Changed Successfully" };
                    }
                    else
                    {
                        result = new ApiResultVm { Succeeded = false, Message = "Invalid password" };
                    }
                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                }
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<ApiResultVm> ChangePasswordTeacher(PChangePassword model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var user = await context.Teachers.FirstOrDefaultAsync(e => e.Id.ToString() == model.UserName.ToLower());
                if(user != null)
                {
                    var check = model.OldPass == CommonLib.GetRealPass(user.PasswordHash);
                    if(check)
                    {
                        user.PasswordHash = CommonLib.SetHashedPass(model.NewPass); 
                        context.Teachers.Update(user);
                        await context.SaveChangesAsync();
                        result = new ApiResultVm { Succeeded = true, Message = "Password Changed Successfully " };
                    }
                    else
                    {
                        result = new ApiResultVm { Succeeded = false, Message = "Invalid Password" };
                    }
                }
                else
                {
                    result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                }
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
        public async Task<RApplicationUserListVm> GetAll()
        {
            RApplicationUserListVm result = new RApplicationUserListVm();
            try
            {
                var users = await context.Users.ToListAsync();
                if (users != null)
                {
                    List<ApplicationUserVm> vms = new List<ApplicationUserVm>();
                    foreach (var item in users)
                    {
                        ApplicationUserVm vm = new ApplicationUserVm
                        {
                            Id = item.Id,
                            Email = item.Email,
                            FullName = item.FullName,
                            PhoneNumber = item.PhoneNumber,
                            UserName = item.UserName,
                        };
                        vms.Add(vm);
                    }
                    result.Data = vms;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Success" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "Something Error" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };

            }
            return result;
        }
    }
}