using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School? School { get; set; }
        public string RoomName { get; set; }
        public string Section { get; set; }
        public string ClassTeacher { get; set; }
    }
}
