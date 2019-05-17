using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class Actor
    {
        public Actor()
        {
            UseCaseActor = new HashSet<UseCaseActor>();
        }

        public DateTime CreateTime { get; set; }
        public int ActorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; }

        public virtual RoleActor Role { get; set; }
        public virtual ICollection<UseCaseActor> UseCaseActor { get; set; }
    }
}
