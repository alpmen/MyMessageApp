using System;
using System.Collections.Generic;

namespace MyMessageApp.Data.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            InverseCreatedByNavigation = new HashSet<User>();
            InverseUpdatedByNavigation = new HashSet<User>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            Pages = new HashSet<Page>();
            RoleCreatedByNavigations = new HashSet<Role>();
            RolePages = new HashSet<RolePage>();
            RoleUpdatedByNavigations = new HashSet<Role>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual User? UpdatedByNavigation { get; set; }
        public virtual ICollection<User> InverseCreatedByNavigation { get; set; }
        public virtual ICollection<User> InverseUpdatedByNavigation { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
        public virtual ICollection<Role> RoleCreatedByNavigations { get; set; }
        public virtual ICollection<RolePage> RolePages { get; set; }
        public virtual ICollection<Role> RoleUpdatedByNavigations { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
