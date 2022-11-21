using System;
using System.Collections.Generic;

namespace MyMessageApp.Data.Domain.Entities
{
    public partial class RolePage
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PageId { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CeratedBy { get; set; }

        public virtual User? CeratedByNavigation { get; set; }
        public virtual Page Page { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
