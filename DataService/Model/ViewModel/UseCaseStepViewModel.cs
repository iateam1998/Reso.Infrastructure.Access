using DataService.DBEntity;

namespace DataService.Model.ViewModel
{
    public class UseCaseStepViewModel : BaseViewModel<UseCaseStep>
    {
        public int Id { get; set; }
        public int UseCaseId { get; set; }
        public string Step { get; set; }
        public bool IsDone { get; set; }
        public bool Active { get; set; }

        public virtual UseCaseViewModel UseCase { get; set; }
    }
}