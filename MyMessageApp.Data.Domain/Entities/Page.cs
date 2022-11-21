using System;
using System.Collections.Generic;

namespace MyMessageApp.Data.Domain.Entities
{
    public partial class Page
    {
        public Page()
        {
            RolePages = new HashSet<RolePage>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual ICollection<RolePage> RolePages { get; set; }
    }
}
