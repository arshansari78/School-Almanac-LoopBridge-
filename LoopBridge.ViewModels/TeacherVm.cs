using LoopBridge.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text;

namespace LoopBridge.ViewModels
{
    public class TeacherVm
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public SchoolVm? School { get; set; }
        public int EmpId { get; set; }
        [StringLength(10, MinimumLength = 10)]
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PasswordHash { get; set; }
        public string? ProfilePhoto { get; set; }
        public string Role { get; set; }
        public EStatus Status { get; set; } // Active DeActive
    }
}
