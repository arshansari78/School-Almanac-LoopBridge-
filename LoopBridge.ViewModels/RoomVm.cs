using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.ViewModels
{
    public class RoomVm
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public SchoolVm? School { get; set; }
        public string RoomName { get; set; }
        public string Section { get; set; }
        public string ClassTeacher { get; set; }
    }
}
