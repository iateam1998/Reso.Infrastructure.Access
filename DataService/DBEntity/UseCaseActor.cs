using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class UseCaseActor
    {
        public int UseCaseId { get; set; }
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual UseCase UseCase { get; set; }
    }
}
