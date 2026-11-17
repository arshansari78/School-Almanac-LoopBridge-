using LoopBridge.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.Models
{
    public class School
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string BoardType { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "NVARCHAR(10)")]
        public string PhoneNo { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Address { get; set; }
        public string RegistrationCertificate { get; set; }
        public string? SchoolLogo { get; set; }
        public string PasswordHash { get; set; }
        public EStatus Status { get; set; }//Active DeActive
        

    }
}
