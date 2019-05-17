using DataService.DBEntity;

namespace DataService.Model.ViewModel
{
    public class UseCaseActorViewModel:BaseViewModel<UseCaseActor>
    {
        public int UseCaseId { get; set; }
        public int ActorId { get; set; }

        public virtual ActorViewModel Actor { get; set; }
        public virtual UseCaseViewModel UseCase { get; set; }
    }
}