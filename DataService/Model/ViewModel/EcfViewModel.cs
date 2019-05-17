using DataService.DBEntity;
using System;

namespace DataService.Model.ViewModel
{
    public class EcfViewModel : BaseViewModel<Ecf>
    {
        public int Ecfid { get; set; }
        public int ApplicationCharacteristicId { get; set; }
        public int E1 { get; set; } = 0;
        public int E2 { get; set; } = 0;
        public int E3 { get; set; } = 0;
        public int E4 { get; set; } = 0;
        public int E5 { get; set; } = 0;
        public int E6 { get; set; } = 0;
        public int E7 { get; set; } = 0;
        public int E8 { get; set; } = 0;
        public bool Active { get; set; }

        private double E1_Weight = 1.5;
        private double E2_Weight = 0.5;
        private double E3_Weight = 1.0;
        private double E4_Weight = 0.5;
        private double E5_Weight = 1.0;
        private double E6_Weight = 2.0;
        private double E7_Weight = -1.0;
        private double E8_Weight = -1.0;

        public double TotalEF
        {
            get
            {
                return Math.Round(
                    (this.E1 * E1_Weight)
                    + (this.E2 * E2_Weight)
                    + (this.E3 * E3_Weight)
                    + (this.E4 * E4_Weight)
                    + (this.E5 * E5_Weight)
                    + (this.E6 * E6_Weight)
                    + (this.E7 * E7_Weight)
                    + (this.E8 * E8_Weight)
                    ,2);

            }
        }
        public double TotalECF
        {
            get
            {
                return Math.Round(1.4 + (-0.03 * TotalEF),2);
            }
        }

        //public virtual ApplicationCharacteristicViewModel ApplicationCharacteristic { get; set; }
    }
}