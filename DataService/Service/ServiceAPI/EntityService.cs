using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IEntityService : IBaseService<Entity, EntityViewModel>
    {
        EntityViewModel GetById(int entityId, int? applicationId, bool? active);
    }
    public class EntityService : BaseService<Entity, EntityViewModel>, IEntityService
    {
        public EntityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public EntityViewModel GetById(int entityId, int? applicationId, bool? active)
        {
            return this.FirstOrDefault(p =>
            p.EntityId == entityId
            && (applicationId == null || p.ApplicationId == applicationId)
            && (active ==null || p.Active == active)
            );
        }
    }
}
