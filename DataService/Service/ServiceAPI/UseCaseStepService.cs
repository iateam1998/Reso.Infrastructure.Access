using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IUseCaseStepService : IBaseService<UseCaseStep, UseCaseStepViewModel>
    {
        IEnumerable<UseCaseStepViewModel> GetByUseCaseId(int useCaseId);
    }
    public class UseCaseStepService : BaseService<UseCaseStep, UseCaseStepViewModel>, IUseCaseStepService
    {
        public UseCaseStepService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<UseCaseStepViewModel> GetByUseCaseId(int useCaseId)
        {
            return this.Get(p => p.UseCaseId == useCaseId);
        }
    }
}
