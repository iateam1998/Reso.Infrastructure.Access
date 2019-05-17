using DataService.DBEntity;
using System;
using System.Collections.Generic;

namespace DataService.Model.ViewModel
{
    public class UseCaseViewModel : BaseViewModel<UseCase>
    {
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

        public virtual ApplicationViewModel Application { get; set; }
        public virtual ICollection<UseCaseActorViewModel> UseCaseActor { get; set; }
        public virtual ICollection<UseCaseEntityViewModel> UseCaseEntity { get; set; }
        public virtual ICollection<UseCaseStepViewModel> UseCaseStep { get; set; }
    }
}