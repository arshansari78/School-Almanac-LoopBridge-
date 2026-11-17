using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.Models
{
    public class ClassTeacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher? Teacher { get; set; }
        public int RoomId { get; set; }  
        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; } 


    }
}
