using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBridge.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string PhoneNo { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
