using DataService.DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Model.ViewModel
{
    public class RoleActorViewModel : BaseViewModel<RoleActor>
    {
        public int RoleActorId { get; set; }
        public string RoleName { get; set; }
        public int Complexity { get; set; }
        public bool Active { get; set; }
    }
}
