using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class UseCaseEntity
    {
        public int UseCaseId { get; set; }
        public int EntityId { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual UseCase UseCase { get; set; }
    }
}
