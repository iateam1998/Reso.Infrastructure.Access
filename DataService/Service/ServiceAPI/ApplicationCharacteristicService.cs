using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IApplicationCharacteristicService : IBaseService<ApplicationCharacteristic, ApplicationCharacteristicViewModel>
    {
        ApplicationCharacteristicViewModel GetByAppId(int applicationId, bool? active);
    }
    public class ApplicationCharacteristicService : BaseService<ApplicationCharacteristic, ApplicationCharacteristicViewModel>, IApplicationCharacteristicService
    {
        public ApplicationCharacteristicService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public ApplicationCharacteristicViewModel GetByAppId(int applicationId, bool? active)
        {
            return this.FirstOrDefault(p => p.ApplicationId == applicationId && (active == null || p.Active == active));
        }

    }
}
