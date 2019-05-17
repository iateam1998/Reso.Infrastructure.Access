using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataService.Model;
using Microsoft.EntityFrameworkCore;

namespace DataService.Service
{ 
   
    public interface IBaseService<TEntity, TViewModel>
        where TEntity : class, new() 
        where TViewModel : BaseViewModel<TEntity>, new()
    {
        TViewModel FindById(int id);
        Task<TViewModel> FindByIdAsync(int id);
        IEnumerable<TViewModel> GetAll();
        IEnumerable<TViewModel> GetAllActive();
        IEnumerable<TViewModel> Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TViewModel> GetActive(Expression<Func<TEntity, bool>> predicate);
       
        TViewModel FirstOrDefault();
        TViewModel FirstOrDefaultActive();
        TViewModel FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TViewModel FirstOrDefaultActive(Expression<Func<TEntity, bool>> predicate);
        Task<TViewModel> FirstOrDefaultAsync();
        Task<TViewModel> FirstOrDefaultActiveAsync();
        Task<TViewModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TViewModel> FirstOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate);

        TViewModel LastOrDefault();
        TViewModel LastOrDefaultActive();
        TViewModel LastOrDefault(Expression<Func<TEntity, bool>> predicate);
        TViewModel LastOrDefaultActive(Expression<Func<TEntity, bool>> predicate);
        Task<TViewModel> LastOrDefaultAsync();
        Task<TViewModel> LastOrDefaultActiveAsync();
        Task<TViewModel> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TViewModel> LastOrDefaultActiveAsync(Expression<Func<TEntity, bool>> predicate);


        TViewModel Create(TViewModel viewModel);
        Task<TViewModel> CreateAsync(TViewModel viewModel);
        TViewModel Update( TViewModel viewModel);
        Task<TViewModel> UpdateAsync(TViewModel viewModel);
        TViewModel Update<TKey>(TKey id, TViewModel viewModel);
        Task<TViewModel> UpdateAsync<TKey>(TKey id, TViewModel viewModel);
        Task<TViewModel> DeactiveAsync<TKey>(TKey id);
        void Delete<TKey>(TKey id);
        Task DeleteAsync<TKey>(TKey id);
        DbSet<TRepo> CreateRepo<TRepo>() where TRepo : class;
    }
}