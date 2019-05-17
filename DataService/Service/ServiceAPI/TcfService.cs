using AutoMapper;
using DataService.DBEntity;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface ITcfService : IBaseService<Tcf, TcfViewModel>
    {
        TcfViewModel GetByAppCharId(int applicationCharacteristicId, bool? active);
    }
    public class TcfService : BaseService<Tcf, TcfViewModel>, ITcfService
    {
        public TcfService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public TcfViewModel GetByAppCharId(int applicationCharacteristicId, bool? active)
        {
            return this.FirstOrDefault(p =>
            p.ApplicationCharacteristicId == applicationCharacteristicId
            && (active == null || p.Active == false)
            );
        }
    }
}
