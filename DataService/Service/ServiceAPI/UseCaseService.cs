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
    public interface IUseCaseService : IBaseService<UseCase, UseCaseViewModel>
    {
        UseCaseViewModel GetByName(string useCaseName, int? applicationId, bool? isDone, bool? active);
        UseCaseViewModel Create(UseCaseCreateModel createModel);

    }
    public class UseCaseService : BaseService<UseCase, UseCaseViewModel>, IUseCaseService
    {
        private IServiceProvider _serviceProvider;
        public UseCaseService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper)
        {
            _serviceProvider = serviceProvider;
        }
        public UseCaseViewModel GetById(int useCaseId, int? applicationId, bool? isDone, bool? active)
        {
            return this.FirstOrDefault(p =>
            p.UseCaseId == useCaseId
            && (active == null || p.Active == active)
            && (applicationId == null || p.ApplicationId == applicationId)
            && (isDone == null || p.IsDone == isDone)
            );
        }
        public UseCaseViewModel GetByName(string useCaseName, int? applicationId, bool? isDone, bool? active)
        {
            return this.FirstOrDefault(p =>
            p.UseCaseName == useCaseName
            && (active == null || p.Active == active)
            && (applicationId == null || p.ApplicationId == applicationId)
            && (isDone == null || p.IsDone == isDone)
            );
        }
        public UseCaseViewModel Create(UseCaseCreateModel createModel)
        {
            var applicationService = ServiceFactory.CreateService<IApplicationService>(_serviceProvider);
            var uucwService = ServiceFactory.CreateService<IUucwService>(_serviceProvider);
            if (applicationService.GetById(createModel.ApplicationId, null, null, null) == null)
            {
                return null;
            }

            if (GetByName(createModel.UseCaseName, createModel.ApplicationId, null, null) != null)
            {
                return null;
            }
            var actorService = ServiceFactory.CreateService<IActorService>(_serviceProvider);
            var entityService = ServiceFactory.CreateService<IEntityService>(_serviceProvider);
            var listActor = new List<ActorViewModel>();
            if (createModel.UseCaseActor.Count > 0)
            {
                foreach (int i in createModel.UseCaseActor)
                {
                    var actor = actorService.GetById(i, null, null);
                    if (actor == null)
                    {
                        return null;
                    }
                    listActor.Add(actor);
                }
            }
            var listEntity = new List<EntityViewModel>();
            if (createModel.UseCaseEntity.Count > 0)
            {
                foreach (int i in createModel.UseCaseEntity)
                {
                    var entity = entityService.GetById(i, createModel.ApplicationId, null);
                    if (entity == null)
                    {
                        return null;
                    }
                    listEntity.Add(entity);
                }
            }
            var useCaseModel = new UseCaseViewModel()
            {
                Active = true,
                ApplicationId = createModel.ApplicationId,
                Complexity = createModel.Complexity,
                StartTime = createModel.StartTime,
                Note = createModel.Note,
                CreateBy = createModel.CreateBy,
                CreateTime = createModel.CreateTime,
                Deadline = createModel.Deadline,
                Description = createModel.Description,
                EndTime = createModel.EndTime,
                Goal = createModel.Goal,
                IsDone = createModel.IsDone,
                Priority = createModel.Priority,
                Stakeholder = createModel.Stakeholder,
                Status = createModel.Status,
                UpdateTime = createModel.UpdateTime,
                UseCaseName = createModel.UseCaseName
            };
            var result = this.Create(useCaseModel);
            if (result == null)
            {
                return null;
            }

            var useCaseActorRepo = this.CreateRepo<UseCaseActor>();
            var useCaseEntityRepo = this.CreateRepo<UseCaseEntity>();
            var useCaseStepRepo = this.CreateRepo<UseCaseStep>();
            try
            {
                if (listEntity.Count > 0)
                {
                    foreach (EntityViewModel item in listEntity)
                    {
                        var useCaseEnity = new UseCaseEntity()
                        {
                            EntityId = item.EntityId,
                            UseCaseId = result.UseCaseId
                        };
                        useCaseEntityRepo.Add(useCaseEnity);
                    }
                }
                if (listActor.Count > 0)
                {
                    foreach (ActorViewModel item in listActor)
                    {
                        var useCaseActor = new UseCaseActor()
                        {
                            ActorId = item.ActorId,
                            UseCaseId = result.UseCaseId
                        };
                        useCaseActorRepo.Add(useCaseActor);
                    }
                }
                if (createModel.UseCaseStep.Count > 0)
                {
                    foreach (string item in createModel.UseCaseStep)
                    {
                        var useCaseStep = new UseCaseStep()
                        {
                            UseCaseId = result.UseCaseId,
                            Step = item,
                            Active = true,
                            IsDone = false
                        };
                        useCaseStepRepo.Add(useCaseStep);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Delete(result);
                return null;
            }
            this.UnitOfWork.Commit();

            result = this.Get(p => p.UseCaseId == result.UseCaseId).FirstOrDefault();
            var usecaseActor = result.UseCaseActor;

            var uawService = ServiceFactory.CreateService<IUawService>(_serviceProvider);
            var uaw = result.Application.ApplicationCharacteristic.Uaw;
            foreach (UseCaseActorViewModel actor in usecaseActor)
            {
                if (actor.Actor.Role.Complexity == 3)
                {
                    ++uaw.Complex;
                }
                else if (actor.Actor.Role.Complexity == 2)
                {
                    ++uaw.Average;
                }
                else if (actor.Actor.Role.Complexity == 1)
                {
                    ++uaw.Simple;
                }
            }
            uawService.Update(uaw);

            if (result.Complexity == 3)
            {
                ++result.Application.ApplicationCharacteristic.Uucw.Complex;
            }
            else if (result.Complexity == 2)
            {
                ++result.Application.ApplicationCharacteristic.Uucw.Average;
            }
            else
            {
                ++result.Application.ApplicationCharacteristic.Uucw.Simple;
            }
            uucwService.Update(result.Application.ApplicationCharacteristic.Uucw);
            return result;
        }
    }
}
