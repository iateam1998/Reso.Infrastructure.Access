using DataService.DBEntity;
using System;

namespace DataService.Model.ViewModel
{
    public class UawViewModel : BaseViewModel<Uaw>
    {
        public int Uawid { get; set; }
        public int ApplicationCharacteristicId { get; set; }
        public int Simple { get; set; }
        public int Average { get; set; }
        public int Complex { get; set; }
        public bool Active { get; set; }

        private double SimpleWeight = 1;
        private double AverageWeight = 2;
        private double ComplexWeight = 3;

        public double TotalUaw
        {
            get
            {
                return Math.Round((this.Simple * SimpleWeight) + (this.Average * this.AverageWeight) + (this.Complex * this.ComplexWeight),2);
            }
        }


        //public virtual ApplicationCharacteristic ApplicationCharacteristic { get; set; }
    }
}