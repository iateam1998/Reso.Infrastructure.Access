using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataService.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SysLog.Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataService.Service
{
    public interface IActivable
    {
        bool Active { get; set; }
    }
    public class BaseService<TEntity, TViewModel> : IBaseService<TEntity, TViewModel>
        where TEntity : class, new()
        where TViewModel : BaseViewModel<TEntity>, new()
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private DbContext dbContext;

        public IUnitOfWork UnitOfWork { get => unitOfWork; }
        public IMapper Mapper { get => mapper; }

        private DbSet<TEntity> selfDbSet;
        public DbSet<TEntity> SelfDbSet { get => selfDbSet; }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = unitOfWork.DbContext;
            this.selfDbSet = this.dbContext.Set<TEntity>();
            this.mapper = mapper;
        }

        #region Find Services
        public TViewModel FindById(int id)
        {
            var entity = selfDbSet.Find(id);
            return CreateVM(entity);
        }
        public async Task<TViewModel> FindByIdAsync(int id)
        {
            var entity = await selfDbSet.FindAsync(id);
            return CreateVM(entity);
        }
        #endregion
        #region Get Services
        public IEnumerable<TViewModel> GetAll()
        {
            return GetAllAsNoTracking().ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IEnumerable<TViewModel> GetAllActive()
        {
            return GetAllActiveAsNoTracking().ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IEnumerable<TViewModel> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAsNoTracking(predicate).ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IEnumerable<TViewModel> GetActive(Expression<Func<TEntity, bool>> predicate)
        {
            return GetActiveAsNoTracking(predicate).ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        #endregion
        #region First or Default Services
        public TViewModel FirstOrDefault()
        {
            var e = GetAllAsNoTracking().FirstOrDefault();
            return CreateVM(e);
        }
        public TViewModel FirstOrDefaultActive()
        {
            var e = GetAllActiveAsNoTracking().FirstOrDefault();
            return CreateVM(e);
        }
        public TViewModel FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetAsNoTracking(predicate).FirstOrDefault();
            if(e == null)
            {
                return null;
            }
            return CreateVM(e);
        }
        public TViewModel FirstOrDefaultActive(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetActiveAsNoTracking(predicate).FirstOrDefault();
            return CreateVM(e);
        }


        public async Task<TViewModel> FirstOrDefaultAsync()
        {
            var e = await GetAllAsNoTracking().FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultActiveAsync()
        {
            var e = await GetAllActiveAsNoTracking().FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetAsNoTracking(predicate).FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetActiveAsNoTracking(predicate).FirstOrDefaultAsync();
            return CreateVM(e);
        }

        #endregion
        #region Last or Default Services
        public TViewModel LastOrDefault()
        {
            var e = GetAllAsNoTracking().LastOrDefault();
            return CreateVM(e);
        }
        public TViewModel LastOrDefaultActive()
        {
            var e = GetAllActiveAsNoTracking().LastOrDefault();
            return CreateVM(e);
        }
        public TViewModel LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetAsNoTracking(predicate).LastOrDefault();
            return CreateVM(e);
        }
        public TViewModel LastOrDefaultActive(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetActiveAsNoTracking(predicate).LastOrDefault();
            return CreateVM(e);
        }


        public async Task<TViewModel> LastOrDefaultAsync()
        {
            var e = await GetAllAsNoTracking().LastOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> LastOrDefaultActiveAsync()
        {
            var e = await GetAllActiveAsNoTracking().LastOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetAsNoTracking(predicate).LastOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> LastOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetActiveAsNoTracking(predicate).LastOrDefaultAsync();
            return CreateVM(e);
        }
        #endregion
        #region Create Services
        public TViewModel Create(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            selfDbSet.Add(e);
            dbContext.SaveChanges();
            return CreateVM(e);
        }
        public async Task<TViewModel> CreateAsync(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            await selfDbSet.AddAsync(e);
            await dbContext.SaveChangesAsync();
            return CreateVM(e);
        }
        protected TEntity CreateEntity(TViewModel viewModel)
        {
            viewModel.SetMapper(Mapper);
            return viewModel.ToEntity();
        }
        protected TViewModel CreateVM(TEntity entity)
        {
            TViewModel vModel = new TViewModel();
            vModel.SetMapper(Mapper);
            vModel.CopyFromEntity(entity);
            return vModel;
        }
        #endregion
        #region Delete Services
        public void Delete(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            selfDbSet.Remove(e);
            dbContext.SaveChanges();
        }
        public void Delete<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            selfDbSet.Remove(entity);
            dbContext.SaveChanges();
        }
        public async Task DeleteAsync<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            selfDbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        #endregion
        #region Update Services
        public TViewModel Update(TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            dbContext.SaveChanges();
            return CreateVM(entity);
        }

        public async Task<TViewModel> UpdateAsync(TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);
        }
        public TViewModel Update<TKey>(TKey id, TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            dbContext.SaveChanges();
            return CreateVM(entity);
        }
        public async Task<TViewModel> UpdateAsync<TKey>(TKey id, TViewModel viewModel)
        {
            var entity = await selfDbSet.FindAsync(id);
            entity = VMToE(viewModel, entity);
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);
        }
        public async Task<TViewModel> DeactiveAsync<TKey>(TKey id)
        {
            var entity = await selfDbSet.FindAsync(id);
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                ((IActivable)entity).Active = false;
            }
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);

        }
        #endregion
        #region As No Tracking Getter Utils
        protected IQueryable<TEntity> GetAllAsNoTracking()
        {
            return selfDbSet.AsNoTracking();
        }
        protected IQueryable<TEntity> GetAllActiveAsNoTracking()
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                var sourceQuerable = selfDbSet.AsNoTracking();
                Expression<Func<TEntity, bool>> checkActiveExpress = (TEntity t) => ((IActivable)t).Active;
                var cleanCheckActiveExpress = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(checkActiveExpress);
                return sourceQuerable.Where(cleanCheckActiveExpress);
            }
            return this.GetAllAsNoTracking();
        }
        protected IQueryable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return selfDbSet.AsNoTracking().Where(predicate);
        }
        protected IQueryable<TEntity> GetActiveAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                var sourceQuerable = selfDbSet.AsNoTracking().Where<TEntity>(predicate);
                Expression<Func<TEntity, bool>> checkActiveExpress = (TEntity t) => ((IActivable)t).Active;
                var cleanCheckActiveExpress = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(checkActiveExpress);
                return sourceQuerable.Where(cleanCheckActiveExpress);

            }
            return this.GetAsNoTracking(predicate);
        }
        #endregion
        #region Mapping Utils
        protected TEntity VMToE(TViewModel model)
        {
            var e = new TEntity();
            model.SetMapper(Mapper);
            model.CopyToEntity(e);
            return e;
        }
        protected TEntity VMToE(TViewModel model, TEntity e)
        {
            model.SetMapper(Mapper);
            model.CopyToEntity(e);
            return e;
        }

        protected TViewModel EToVM(TEntity e)
        {
            var model = new TViewModel();
            model.SetMapper(Mapper);
            model.CopyFromEntity(e);
            return model;
        }

        protected TViewModel EToVM(TEntity e, TViewModel model)
        {
            model.SetMapper(Mapper);
            model.CopyFromEntity(e);
            return model;
        }


        #endregion
        #region Create Repo
        public DbSet<TRepo> CreateRepo<TRepo>() where TRepo : class
        {
            return unitOfWork.DbContext.Set<TRepo>();
        }
        #endregion
    }

    public class BaseService<TEntity, TViewModel, TDbContext> : IBaseService<TEntity, TViewModel>
        where TEntity : class, new()
        where TViewModel : BaseViewModel<TEntity>, new()
        where TDbContext : DbContext
    {
        private IUnitOfWork<TDbContext> unitOfWork;
        private IMapper mapper;
        private TDbContext dbContext;

        public IUnitOfWork<TDbContext> UnitOfWork { get => unitOfWork; }
        public IMapper Mapper { get => mapper; }

        private DbSet<TEntity> selfDbSet;
        public DbSet<TEntity> SelfDbSet { get => selfDbSet; }

        public BaseService(IUnitOfWork<TDbContext> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = unitOfWork.DbContext;
            this.selfDbSet = this.dbContext.Set<TEntity>();
            this.mapper = mapper;
        }
        #region Create Repo
        public DbSet<TRepo> CreateRepo<TRepo>() where TRepo : class
        {
            return unitOfWork.DbContext.Set<TRepo>();
        }
        #endregion
        #region Find Services
        public TViewModel FindById(int id)
        {
            var entity = selfDbSet.Find(id);
            return CreateVM(entity);
        }
        public async Task<TViewModel> FindByIdAsync(int id)
        {
            var entity = await selfDbSet.FindAsync(id);
            return CreateVM(entity);
        }
        #endregion
        #region Get Services
        public IEnumerable<TViewModel> GetAll()
        {
            return GetAllAsNoTracking().ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IEnumerable<TViewModel> GetAllActive()
        {
            return GetAllActiveAsNoTracking().ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IEnumerable<TViewModel> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAsNoTracking(predicate).ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        public IEnumerable<TViewModel> GetActive(Expression<Func<TEntity, bool>> predicate)
        {
            return GetActiveAsNoTracking(predicate).ProjectTo<TViewModel>(this.Mapper.ConfigurationProvider);
        }
        #endregion
        #region First or Default Services
        public TViewModel FirstOrDefault()
        {
            var e = GetAllAsNoTracking().FirstOrDefault();
            return CreateVM(e);
        }
        public TViewModel FirstOrDefaultActive()
        {
            var e = GetAllActiveAsNoTracking().FirstOrDefault();
            return CreateVM(e);
        }
        public TViewModel FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetAsNoTracking(predicate).FirstOrDefault();
            return CreateVM(e);
        }
        public TViewModel FirstOrDefaultActive(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetActiveAsNoTracking(predicate).FirstOrDefault();
            return CreateVM(e);
        }


        public async Task<TViewModel> FirstOrDefaultAsync()
        {
            var e = await GetAllAsNoTracking().FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultActiveAsync()
        {
            var e = await GetAllActiveAsNoTracking().FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetAsNoTracking(predicate).FirstOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> FirstOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetActiveAsNoTracking(predicate).FirstOrDefaultAsync();
            return CreateVM(e);
        }

        #endregion
        #region Last or Default Services
        public TViewModel LastOrDefault()
        {
            var e = GetAllAsNoTracking().LastOrDefault();
            return CreateVM(e);
        }
        public TViewModel LastOrDefaultActive()
        {
            var e = GetAllActiveAsNoTracking().LastOrDefault();
            return CreateVM(e);
        }
        public TViewModel LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetAsNoTracking(predicate).LastOrDefault();
            return CreateVM(e);
        }
        public TViewModel LastOrDefaultActive(Expression<Func<TEntity, bool>> predicate)
        {
            var e = GetActiveAsNoTracking(predicate).LastOrDefault();
            return CreateVM(e);
        }


        public async Task<TViewModel> LastOrDefaultAsync()
        {
            var e = await GetAllAsNoTracking().LastOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> LastOrDefaultActiveAsync()
        {
            var e = await GetAllActiveAsNoTracking().LastOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetAsNoTracking(predicate).LastOrDefaultAsync();
            return CreateVM(e);
        }

        public async Task<TViewModel> LastOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var e = await GetActiveAsNoTracking(predicate).LastOrDefaultAsync();
            return CreateVM(e);
        }
        #endregion
        #region Create Services
        public TViewModel Create(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            selfDbSet.Add(e);
            dbContext.SaveChanges();
            return CreateVM(e);
        }
        public async Task<TViewModel> CreateAsync(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            await selfDbSet.AddAsync(e);
            await dbContext.SaveChangesAsync();
            return CreateVM(e);
        }
        protected TEntity CreateEntity(TViewModel viewModel)
        {
            viewModel.SetMapper(Mapper);
            return viewModel.ToEntity();
        }
        protected TViewModel CreateVM(TEntity entity)
        {
            TViewModel vModel = new TViewModel();
            vModel.SetMapper(Mapper);
            vModel.CopyFromEntity(entity);
            return vModel;
        }
        #endregion
        #region Delete Services
        public void Delete(TViewModel viewModel)
        {
            var e = CreateEntity(viewModel);
            selfDbSet.Remove(e);
            dbContext.SaveChanges();
        }
        public void Delete<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            selfDbSet.Remove(entity);
            dbContext.SaveChanges();
        }
        public async Task DeleteAsync<TKey>(TKey id)
        {
            var entity = selfDbSet.Find(id);
            selfDbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        #endregion
        #region Update Services
        public TViewModel Update(TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            dbContext.SaveChanges();
            return CreateVM(entity);
        }

        public async Task<TViewModel> UpdateAsync(TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);
        }
        public TViewModel Update<TKey>(TKey id, TViewModel viewModel)
        {
            var entity = VMToE(viewModel);
            selfDbSet.Update(entity);
            dbContext.SaveChanges();
            return CreateVM(entity);
        }
        public async Task<TViewModel> UpdateAsync<TKey>(TKey id, TViewModel viewModel)
        {
            var entity = await selfDbSet.FindAsync(id);
            entity = VMToE(viewModel, entity);
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);
        }
        public async Task<TViewModel> DeactiveAsync<TKey>(TKey id)
        {
            var entity = await selfDbSet.FindAsync(id);
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                ((IActivable)entity).Active = false;
            }
            selfDbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return CreateVM(entity);

        }
        #endregion
        #region As No Tracking Getter Utils
        protected IQueryable<TEntity> GetAllAsNoTracking()
        {
            return selfDbSet.AsNoTracking();
        }
        protected IQueryable<TEntity> GetAllActiveAsNoTracking()
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                var sourceQuerable = selfDbSet.AsNoTracking();
                Expression<Func<TEntity, bool>> checkActiveExpress = (TEntity t) => ((IActivable)t).Active;
                var cleanCheckActiveExpress = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(checkActiveExpress);
                return sourceQuerable.Where(cleanCheckActiveExpress);
            }
            return this.GetAllAsNoTracking();
        }
        protected IQueryable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return selfDbSet.AsNoTracking().Where(predicate);
        }
        protected IQueryable<TEntity> GetActiveAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            if (typeof(IActivable).IsAssignableFrom(typeof(TEntity)))
            {
                var sourceQuerable = selfDbSet.AsNoTracking().Where<TEntity>(predicate);
                Expression<Func<TEntity, bool>> checkActiveExpress = (TEntity t) => ((IActivable)t).Active;
                var cleanCheckActiveExpress = (Expression<Func<TEntity, bool>>)RemoveCastsVisitor.Visit(checkActiveExpress);
                return sourceQuerable.Where(cleanCheckActiveExpress);

            }
            return this.GetAsNoTracking(predicate);
        }
        #endregion
        #region Mapping Utils
        protected TEntity VMToE(TViewModel model)
        {
            var e = new TEntity();
            model.SetMapper(Mapper);
            model.CopyToEntity(e);
            return e;
        }
        protected TEntity VMToE(TViewModel model, TEntity e)
        {
            model.SetMapper(Mapper);
            model.CopyToEntity(e);
            return e;
        }

        protected TViewModel EToVM(TEntity e)
        {
            var model = new TViewModel();
            model.SetMapper(Mapper);
            model.CopyFromEntity(e);
            return model;
        }

        protected TViewModel EToVM(TEntity e, TViewModel model)
        {
            model.SetMapper(Mapper);
            model.CopyFromEntity(e);
            return model;
        }


        #endregion
    }
}
