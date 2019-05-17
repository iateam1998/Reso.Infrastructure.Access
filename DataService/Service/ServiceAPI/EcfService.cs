using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IEcfService : IBaseService<Ecf, EcfViewModel>
    {
        EcfViewModel GetByAppCharId(int applicationCharacteristicId, bool? active);
    }
    public class EcfService : BaseService<Ecf, EcfViewModel>, IEcfService
    {
        public EcfService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public EcfViewModel GetByAppCharId(int applicationCharacteristicId, bool? active)
        {
            return this.FirstOrDefault(p =>
            p.ApplicationCharacteristicId == applicationCharacteristicId
            && (active == null || p.Active == false)
            );
        }
    }
}
