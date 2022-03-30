using System;
namespace UserManagement.Shared.Models
{
    public class User : CommonData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
    }
}