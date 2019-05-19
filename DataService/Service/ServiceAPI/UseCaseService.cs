using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        UseCaseViewModel GetById(int useCaseId, int? applicationId, bool? isDone, bool? active);
        UseCaseViewModel Create(UseCaseCreateModel createModel);
        IEnumerable<UseCaseViewModel> Get(UseCaseRequestModel requestModel);
        UseCaseViewModel Update(UseCaseUpdateModel updateModel);
    }
    public class UseCaseService : BaseService<UseCase, UseCaseViewModel>, IUseCaseService
    {
        private IServiceProvider _serviceProvider;
        public UseCaseService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper)
        {
            _serviceProvider = serviceProvider;
        }
        #region Get Method
        public UseCaseViewModel GetById(int useCaseId, int? applicationId, bool? isDone, bool? active)
        {
            return this.Get(p =>
            p.UseCaseId == useCaseId
            && (active == null || p.Active == active)
            && (applicationId == null || p.ApplicationId == applicationId)
            && (isDone == null || p.IsDone == isDone)
            ).FirstOrDefault();
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
        public IEnumerable<UseCaseViewModel> Get(UseCaseRequestModel requestModel)
        {
            var result = this.Get(p =>
             (requestModel.Active == null || requestModel.Active == p.Active)
             && (requestModel.ApplicationId == null || requestModel.ApplicationId == p.ApplicationId)
             && (requestModel.Complexity == null || requestModel.Complexity == p.Complexity)
             && (requestModel.CreateBy == null || p.CreateBy.Contains(requestModel.CreateBy))
             && (requestModel.Goal == null || p.Goal.Contains(requestModel.Goal))
             && (requestModel.IsDone == null || p.IsDone == requestModel.IsDone)
             && (requestModel.Priority == null || requestModel.IsDone == p.IsDone)
             && (requestModel.Status == null || requestModel.Status == p.Status)
             && (requestModel.UseCaseName == null || p.UseCaseName.Contains(requestModel.UseCaseName))
             && (requestModel.UseCaseId == null || requestModel.UseCaseId == p.UseCaseId)
             && (requestModel.Stakeholder == null || p.Stakeholder.Contains(requestModel.Stakeholder))
            ).ToList();
            if (result.Count < 1)
            {
                return new List<UseCaseViewModel>();
            }
            foreach(UseCaseViewModel useCase in result)
            {
                useCase.Application = null;
            }
            return result;
        }
        #endregion

        #region Create Method
        public UseCaseViewModel Create(UseCaseCreateModel createModel)
        {
            var applicationService = ServiceFactory.CreateService<IApplicationService>(_serviceProvider);
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
            result.Application = null;
            return result;
        }
        #endregion
        #region Update Method
        public UseCaseViewModel Update(UseCaseUpdateModel updateModel)
        {
            var checkUseCase = this.FirstOrDefault(p =>
            p.UseCaseId == updateModel.UseCaseId
            && p.ApplicationId == updateModel.ApplicationId
            && p.Active == true);
            if (checkUseCase == null)
            {
                return null;
            }
            var actorService = ServiceFactory.CreateService<IActorService>(_serviceProvider);
            var entityService = ServiceFactory.CreateService<IEntityService>(_serviceProvider);
            var listActor = new List<ActorViewModel>();
            if (updateModel.UseCaseActor.Count > 0)
            {
                foreach (int i in updateModel.UseCaseActor)
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
            if (updateModel.UseCaseEntity.Count > 0)
            {
                foreach (int i in updateModel.UseCaseEntity)
                {
                    var entity = entityService.GetById(i, updateModel.ApplicationId, null);
                    if (entity == null)
                    {
                        return null;
                    }
                    listEntity.Add(entity);
                }
            }
            //Update UseCase Here
            #region Mapping to Update
            //var useCaseUpdate = checkUseCase;
            checkUseCase.Active = true;
            checkUseCase.Complexity = updateModel.Complexity;
            checkUseCase.StartTime = updateModel.StartTime;
            checkUseCase.Note = updateModel.Note;
            checkUseCase.CreateBy = updateModel.CreateBy;
            checkUseCase.CreateTime = updateModel.CreateTime;
            checkUseCase.Deadline = updateModel.Deadline;
            checkUseCase.Description = updateModel.Description;
            checkUseCase.EndTime = updateModel.EndTime;
            checkUseCase.Goal = updateModel.Goal;
            checkUseCase.IsDone = updateModel.IsDone;
            checkUseCase.Priority = updateModel.Priority;
            checkUseCase.Stakeholder = updateModel.Stakeholder;
            checkUseCase.Status = updateModel.Status;
            checkUseCase.UpdateTime = updateModel.UpdateTime;
            checkUseCase.UseCaseName = updateModel.UseCaseName;
            //UseCase Repo
            //var useCaseRepo = this.CreateRepo<UseCase>();
            var result =  this.UpdateAsync(checkUseCase).Result;
            #endregion
            var useCaseActorRepo = this.CreateRepo<UseCaseActor>();
            var useCaseEntityRepo = this.CreateRepo<UseCaseEntity>();
            var useCaseStepRepo = this.CreateRepo<UseCaseStep>();
            try
            {
                if (listEntity.Count > 0)
                {
                    var useCaseEntityInDB = useCaseEntityRepo.Where(p => p.UseCaseId == checkUseCase.UseCaseId).ToList();
                    if (useCaseEntityInDB.Count > 0)
                    {
                        foreach (UseCaseEntity useCaseEntity in useCaseEntityInDB)
                        {
                            useCaseEntityRepo.Remove(useCaseEntity);
                        }
                    }
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
                    var useCaseActorInDB = useCaseActorRepo.Where(p => p.UseCaseId == checkUseCase.UseCaseId).ToList();
                    if (useCaseActorInDB.Count > 0)
                    {
                        foreach (UseCaseActor useCaseActor in useCaseActorInDB)
                        {
                            useCaseActorRepo.Remove(useCaseActor);
                        }
                    }
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
                if (updateModel.UseCaseStep.Count > 0)
                {
                    var useCaseStepInDB = useCaseStepRepo.Where(p => p.UseCaseId == checkUseCase.UseCaseId).ToList();
                    if (useCaseStepInDB.Count > 0)
                    {
                        foreach (UseCaseStep useCaseStep in useCaseStepInDB)
                        {
                            useCaseStepRepo.Remove(useCaseStep);
                        }
                    }
                    foreach (string item in updateModel.UseCaseStep)
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
                //this.Update(checkUseCase);
                return null;
            }
            this.UnitOfWork.Commit();
            result = this.Get(p => p.UseCaseId == result.UseCaseId).FirstOrDefault();
            result.Application = null;
            return result;
        }
        #endregion
    }
}
