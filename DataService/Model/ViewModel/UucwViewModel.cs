using DataService.DBEntity;
using System;

namespace DataService.Model.ViewModel
{
    public class UucwViewModel : BaseViewModel<Uucw>
    {
        public int Uucwid { get; set; }
        public int ApplicationCharacteristicId { get; set; }
        public int Simple { get; set; }
        public int Average { get; set; }
        public int Complex { get; set; }

        private double SimpleWeight = 5;
        private double AverageWeight = 10;
        private double ComplexWeight = 15;

        public double TotalUucw
        {
            get
            {
                return Math.Round((this.Simple * SimpleWeight) + (this.Average * this.AverageWeight) + (this.Complex * this.ComplexWeight),2);
            }
        }

        public bool Active { get; set; }

        //public virtual ApplicationCharacteristic ApplicationCharacteristic { get; set; }
    }
}