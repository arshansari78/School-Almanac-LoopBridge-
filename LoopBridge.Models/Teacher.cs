using LoopBridge.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School? School { get; set; }
        public int EmpId { get; set; }
        [Column (TypeName = "NVARCHAR(10)")]
        public string PhoneNo { get; set; }
        public string Email {  get; set; }
        [Column(TypeName = "NVARCHAR(30)")]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string? ProfilePhoto { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public EStatus Status { get; set; } // Active DeActive


    }
}
 