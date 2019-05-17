using AutoMapper;
using DataService.DBEntity;
using DataService.Model.RequestModel;
using DataService.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Service.ServiceAPI
{
    public interface IApplicationService : IBaseService<Application, ApplicationViewModel>
    {
        ApplicationViewModel GetById(int applicationId, int? stage, int? category, bool? active);
        ApplicationViewModel GetByName(string applicationName, int? stage, int? category, bool? active);
        IEnumerable<ApplicationViewModel> Get(ApplicationRequestModel requestModel);
        ApplicationViewModel Create(ApplicationCreateModel createModel);

    }
    public class ApplicationService : BaseService<Application, ApplicationViewModel>, IApplicationService
    {
        private IServiceProvider _serviceProvider;
        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper)
        {
            _serviceProvider = serviceProvider;
        }

        public ApplicationViewModel GetById(int applicationId, int? stage, int? category, bool? active)
        {
            return this.FirstOrDefault(p => p.ApplicationId == applicationId
            && (active == null || p.Active == active)
            && (stage == null || p.Stage == stage)
            && (category == null || p.Category == category)
            );
        }

        public ApplicationViewModel GetByName(string applicationName, int? stage, int? category, bool? active)
        {
            return this.FirstOrDefault(p =>
            p.ApplicationName == applicationName
            && (active == null || p.Active == active)
            && (stage == null || p.Stage == stage)
            && (category == null || p.Category == category)
            );
        }

        public IEnumerable<ApplicationViewModel> Get(ApplicationRequestModel requestModel)
        {
            var result = this.Get(p =>
            (requestModel.ApplicationId == null || requestModel.ApplicationId == p.ApplicationId)
            && (requestModel.ApplicationName == null || p.ApplicationName.ToLower().Contains(requestModel.ApplicationName))
            && (requestModel.Category == null || p.Category == requestModel.Category)
            && (requestModel.Origin == null || p.Origin == requestModel.Origin)
            && (requestModel.Stage == null || p.Stage == requestModel.Stage)
            && (requestModel.IsDone == null || p.IsDone == requestModel.IsDone)
            && (requestModel.Type == null || p.Type == requestModel.Type)
            && (requestModel.Active == null || p.Active == requestModel.Active)
            ).ToList();
            if (result.Count < 1)
            {
                return new List<ApplicationViewModel>();
            }
            foreach (ApplicationViewModel application in result)
            {
                var applicationChar = application.ApplicationCharacteristic;
                if (applicationChar != null)
                {
                    if (applicationChar.Ecf != null && applicationChar.Uaw != null && applicationChar.Uucw != null && applicationChar.Tcf != null)
                    {
                        applicationChar.TotalUcp = Math.Round((applicationChar.Uucw.TotalUucw + applicationChar.Uaw.TotalUaw) * applicationChar.Tcf.TotalTCF * applicationChar.Ecf.TotalECF,2);
                        applicationChar.EstimatedEffort = Math.Round(applicationChar.TotalUcp * 28,2);
                    }
                }
            }


            return result;
        }

        public ApplicationViewModel Create(ApplicationCreateModel createModel)
        {
            var applicationCharacteristicService = ServiceFactory.CreateService<IApplicationCharacteristicService>(_serviceProvider);
            var uawService = ServiceFactory.CreateService<IUawService>(_serviceProvider);
            var uucwService = ServiceFactory.CreateService<IUucwService>(_serviceProvider);
            var tcfService = ServiceFactory.CreateService<ITcfService>(_serviceProvider);
            var ecfService = ServiceFactory.CreateService<IEcfService>(_serviceProvider);
            if (GetByName(createModel.ApplicationName, createModel.Stage, createModel.Category, null) != null)
            {
                return null;
            }
            var application = new ApplicationViewModel()
            {
                ApplicationName = createModel.ApplicationName,
                Active = true,
                Category = createModel.Category,
                CreateTime = DateTime.Now,
                Description = createModel.Description,
                Efford = createModel.Efford,
                EndDate = createModel.EndDate,
                IsDone = false,
                Note = createModel.Note,
                Origin = createModel.Origin,
                Priority = createModel.Priority,
                SourceCodeUrl = createModel.SourceCodeUrl,
                Stage = createModel.Stage,
                StartDate = createModel.StartDate,
                Status = createModel.Status,
                Team = createModel.Team,
                Technologies = createModel.Technologies,
                Type = createModel.Type,
                UpdateTime = null
            };
            var result = this.Create(application);
            if (result != null)
            {
                var applicationCharacteristicViewModel = new ApplicationCharacteristicViewModel()
                {
                    ApplicationId = result.ApplicationId,
                    ActualEfford = null,
                    Active = true
                };
                var appChar = applicationCharacteristicService.Create(applicationCharacteristicViewModel);
                if (appChar != null)
                {
                    var uucwViewModel = new UucwViewModel()
                    {
                        ApplicationCharacteristicId = appChar.ApplicationCharacteristicId,
                        Active = true,
                        Average = 0,
                        Complex = 0,
                        Simple = 0
                    };
                    uucwService.Create(uucwViewModel);
                    var uawViewModel = new UawViewModel()
                    {
                        ApplicationCharacteristicId = appChar.ApplicationCharacteristicId,
                        Active = true,
                        Average = 0,
                        Complex = 0,
                        Simple = 0
                    };
                    uawService.Create(uawViewModel);
                    var tcfViewModel = new TcfViewModel()
                    {
                        ApplicationCharacteristicId = appChar.ApplicationCharacteristicId,
                        Active = true,
                    };
                    tcfService.Create(tcfViewModel);
                    var ecfViewModel = new EcfViewModel()
                    {
                        ApplicationCharacteristicId = appChar.ApplicationCharacteristicId,
                        Active = true
                    };
                    ecfService.Create(ecfViewModel);

                    result = this.FirstOrDefault(p => p.ApplicationId == result.ApplicationId);
                    return result;
                }
                else
                {
                    this.Delete(result);
                    return null;
                }
            }
            else
            {
                return result;
            }
        }
    }
}
