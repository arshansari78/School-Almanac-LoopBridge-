using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoopBridge.ViewModels
{
    public class DiaryVm
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }
        [StringLength(1000, MinimumLength = 5)]
        public string Detail { get; set; }
        public int StudentId { get; set; }
        public int RoomId { get; set; }
        public int TeacherId { get; set; }

    }
}
