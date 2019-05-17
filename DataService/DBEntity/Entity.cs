using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class Entity
    {
        public Entity()
        {
            UseCaseEntity = new HashSet<UseCaseEntity>();
        }

        public int EntityId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int ApplicationId { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public virtual Application Application { get; set; }
        public virtual ICollection<UseCaseEntity> UseCaseEntity { get; set; }
    }
}
