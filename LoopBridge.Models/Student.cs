using LoopBridge.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int RoomId {  get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room? Room {  get; set; }

        [Column(TypeName = "NVARCHAR(30)")]
        public string Name { get; set; }
        [Column(TypeName = "NVARCHAR(30)")]
        public string ParentName { get; set; }
        [Column(TypeName ="NVARCHAR(10)")]
        public string PhoneNo {  get; set; } 
        public string Email {  get; set; } 
        public string? ProfilePhoto {  get; set; }
        public EStatus Status { get; set; } // Active DeActive
    }
}
