using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IUucwService : IBaseService<Uucw, UucwViewModel>
    {
        UucwViewModel GetByAppCharId(int applicationCharacteristicId, bool? active);
    }
    public class UucwService : BaseService<Uucw, UucwViewModel>, IUucwService
    {
        public UucwService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public UucwViewModel GetByAppCharId(int applicationCharacteristicId, bool? active)
        {
            return this.FirstOrDefault(p => 
            p.ApplicationCharacteristicId == applicationCharacteristicId
            && (active == null || p.Active == false)
            );
        }
    }
}
