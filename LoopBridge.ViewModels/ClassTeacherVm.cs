using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.ViewModels
{
    public class ClassTeacherVm
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public TeacherVm? Teacher { get; set; }
        public int RoomId { get; set; }
        public RoomVm? Room { get; set; }
    }
}

