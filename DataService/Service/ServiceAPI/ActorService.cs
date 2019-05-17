using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IActorService : IBaseService<Actor, ActorViewModel>
    {
        ActorViewModel GetById(int actorId, int? role, bool? active);
    }
    public class ActorService : BaseService<Actor, ActorViewModel>, IActorService
    {
        public ActorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public ActorViewModel GetById(int actorId, int? roleId, bool? active)
        {
            return this.FirstOrDefault(p => p.ActorId == actorId
            && (active == null || p.Active == active)
            && (roleId == null || p.RoleId == roleId)
            );
        }

    }
}
