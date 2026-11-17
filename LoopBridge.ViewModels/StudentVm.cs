using LoopBridge.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoopBridge.ViewModels
{
    public class StudentVm
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        
        public RoomVm? Room { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public string ParentName { get; set; }
        [StringLength(10, MinimumLength = 10)]
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string? ProfilePhoto { get; set; }
        public EStatus Status { get; set; } // Active DeActive

    }
}
