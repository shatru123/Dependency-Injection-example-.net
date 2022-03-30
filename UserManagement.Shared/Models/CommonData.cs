using System;

namespace UserManagement.Shared.Models
{
    public class CommonData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }    
}