using System;
using System.Collections.Generic;

namespace DataService.DBEntity
{
    public partial class UseCaseStep
    {
        public int Id { get; set; }
        public int UseCaseId { get; set; }
        public string Step { get; set; }
        public bool IsDone { get; set; }
        public bool Active { get; set; }

        public virtual UseCase UseCase { get; set; }
    }
}
