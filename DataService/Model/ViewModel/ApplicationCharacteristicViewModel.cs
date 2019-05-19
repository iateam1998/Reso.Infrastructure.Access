using DataService.DBEntity;

namespace DataService.Model.ViewModel
{
    public class ApplicationCharacteristicViewModel : BaseViewModel<ApplicationCharacteristic>
    {
        public ApplicationCharacteristicViewModel()
        {
            Uaw = new UawViewModel();
            Uucw = new UucwViewModel();
        }
        public int ApplicationCharacteristicId { get; set; }
        public int ApplicationId { get; set; }
        public int? ActualEfford { get; set; }
        public bool Active { get; set; }
        public double TotalUcp { get; set; }
        public double EstimatedEffort { get; set; }

        //public virtual ApplicationViewModel Application { get; set; }
        public virtual EcfViewModel Ecf { get; set; }
        public virtual TcfViewModel Tcf { get; set; }
        public virtual UawViewModel Uaw { get; set; }
        public virtual UucwViewModel Uucw { get; set; }

    }
}