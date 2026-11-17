using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.ViewModels
{
    public class ParentVm
    {
        public int Id { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(10, MinimumLength = 10)]
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

    }
}
