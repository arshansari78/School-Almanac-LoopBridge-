using LoopBridge.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoopBridge.ViewModels
{
    public class SchoolVm
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string BoardType { get; set; }
        public string Email { get; set; }
        [StringLength(10,MinimumLength = 10)]
        public string PhoneNo { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Address { get; set; }
        public string RegistrationCertificate { get; set; }
        public string? SchoolLogo { get; set; }

        public string PasswordHash { get; set; }
        public EStatus Status { get; set; } //Active Deactive



    }
}
