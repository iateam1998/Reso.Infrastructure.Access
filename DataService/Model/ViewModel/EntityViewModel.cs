using DataService.DBEntity;
using System;
using System.Collections.Generic;

namespace DataService.Model.ViewModel
{
    public class EntityViewModel : BaseViewModel<Entity>
    {
        public int EntityId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int ApplicationId { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public virtual ApplicationViewModel Application { get; set; }
        public virtual ICollection<UseCaseEntityViewModel> UseCaseEntity { get; set; }
    }
}