using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IUawService : IBaseService<Uaw, UawViewModel>
    {
        UawViewModel GetByAppCharId(int applicationCharacteristicId, bool? active);
    }
    public class UawService : BaseService<Uaw, UawViewModel>, IUawService
    {
        public UawService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public UawViewModel GetByAppCharId(int applicationCharacteristicId, bool? active)
        {
            return this.FirstOrDefault(p =>
            p.ApplicationCharacteristicId == applicationCharacteristicId
            && (active == null || p.Active == false)
            );
        }
    }
}
