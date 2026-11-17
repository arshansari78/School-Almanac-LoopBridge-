using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.Models
{
    public class Diary
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime OnDate { get; set; }
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }
        public int RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }
        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher? Teacher { get; set; }

    }
}
