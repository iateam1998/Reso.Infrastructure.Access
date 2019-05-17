using DataService.DBEntity;

namespace DataService.Model.ViewModel
{
    public class UseCaseEntityViewModel :BaseViewModel<UseCaseEntity>
    {
        public int UseCaseId { get; set; }
        public int EntityId { get; set; }

        public virtual EntityViewModel Entity { get; set; }
        public virtual UseCaseViewModel UseCase { get; set; }
    }
}