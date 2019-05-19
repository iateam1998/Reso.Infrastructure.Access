using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IUseCaseEntityService : IBaseService<UseCaseEntity, UseCaseEntityViewModel>
    {
        IEnumerable<UseCaseEntityViewModel> GetByUseCaseId(int useCaseId);
    }
    public class UseCaseEntityService : BaseService<UseCaseEntity, UseCaseEntityViewModel>, IUseCaseEntityService
    {
        public UseCaseEntityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<UseCaseEntityViewModel> GetByUseCaseId(int useCaseId)
        {
            return this.Get(p => p.UseCaseId == useCaseId);
        }
    }
}
