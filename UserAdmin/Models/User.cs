using System;
using System.Collections.Generic;
using System.Text;

namespace UserAdmin.Models
{
    class User
    {
        public int? id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public DateTime registeredAt { get; set; }
    }
}
