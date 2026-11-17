using LoopBridge.Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Common
{

    public static class CRole{
        public const string Administrator = "Administrator";
        public const string School = "School";
        public const string Teacher = "Teacher";
        public const string Parent = "Parent";
        public const string Room = "Room";
        public const string Student = "Student";
        public const string ClassTeacher = "ClassTeacher";
        public const string Diary = "Dairy";

    }

    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeRoleAttribute(params string[] roles):base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
