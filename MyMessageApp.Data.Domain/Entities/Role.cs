using System;
using System.Collections.Generic;

namespace MyMessageApp.Data.Domain.Entities
{
    public partial class Role
    {
        public Role()
        {
            RolePages = new HashSet<RolePage>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual User? UpdatedByNavigation { get; set; }
        public virtual ICollection<RolePage> RolePages { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
