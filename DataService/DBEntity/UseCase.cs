using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class UseCase
    {
        public UseCase()
        {
            UseCaseActor = new HashSet<UseCaseActor>();
            UseCaseEntity = new HashSet<UseCaseEntity>();
            UseCaseStep = new HashSet<UseCaseStep>();
        }

        public int UseCaseId { get; set; }
        public string UseCaseName { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public int ApplicationId { get; set; }
        public string Goal { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public int Complexity { get; set; }
        public bool IsDone { get; set; }
        public string Stakeholder { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }

        public virtual Application Application { get; set; }
        public virtual ICollection<UseCaseActor> UseCaseActor { get; set; }
        public virtual ICollection<UseCaseEntity> UseCaseEntity { get; set; }
        public virtual ICollection<UseCaseStep> UseCaseStep { get; set; }
    }
}
