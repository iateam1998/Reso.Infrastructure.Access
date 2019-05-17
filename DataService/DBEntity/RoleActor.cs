using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class RoleActor
    {
        public RoleActor()
        {
            Actor = new HashSet<Actor>();
        }

        public int RoleActorId { get; set; }
        public string RoleName { get; set; }
        public int Complexity { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Actor> Actor { get; set; }
    }
}
