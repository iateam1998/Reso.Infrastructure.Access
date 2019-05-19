using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IUseCaseActorService : IBaseService<UseCaseActor, UseCaseActorViewModel>
    {
        IEnumerable<UseCaseActorViewModel> GetByUseCaseId(int useCaseId);
    }
    public class UseCaseActorService : BaseService<UseCaseActor, UseCaseActorViewModel>, IUseCaseActorService
    {
        public UseCaseActorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public IEnumerable<UseCaseActorViewModel> GetByUseCaseId(int useCaseId)
        {
            return this.Get(p => p.UseCaseId == useCaseId);
        }
    }
}
